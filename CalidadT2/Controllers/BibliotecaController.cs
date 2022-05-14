using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalidadT2.Constantes;
using CalidadT2.Models;
using CalidadT2.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CalidadT2.Controllers
{
    [Authorize]
    public class BibliotecaController : Controller
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IBibliotecaRepository bibliotecaRepository;



        public BibliotecaController(IUsuarioRepository usuarioRepository, IBibliotecaRepository bibliotecaRepository)
        {
            this.usuarioRepository = usuarioRepository;
            this.bibliotecaRepository = bibliotecaRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            Usuario user = LoggedUser();

            var model = bibliotecaRepository.GetBibliotecaByUser(user.Id);

            return View(model);
        }

        [HttpGet]
        public ActionResult Add(int libro)
        {
            Usuario user = LoggedUser();

            var biblioteca = new Biblioteca
            {
                LibroId = libro,
                UsuarioId = user.Id,
                Estado = ESTADO.POR_LEER
            };

            bibliotecaRepository.AgregarLibroBiblioteca(biblioteca);

            //TempData["SuccessMessage"] = "Se añádio el libro a su biblioteca";

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult MarcarComoLeyendo(int libroId)
        {
            Usuario user = LoggedUser();

            var libro = bibliotecaRepository.EncontrarLibroBiblioteca(user.Id,libroId);
            bibliotecaRepository.MarcarLibroLeido(libro);

            //TempData["SuccessMessage"] = "Se marco como leyendo el libro";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult MarcarComoTerminado(int libroId)
        {
            Usuario user = LoggedUser();

            var libro = bibliotecaRepository.EncontrarLibroBiblioteca(user.Id,libroId);

            bibliotecaRepository.MarcarLibroTerminado(libro);

            //TempData["SuccessMessage"] = "Se marco como leyendo el libro";

            return RedirectToAction("Index");
        }

        private Usuario LoggedUser()
        {
            return usuarioRepository.LoggedUser();
        }
    }
}
