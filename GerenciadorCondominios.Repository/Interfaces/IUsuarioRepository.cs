using GerenciadorCondominios.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCondominios.Repository.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        int VerificarSeExisteUsuario();
        Task LogarUsuario(Usuario usuario, bool lembrarUsuario);
        Task<IdentityResult> CriarUsuario(Usuario usuario, string senha);
        Task AddUsuarioRole(Usuario usuario, string role);

        Task<Usuario> GetUsuarioByEmail(string email);
        Task Logout();
    }
}
