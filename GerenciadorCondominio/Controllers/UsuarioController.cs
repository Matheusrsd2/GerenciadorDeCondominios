using GerenciadorCondominio.ViewModels;
using GerenciadorCondominios.Domain.Models;
using GerenciadorCondominios.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominio.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _repo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UsuarioController(IUsuarioRepository repo, IWebHostEnvironment webHostEnvironment)
        {
            _repo = repo;
            _webHostEnvironment = webHostEnvironment;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAll());
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Registrar(RegistroViewModel model, IFormFile foto)
        {
            if(ModelState.IsValid)
            {
                if (foto != null)
                {
                    string diretorioPasta = Path.Combine(_webHostEnvironment.WebRootPath, "imagens");
                    string nomeFoto = Guid.NewGuid().ToString() + foto.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(diretorioPasta, nomeFoto), FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                        model.Foto = "~/Imagens/" + nomeFoto;
                    }
                }

               
                Usuario usuario = new Usuario();
                IdentityResult usuarioCriado;

                if (_repo.VerificarSeExisteUsuario() == 0)
                {
                    usuario.UserName = model.Nome;
                    usuario.CPF = model.CPF;
                    usuario.Email = model.Email;
                    usuario.PhoneNumber = model.Telefone;
                    usuario.Foto = model.Foto;
                    usuario.PrimeiroAcesso = false;
                    usuario.Status = StatusConta.Aprovado;

                    usuarioCriado = await _repo.CriarUsuario(usuario, model.Senha);

                    if (usuarioCriado.Succeeded)
                    {
                        await _repo.AddUsuarioRole(usuario, "Administrador");
                        await _repo.LogarUsuario(usuario, false);
                        return RedirectToAction("Index", "Usuario");
                    }
                }
                usuario.UserName = model.Nome;
                usuario.CPF = model.CPF;
                usuario.Email = model.Email;
                usuario.PhoneNumber = model.Telefone;
                usuario.Foto = model.Foto;
                usuario.PrimeiroAcesso = true;
                usuario.Status = StatusConta.Analisando;

                usuarioCriado = await _repo.CriarUsuario(usuario, model.Senha);

                if (usuarioCriado.Succeeded)
                {
                    return View("Analise", usuario.UserName);
                }

                else
                {
                    foreach (IdentityError erro in usuarioCriado.Errors)
                    {
                        ModelState.AddModelError("", erro.Description);
                    }
                    return View(model);
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                Usuario usuario = await _repo.GetUsuarioByEmail(model.Email);

                if(usuario != null)
                {
                    if(usuario.Status == StatusConta.Analisando)
                    {
                        return View("Analise", usuario.UserName);
                    }
                    else if(usuario.Status == StatusConta.Reprovado)
                    {
                        return View("Reprovado", usuario.UserName);
                    }
                    else if(usuario.PrimeiroAcesso == true)
                    {
                        return RedirectToAction(nameof(RedefinirSenha), usuario);
                    }
                    else
                    {
                        PasswordHasher<Usuario> passwordHasher = new PasswordHasher<Usuario>();
                        if (passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash, model.Senha) == PasswordVerificationResult.Success)
                        {
                            await _repo.LogarUsuario(usuario, false);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Email e/ou senha inválidos");
                            return View(model);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email e/ou senha inválidos");
                    return View(model);
                }
            }
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _repo.Logout();
            return RedirectToAction("Login");
        }

        public IActionResult Analise(string nome)
        {
            return View(nome);
        }

        public IActionResult Reprovado(string nome)
        {
            return View(nome);
        }
        public async Task<JsonResult> AprovarUsuario(string usuarioId)
        {
            Usuario usuario = await _repo.GetById(usuarioId);
            usuario.Status = StatusConta.Aprovado;
            await _repo.Update(usuario);
            return Json(true);

        }

        public async Task<IActionResult> MinhasInformacoes()
        {
            var dados = await _repo.GetUsuarioByName(User);
            return View(dados);
        }

        [HttpGet]
        public async Task<IActionResult> Atualizar(string id)
        {
            Usuario usuario = await _repo.GetById(id);
            if (usuario == null)
                return NotFound();

            AtualizarViewModel model = new AtualizarViewModel
            {
                Id = usuario.Id,
                Nome = usuario.UserName,
                CPF = usuario.CPF,
                Email = usuario.Email,
                Foto = usuario.Foto,
                Telefone = usuario.PhoneNumber
            };
            TempData["Foto"] = usuario.Foto;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Atualizar(AtualizarViewModel viewModel, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                if (foto != null)
                {
                    string diretorioPasta = Path.Combine(_webHostEnvironment.WebRootPath, "imagens");
                    string nomeFoto = Guid.NewGuid().ToString() + foto.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(diretorioPasta, nomeFoto), FileMode.Create))
                    {
                        foto.CopyTo(fileStream);
                        viewModel.Foto = "~/Imagens/" + nomeFoto;
                    }
                }
                Usuario usuario = await _repo.GetById(viewModel.Id);
                usuario.UserName = viewModel.Nome;
                usuario.CPF = viewModel.CPF;
                usuario.PhoneNumber = viewModel.Telefone;
                usuario.Foto = viewModel.Foto;
                usuario.Email = viewModel.Email;

                await _repo.Update(usuario);

                TempData["Atualizacao"] = "Registro atualizado";

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult RedefinirSenha(Usuario usuario)
        {
            LoginViewModel model = new LoginViewModel
            {
                Email = usuario.Email
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RedefinirSenha(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                Usuario usuario = await _repo.GetUsuarioByEmail(model.Email);
                model.Senha = _repo.CodificarSenha(usuario, model.Senha);
                usuario.PasswordHash = model.Senha;
                usuario.PrimeiroAcesso = false;
                await _repo.Update(usuario);
                await _repo.LogarUsuario(usuario, false);

                return RedirectToAction(nameof(MinhasInformacoes));
            }
            return View(model);
        }
            
        
    }
}
