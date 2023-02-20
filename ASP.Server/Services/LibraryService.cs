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

        public List<Book> GetBooks(int limit = 10, int offset = 0)
        {


            List<Book> books = _context.Books
           .Include(b => b.Genres)
           .Include(c => c.Auteur)
           .OrderBy(b => b.Id)
           .Skip(offset)
           .Take(limit)
           .ToList();

            

            return books;
        }


        public Book GetBookById(int id)
        {
            return _context.Books.Include(b => b.Genres).Include(c => c.Auteur).FirstOrDefault(b => b.Id == id);
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
                existingBook.Contenu = book.Contenu;
                existingBook.Auteur = book.Auteur;
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



        public List<Book> GetBooksByGenreService(int limit = 10, int offset = 0, string label = null)
        {





            List<Book> books = _context.Books
               .Include(b => b.Genres)
               .Include(c => c.Auteur)
               .Where(b => b.Genres.Any(g => g.Label == label))
               .OrderBy(b => b.Id)
               .Skip(offset)
               .Take(limit)
               .ToList();



      



            return books;

            }



        public List<Book> GetBooksByAuteurService(String auteurName,int limit = 10, int offset = 0)
        {
            var books = _context.Books
                .Include(b => b.Genres)
                .Include(c => c.Auteur)
                .Where(b => b.Auteur.Name == auteurName)
                .Skip(offset)
               .Take(limit)
                .ToList();

            return books;
        }




        public int GetTotalBooks()
        {
            return _context.Books.Count();
        }


        public Dictionary<string, int> GetBooksCountByAuthor()
        {
            var bookCountsByAuthor = _context.Books
          .Include(b => b.Auteur)
          .ToList()
          .GroupBy(b => b.Auteur.Name)
          .ToDictionary(g => g.Key, g => g.Count());

            return bookCountsByAuthor;
        }


        /* example usage : 
         foreach (var kvp in bookCountsByAuthor)
{
    Console.WriteLine($"{kvp.Key}: {kvp.Value} books");
}
         */



        public (Book book, int wordCount) GetBookWithMaxWordCount()
        {
            var book = _context.Books
                .OrderByDescending(b => b.Contenu.Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length)
                .FirstOrDefault();

            if (book == null)
            {
                return (null, 0);
            }

            int wordCount = book.Contenu.Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;

            return (book, wordCount);
        }

        public (Book book, int wordCount) GetBookWithMinWordCount()
        {
            var book = _context.Books
                .OrderByDescending(b => b.Contenu.Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length)
                .LastOrDefault();

            if (book == null)
            {
                return (null, 0);
            }

            int wordCount = book.Contenu.Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;

            return (book, wordCount);
        }


       


        public int GetWordCountMedian()
        {
            var books = _context.Books.ToList();
            var wordCounts = books.Select(b => b.Contenu.Split(' ').Count()).OrderBy(n => n);
            var medianIndex = wordCounts.Count() / 2;
            if (wordCounts.Count() % 2 == 0)
            {
                return (int)(wordCounts.ElementAt(medianIndex) + wordCounts.ElementAt(medianIndex - 1)) / 2;
            }
            else
            {
                return wordCounts.ElementAt(medianIndex);
            }
        }


        public int GetWordCountMean()
        {
            var bookCount = _context.Books.Count();
            var totalWordCount = _context.Books.Sum(b => b.Contenu.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length);
            return totalWordCount / (int)bookCount;
        }

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



    }


    public class AuteurService
    {
        private readonly LibraryDbContext _context;

        public AuteurService(LibraryDbContext context)
        {
            _context = context;
        }

        public List<Auteur> GetAllAuteurs()
        {
            return _context.Auteurs.ToList();
        }

        public Auteur GetAuteurById(int id)
        {
            return _context.Auteurs.Find(id);
        }

        public void AddAuteur(Auteur auteur)
        {
            _context.Auteurs.Add(auteur);
            _context.SaveChanges();
        }

        public void UpdateAuteur(int id, Auteur updatedAuteur)
        {
            var auteur = _context.Auteurs.Find(id);
            if (auteur != null)
            {
                auteur.Name = updatedAuteur.Name;
                auteur.Age = updatedAuteur.Age;
                _context.SaveChanges();
            }
        }

        public void DeleteAuteur(int id)
        {
            var auteur = _context.Auteurs.Find(id);
            if (auteur != null)
            {
                _context.Auteurs.Remove(auteur);
                _context.SaveChanges();
            }
        }
    }






}
