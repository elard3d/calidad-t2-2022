using System;
using System.Linq;
using CalidadT2.Models;
using CalidadT2.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CalidadT2.Controllers
{
    public class LibroController : Controller
    {

        private readonly IUsuarioRepository usuarioRepository;
        private readonly ILibroRepository libroRepository;
        public LibroController(ILibroRepository libroRepository, IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
            this.libroRepository = libroRepository;
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var model = libroRepository.DetalleLibro(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult AddComentario(Comentario comentario)
        {
            Usuario user = LoggedUser();
            libroRepository.ComentarLibro(comentario,user.Id);

            var libro = libroRepository.GetLibroById(comentario.LibroId);
            libroRepository.puntaje(libro,comentario);

            return RedirectToAction("Details", new { id = comentario.LibroId });
        }

        private Usuario LoggedUser()
        {
            return usuarioRepository.LoggedUser();
        }
    }
}
