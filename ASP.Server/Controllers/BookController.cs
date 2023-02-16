using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ASP.Server.Service;
using System.Threading.Tasks;


namespace ASP.Server.Controllers
{
    public class CreateBookModel
    {
        [Required]
        [Display(Name = "Nom")]
        public String Name { get; set; }

        // Ajouter ici tous les champ que l'utilisateur devra remplir pour ajouter un livre

        // Liste des genres séléctionné par l'utilisateur
        public List<int> Genres { get; set; }

        // Liste des genres a afficher à l'utilisateur
        public IEnumerable<Genre> AllGenres { get; init;  }
    }

    public class BookController : Controller
    {
        private readonly LibraryDbContext libraryDbContext;

        private BookService bookService;
        private GenreService genreService;

        public BookController(BookService bookService, LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;

            this.bookService = bookService;
        }

        public  ActionResult<IEnumerable<Book>> List()
        {
            // récupérer les livres dans la base de donées pour qu'elle puisse être affiché
            List<Book> ListBooks = this.bookService.GetBooks();

          
            return View(ListBooks);
        }

        public ActionResult<CreateBookModel> Create(CreateBookModel book)
        {
            // Le IsValid est True uniquement si tous les champs de CreateBookModel marqués Required sont remplis
            if (ModelState.IsValid)
            {
                // Il faut intéroger la base pour récupérer l'ensemble des objets genre qui correspond aux id dans CreateBookModel.Genres
                List<Genre> genres = null;
                // Completer la création du livre avec toute les information nécéssaire que vous aurez ajoutez, et metter la liste des gener récupéré de la base aussi
            //    bookService.AddBook(b);

                //redirect
            }

            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            return View(new CreateBookModel() { AllGenres = null } );
        }

        [HttpDelete]
        public bool Delete([FromForm] int[] ids)
        {
            Console.WriteLine("in delete function");

            try
            {
                foreach (var id in ids)
                {
                    bookService.DeleteBook(id);
                }

             }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }


            return true;

        }


         public String testa()
        {
            Book b = new Book { Titre = "hello" };

            bookService.AddBook(b);


            List<Book> bListTest = bookService.GetBooks();
            foreach (var book in bListTest)
            {
                Console.WriteLine($"- Book ID: {book.Id}, Titre: {book.Titre}, Prix: {book.Prix}");
            }
            return "hi";
           // return libraryDbContext.Books.Single(a => a.Titre == "48 law of power").Titre;
        }


    }
}
