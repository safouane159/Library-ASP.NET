using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.Server.Database;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace ASP.Server.Api
{

    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibraryDbContext libraryDbContext;

        public BookController(LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }


        [HttpGet]
        public ActionResult<List<BookDTO>> GetBooks(int limit = 10, int offset = 0)
        {

            var books = libraryDbContext.Books
            .Include(b => b.Genres)
            .OrderBy(b => b.Id)
            .Skip(offset)
            .Take(limit)
            .ToList();

            var bookDTO = books.Select(b => b.ToBookDTO());

            return Ok(bookDTO);

        }

        [HttpGet]
        public ActionResult<List<BookDTO>> GetBooksByGenre(int limit = 10, int offset = 0, string label = null)
        {
            if (string.IsNullOrWhiteSpace(label) ||  string.IsNullOrEmpty(label))
            {
                return BadRequest("Genre label cannot be empty.");
                throw new ArgumentException($"'{nameof(label)}' cannot be null or whitespace.", nameof(label));
                   
            }else {
                var books = libraryDbContext.Books
               .Include(b => b.Genres)
               .Where(b => b.Genres.Any(g => g.Label == label))
               .OrderBy(b => b.Id)
               .Skip(offset)
               .Take(limit)
               .ToList();

                if (books == null || books.Count == 0)
                {
                    //return NotFound($"No book found of genre :'{label}' ! try another Genre. ");
                    return new List<BookDTO>();
                }

                var bookDTO = books.Select(b => b.ToBookDTO());

                return Ok(bookDTO);
            }

           

        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = libraryDbContext.Books.Include(b => b.Genres).FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                //return NotFound();
                return new Book();
            }

            return Ok(book);
        }



    }
}

