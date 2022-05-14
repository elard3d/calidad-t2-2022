using CalidadT2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalidadT2.Repositories
{
    public interface ILibroRepository
    {
        List<Libro> GetAllLibros();
        Libro DetalleLibro(int id);
        void ComentarLibro(Comentario comentario, int userid);
        Libro GetLibroById(int id);
        void puntaje(Libro libro, Comentario comentario);

    }
    public class LibroRepository : ILibroRepository
    {
        private readonly AppBibliotecaContext context;

        public LibroRepository(AppBibliotecaContext context)
        {
            this.context = context;
        }

        public void ComentarLibro(Comentario comentario,int userid)
        {
            comentario.UsuarioId = userid;
            comentario.Fecha = DateTime.Now;
            context.Comentarios.Add(comentario);

        }

        public Libro DetalleLibro(int id)
        {
            return context.Libros
                .Include("Autor")
                .Include("Comentarios.Usuario")
                .Where(o => o.Id == id)
                .FirstOrDefault();
        }

        public List<Libro> GetAllLibros()
        {
            return context.Libros.Include(o => o.Autor).ToList(); 
        }

        public Libro GetLibroById(int id)
        {
            return context.Libros.Where(o => o.Id == id).FirstOrDefault();
        }

        public void puntaje(Libro libro,Comentario comentario)
        {
            libro.Puntaje = (libro.Puntaje + comentario.Puntaje) / 2;
            context.SaveChanges();
        }
    }
}
