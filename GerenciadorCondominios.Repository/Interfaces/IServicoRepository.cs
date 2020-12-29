using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GerenciadorCondominios.Domain.Models;

namespace GerenciadorCondominios.Repository.Interfaces
{
    public interface IServicoRepository : IRepository<Servico>
    {
        Task<IEnumerable<Servico>> GetServicoByUsuario(string usuarioId);
    }
}