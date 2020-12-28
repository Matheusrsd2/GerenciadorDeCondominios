using GerenciadorCondominios.Domain.Models;
using GerenciadorCondominios.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCondominios.Repository.Repositories
{
    public class VeiculoRepository : Repository<Veiculo>, IVeiculoRepository
    {
        private readonly Context _context;

        public VeiculoRepository(Context context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Veiculo>> GetVeiculosByUsuario(string usuarioId)
        {
            try
            {
                return await _context.Veiculos.Where(v => v.UsuarioId == usuarioId).ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
