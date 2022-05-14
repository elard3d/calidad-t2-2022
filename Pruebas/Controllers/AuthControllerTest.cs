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
    public class AuthControllerTest
    {      

        [Test]
        public void PruebaLogin()
        {

            var mock = new Mock<IUsuarioRepository>();
            mock.Setup(o => o.FindUserByCredentials("admin", "admin")).Returns(new Usuario());

            var authMock = new Mock<IAuthService>();

            var controller = new AuthController(mock.Object, authMock.Object);

            var result = controller.Login("admin", "admin");

            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }

        [Test]
        public void PruebaLoginError()
        {

            var mock = new Mock<IUsuarioRepository>();
            mock.Setup(o => o.FindUserByCredentials("usuario", "12345678")).Returns((Usuario)null);
            var controller = new AuthController(mock.Object, null);

            var result = controller.Login("usuario", "12345678") as ViewResult;

            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsFalse(controller.ModelState.IsValid);
        }


        [Test]
        public void PruebaCerrarSesion()
        {
            var authMock = new Mock<IAuthService>();

            var controller = new AuthController(null, authMock.Object);

            var result = controller.Logout();

            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }
    }
}