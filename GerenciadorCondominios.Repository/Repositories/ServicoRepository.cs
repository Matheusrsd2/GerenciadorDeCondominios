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
    public class ServicoRepository : Repository<Servico>, IServicoRepository
    {
        private Context _context;

        public ServicoRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Servico>> GetServicoByUsuario(string usuarioId)
        {
            return await _context.Servicos.Where(s => s.UsuarioId == usuarioId).ToListAsync();
        }
    }
}