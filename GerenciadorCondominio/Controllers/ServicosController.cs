using GerenciadorCondominio.ViewModels;
using GerenciadorCondominios.Domain.Enums;
using GerenciadorCondominios.Domain.Models;
using GerenciadorCondominios.Repository.Interfaces;
using GerenciadorCondominios.Repository.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GerenciadorCondominios.Controllers
{
    public class ServicosController : Controller
    {
        private IServicoRepository _servicoRepository;
        private IUsuarioRepository _usuarioRepository;
        private IServicoPredioRepository _servicoPredioRepository;
        private IHistoricoRecursosRepository _historicoRecursosRepository;
        

        public ServicosController(IServicoRepository servicoRepo, IUsuarioRepository usuarioRepo,IServicoPredioRepository servicoPredioRepository,IHistoricoRecursosRepository historicoRecursosRepository)
        {
            _servicoRepository = servicoRepo;
            _usuarioRepository = usuarioRepo;
            _historicoRecursosRepository = historicoRecursosRepository;
            _servicoPredioRepository = servicoPredioRepository;
        }

        public async Task<IActionResult> Index()
        {
            Usuario usuario = await _usuarioRepository.GetUsuarioByName(User);
            if(await _usuarioRepository.VerificarSeUsuarioTemRole(usuario, "Morador"))
            {
                return View(await _servicoRepository.GetServicoByUsuario(usuario.Id));    
            }
            return View(await _servicoRepository.GetAll());
            
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Servico servico)
        {   
            if(ModelState.IsValid)
            {
                Usuario usuario = await _usuarioRepository.GetUsuarioByName(User);
                servico.UsuarioId = usuario.Id;
                servico.Status = StatusServico.Pendente;
                await _servicoRepository.Add(servico);
                TempData["NovoRegistro"] = "Serviço criado com sucesso";
                return RedirectToAction(nameof(Index));
            }
            return View(servico);
        }

        [HttpGet]
        public async Task<IActionResult> AprovarServico(int id)
        {
            Servico servico = await _servicoRepository.GetById(id);
            ServicoAprovadoViewModel viewModel = new ServicoAprovadoViewModel
            {
                Id = servico.Id,
                Nome = servico.Nome
            };
            return View(viewModel);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AprovarServico(ServicoAprovadoViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                Servico servico = await _servicoRepository.GetById(viewModel.Id);
                servico.Status = StatusServico.Aceito;
                await _servicoRepository.Update(servico);

                ServicoPredio servicoPredio = new ServicoPredio
                {
                    ServicoId = viewModel.Id,
                    DataServico = viewModel.Data
                };
                await _servicoPredioRepository.Add(servicoPredio);

                HistoricoRecursos historicoRecursos = new HistoricoRecursos
                {
                    Valor = servico.Valor,
                    MesId = servicoPredio.DataServico.Month,
                    Dia = servicoPredio.DataServico.Day,
                    Ano = servicoPredio.DataServico.Year,
                    Tipos = Tipo.Saida
                };

                await _historicoRecursosRepository.Add(historicoRecursos);
                TempData["NovoRegistro"] = $"{servico.Nome} Escalado com Sucesso!";

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public async Task<IActionResult> RecusarServico(int id)
        {
            Servico servico = await _servicoRepository.GetById(id);
            if (servico == null)
            {
                return NotFound();
            }
            
            servico.Status = StatusServico.Recusado;
            await _servicoRepository.Update(servico);
            TempData["Exclusao"] = "Servico Recusado";

            return RedirectToAction(nameof(Index));
        }

        
    }


}