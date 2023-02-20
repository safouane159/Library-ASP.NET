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
using ASP.Server.Service;

namespace ASP.Server.Api
{

    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibraryDbContext libraryDbContext;
        private BookService bookService;
        public BookController(LibraryDbContext libraryDbContext, BookService bookService)
        {
            this.libraryDbContext = libraryDbContext;
            this.bookService = bookService;
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


                List<Book> books = bookService.GetBooksByGenreService(limit, offset, label);


                if (books == null || books.Count == 0)
                {
                    return NotFound($"No book found of genre :'{label}' ! try another Genre. ");
                }

                List<BookDTO> bookDTO = books.Select(b => b.ToBookDTO()).ToList();
                return bookDTO;
            }

           

        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = libraryDbContext.Books
                .Include(b => b.Genres)
                .Include(b => b.Auteur)
                .FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }





        [HttpGet]
        public ActionResult<List<BookDTO>>  GetBooksByAuteur(String AuteurName)
        {




            if (string.IsNullOrWhiteSpace(AuteurName) || string.IsNullOrEmpty(AuteurName))
            {
                return BadRequest("Genre label cannot be empty.");
                throw new ArgumentException($"'{nameof(AuteurName)}' cannot be null or whitespace.", nameof(AuteurName));

            }
            else
            {


                List<Book> books = bookService.GetBooksByAuteurService(AuteurName);


                if (books == null || books.Count == 0)
                {
                    return NotFound($"No book found of genre :'{AuteurName}' ! try another Genre. ");
                }

                List<BookDTO> bookDTO = books.Select(b => b.ToBookDTO()).ToList();
                return bookDTO;
            }




        }


    }
}

