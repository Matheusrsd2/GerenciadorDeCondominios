using GerenciadorCondominios.Domain.Models;
using GerenciadorCondominios.Repository.Interfaces;
using GerenciadorCondominios.Repository.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GerenciadorCondominios.Controllers
{
    public class EventosController : Controller
    {
        public readonly IEventoRepository _eventoRepository;
        public readonly IUsuarioRepository _usuarioRepository;
        

        public EventosController(IEventoRepository eventoRepository, IUsuarioRepository usuarioRepository)
        {
            _eventoRepository = eventoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IActionResult> Index()
        {
            Usuario usuario = await _usuarioRepository.GetUsuarioByName(User);
            if (await _usuarioRepository.VerificarSeUsuarioTemRole(usuario, "Morador"))
            {
                return View(await _eventoRepository.GetEventoByUsuario(usuario.Id));
            }
            return View(await _eventoRepository.GetAll());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            Usuario usuario = await _usuarioRepository.GetUsuarioByName(User);
            Evento evento = new Evento
            {
                UsuarioId = usuario.Id
            };

            return View(evento);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Evento evento)
        {
            if (ModelState.IsValid)
            {
                await _eventoRepository.Add(evento);
                TempData["NovoRegistro"] = $"Evento {evento.Nome} inserido com sucesso";
                return RedirectToAction(nameof(Index));
            }

            return View(evento);
        }

        
    }
}