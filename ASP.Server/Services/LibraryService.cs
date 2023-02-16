using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP.Server.Database;
using ASP.Server.Model;
using Microsoft.EntityFrameworkCore;

namespace ASP.Server.Service
{
    public  class BookService
    {


        private readonly LibraryDbContext _context;

        public BookService(LibraryDbContext context)
        {
            _context = context;
        }


        public List<Book> GetBooks()
        {
            return _context.Books.Include(b => b.Genres).ToList();
        }

        public Book GetBookById(int id)
        {
            return _context.Books.Include(b => b.Genres).FirstOrDefault(b => b.Id == id);
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void UpdateBook(int id, Book book)
        {
            var existingBook = _context.Books.FirstOrDefault(b => b.Id == id);

            if (existingBook != null)
            {
                existingBook.Titre = book.Titre;
                existingBook.Prix = book.Prix;
                existingBook.Genres = book.Genres;

                _context.SaveChanges();
            }
        }

        public void DeleteBook(int id)
        {
            var existingBook = _context.Books.FirstOrDefault(b => b.Id == id);

            if (existingBook != null)
            {
                _context.Books.Remove(existingBook);
                _context.SaveChanges();
            }
        }
        // Ajouter ici toutes vos fonctions qui peuvent être accéder a différent endroit de votre programme
    }



    public class GenreService
    {


        private readonly LibraryDbContext _context;

        public GenreService(LibraryDbContext context)
        {
            _context = context;
        }


        public List<Genre> GetGenres()
        {
            return _context.Genres.Include(g => g.Books).ToList();
        }

        public Genre GetGenreById(int id)
        {
            return _context.Genres.Include(g => g.Books).FirstOrDefault(g => g.Id == id);
        }

        public void AddGenre(Genre genre)
        {
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }

        public void UpdateGenre(int id, Genre genre)
        {
            var existingGenre = _context.Genres.FirstOrDefault(g => g.Id == id);

            if (existingGenre != null)
            {
                existingGenre.Label = genre.Label;
                existingGenre.Books = genre.Books;

                _context.SaveChanges();
            }
        }

        public void DeleteGenre(int id)
        {
            var existingGenre = _context.Genres.FirstOrDefault(g => g.Id == id);

            if (existingGenre != null)
            {
                _context.Genres.Remove(existingGenre);
                _context.SaveChanges();
            }
        }
        // Ajouter ici toutes vos fonctions qui peuvent être accéder a différent endroit de votre programme
    }
}
