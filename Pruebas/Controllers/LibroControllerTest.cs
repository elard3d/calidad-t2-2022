using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using CalidadT2.Repositories;
using CalidadT2.Controllers;
using CalidadT2.Models;
using Microsoft.AspNetCore.Mvc;
using CalidadT2.Services;

namespace TestT2Calidad.Controllers
{
    class LibroControllerTest
    {
        [Test]
        public void PreubaDetalles()
        {

            var mock = new Mock<IUsuarioRepository>();
            mock.Setup(o => o.LoggedUser()).Returns(new Usuario() { Id = 0 });

            var libromock = new Mock<ILibroRepository>();
            libromock.Setup(o => o.DetalleLibro(0)).Returns(new Libro());

            var controller = new LibroController(libromock.Object, mock.Object);

            var result = controller.Details(0) as ViewResult;

            Assert.IsInstanceOf<ViewResult>(result);
        }
        [Test]
        public void PruebaNuevoComentario()
        {

            var mock = new Mock<IUsuarioRepository>();
            mock.Setup(o => o.LoggedUser()).Returns(new Usuario() { Id = 0 });

            var libromock = new Mock<ILibroRepository>();
            libromock.Setup(o => o.GetLibroById(0)).Returns(new Libro());

            var controller = new LibroController(libromock.Object, mock.Object);

            var result = controller.AddComentario(new Comentario());

            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }
    }
}
