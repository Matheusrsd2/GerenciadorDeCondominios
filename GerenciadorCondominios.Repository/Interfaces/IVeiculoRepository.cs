using GerenciadorCondominios.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCondominios.Repository.Interfaces
{
    public interface IVeiculoRepository : IRepository<Veiculo>
    {
        Task<IEnumerable<Veiculo>> GetVeiculosByUsuario(string usuarioId);
    }
}
