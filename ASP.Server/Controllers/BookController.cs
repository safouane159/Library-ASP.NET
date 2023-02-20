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

        public BookController(BookService bookService, GenreService genreService, LibraryDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
            this.genreService = genreService;
            this.bookService = bookService;
        }


        public ActionResult<IEnumerable<Book>> List([FromQuery] int page = 0)
        {
            // récupérer les livres dans la base de donées pour qu'elle puisse être affiché
            List<Book> ListBooks = this.bookService.GetBooks();

            var limit = 5;
            var offset = limit * page;

            List<Book> ListBooks = this.bookService.GetBooks(limit: limit, offset: offset);

            foreach (var book in ListBooks)
            {
                Console.WriteLine($"auteur => {book.Auteur}");
            }


            return View(ListBooks);
        }

        public ActionResult<Book> CreateUpdateView(int id = 0)
        {
            Book book = null;
            List<Genre> genres = genreService.GetGenres();

            if (id > 0)
            {
                book = bookService.GetBookById(id);
            }

            ViewBag.Genres = genres;


            return View("Create", book);
        }

        /*public ActionResult<CreateBookModel> Create(CreateBookModel book)
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
        }*/



        [HttpPost]
        public String Create(int id,string title, string author, double price, int[] categories, string content)
        {


            /*

            ICollection<Genre> genres = new List<Genre>();

            foreach (var cat in categories)
            {
                Console.WriteLine($"- CAT ID: {cat}");
                genres.Add(genreService.GetGenreById(cat));
            }





            Book book = new Book() { Auteur = author, Contenu = content, Prix = price, Titre = title, Genres = genres };

            if(id == 0)
            {
                bookService.AddBook(book);
            }
            else
            {
                bookService.UpdateBook(id, book);
            }


            return $"- Book title: {title}, Titre: {author}, Prix: {price}";*/
            return "";
        }


        public ActionResult<Book> View(int id = 0)
        {


            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            return View(bookService.GetBookById(id));
        }



        [HttpDelete]
        public bool Delete([FromForm] int[] ids)
        {

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



           Book b1 = bookService.GetBookById(1);

            List<Genre> l= new List<Genre>();

            Console.WriteLine($" book : {bookService.GetBookById(1).Titre} ");
            Console.WriteLine($" added genre : {genreService.GetGenreById(3)} ");
            Console.WriteLine($" added genre : {genreService.GetGenreById(6)} ");


            l.Add(genreService.GetGenreById(7));
            l.Add(genreService.GetGenreById(8));
            b1.Genres.Clear();


            foreach (var genre in l)
            {
                b1.Genres.Add(genre); // Add selected genres to the book's genre collection
            }


            bookService.UpdateBook(b1.Id, b1);




            var mean = bookService.GetWordCountMean();
            var median = bookService.GetWordCountMedian();
            var ( book,min) = bookService.GetBookWithMinWordCount();
            var (book2, max) = bookService.GetBookWithMaxWordCount();
           // Console.WriteLine($"{book.Titre}: {min} books");
           // Console.WriteLine($"{book2.Titre}: {max} books");
           // Console.WriteLine($" le mean : {mean} books");
           // Console.WriteLine($" le median : {median} books");
         
            return "hi";
           // return libraryDbContext.Books.Single(a => a.Titre == "48 law of power").Titre;
        }


    }
}
