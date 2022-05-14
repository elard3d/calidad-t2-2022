using CalidadT2.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalidadT2.Repositories
{
    
    public interface IUsuarioRepository
    {
        Usuario FindUserByCredentials(string username, string password);
        Usuario LoggedUser();

    }
    public class UsuarioRepository : IUsuarioRepository
    {
        private static HttpContext httpContext => new HttpContextAccessor().HttpContext;

        private readonly AppBibliotecaContext context;

        public UsuarioRepository(AppBibliotecaContext context)
        {
            this.context = context;
        }
        public Usuario FindUserByCredentials(string username, string password)
        {
            return context.Usuarios
               .FirstOrDefault(o => o.Username == username && o.Password == password);
        }

        public Usuario LoggedUser()
        {   var claim = httpContext.User.Claims.FirstOrDefault();
            var user = context.Usuarios.Where(o => o.Username == claim.Value).FirstOrDefault();
            return user;
        }
    }
}
