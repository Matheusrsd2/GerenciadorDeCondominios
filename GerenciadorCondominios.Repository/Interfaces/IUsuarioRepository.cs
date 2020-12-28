using GerenciadorCondominios.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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

        Task UpdateUsuario(Usuario usuario);
        Task<Usuario> GetUsuarioByEmail(string email);

        Task<Usuario> GetUsuarioByName(ClaimsPrincipal usuario);
        Task Logout();

        //Usuario roles
        Task<bool> VerificarSeUsuarioTemRole(Usuario usuario, string funcao);

        string CodificarSenha(Usuario usuario, string senha);
      


    }
}
