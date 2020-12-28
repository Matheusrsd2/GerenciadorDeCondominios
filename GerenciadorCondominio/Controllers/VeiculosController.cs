
using GerenciadorCondominios.Domain.Models;
using GerenciadorCondominios.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GerenciadorCondominios.Controllers
{
    [Authorize]
    public class VeiculosController : Controller
    {
        private readonly IVeiculoRepository _veiculoRepositorio;
        private readonly IUsuarioRepository _usuarioRepositorio;

        public VeiculosController(IVeiculoRepository veiculoRepositorio, IUsuarioRepository usuarioRepositorio)
        {
            _veiculoRepositorio = veiculoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = await _usuarioRepositorio.GetUsuarioByName(User);
                veiculo.UsuarioId = usuario.Id;
                await _veiculoRepositorio.Add(veiculo);
                return RedirectToAction("MinhasInformacoes", "Usuario");
            }

            return View(veiculo);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var veiculo = await _veiculoRepositorio.GetById(id);
            if (veiculo == null)
            {
                return NotFound();
            }
            return View(veiculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VeiculoId,Nome,Marca,Cor,Placa,UsuarioId")] Veiculo veiculo)
        {
            if (id != veiculo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _veiculoRepositorio.Update(veiculo);
                return RedirectToAction("MinhasInformacoes", "Usuarios");
            }

            return View(veiculo);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _veiculoRepositorio.Delete(id);
            return Json("Veículo excluído com sucesso");
        }
    }
}