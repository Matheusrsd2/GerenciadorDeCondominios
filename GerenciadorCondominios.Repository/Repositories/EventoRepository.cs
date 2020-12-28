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
    public class EventoRepository : Repository<Evento>, IEventoRepository
    {
        public readonly Context _context;
        public EventoRepository(Context contexto) : base(contexto)
        {
            _context = contexto;
        }

        public async Task<IEnumerable<Evento>> GetEventoByUsuario(string usuarioId)
        {
            return await _context.Eventos.Where(e => e.Usuario.Id == usuarioId).ToListAsync();
        }
    }
}