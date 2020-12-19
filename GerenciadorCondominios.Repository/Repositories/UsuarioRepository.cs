using GerenciadorCondominios.Domain.Models;
using GerenciadorCondominios.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCondominios.Repository.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private readonly Context _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        public UsuarioRepository(Context context, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task AddUsuarioRole(Usuario usuario, string role)
        {
            try
            {
                await _userManager.AddToRoleAsync(usuario, role);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IdentityResult> CriarUsuario(Usuario usuario, string senha)
        {
            try
            {
                return await _userManager.CreateAsync(usuario, senha);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<Usuario> GetUsuarioByEmail(string email)
        {
            try
            {
                return await _userManager.FindByEmailAsync(email);
            }
            catch(Exception e)
            {
                throw e;
            }
        }


        public async Task LogarUsuario(Usuario usuario, bool lembrarUsuario)
        {
            try
            {
                await _signInManager.SignInAsync(usuario, lembrarUsuario);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int VerificarSeExisteUsuario()
        {
            try
            {
                return _context.Usuarios.Count();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
