using GerenciadorCondominios.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GerenciadorCondominios.ViewComponents
{
    public class VeiculosViewComponent : ViewComponent
    {
        private readonly IVeiculoRepository _veiculoRepositorio;

        public VeiculosViewComponent(IVeiculoRepository veiculoRepositorio)
        {
            _veiculoRepositorio = veiculoRepositorio;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            return View(await _veiculoRepositorio.GetVeiculosByUsuario(id));
        }
    }
}