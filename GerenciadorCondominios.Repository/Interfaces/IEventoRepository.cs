using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GerenciadorCondominios.Domain.Models;
using GerenciadorCondominios.Repository.Repositories;

namespace GerenciadorCondominios.Repository.Interfaces
{
    public interface IEventoRepository : IRepository<Evento>
    {
        Task<IEnumerable<Evento>> GetEventoByUsuario(string usuarioId);
    }
}