using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Database;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.Server.Service;

namespace ASP.Server.Controllers
{
	public class AuteurController : Controller
    {

        private readonly LibraryDbContext libraryDbContext;
        private BookService bookService;
        private GenreService genreService;
        private AuteurService auteurService;

        public AuteurController(LibraryDbContext libraryDbContext, BookService bookService, GenreService genreService, AuteurService auteurService)
		{
            this.libraryDbContext = libraryDbContext;
            this.genreService = genreService;
            this.bookService = bookService;
            this.auteurService = auteurService;
        }



        public ActionResult<IEnumerable<Auteur>> List()
        {
            // récupérer les livres dans la base de donées pour qu'elle puisse être affiché


            List<Auteur> ListAuthors = this.auteurService.GetAllAuteurs();


            return View(ListAuthors);
        }


        [HttpDelete]
        public bool Delete([FromForm] int[] ids)
        {


            try
            {
                foreach (var id in ids)
                {
                    Console.WriteLine($"ID --_>{id}");
                    auteurService.DeleteAuteur(id);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Author Exception: {ex.Message}");
            }


            return true;

        }


        public ActionResult<Auteur> CreateUpdateView(int id = 0)
        {
            Auteur auteur = null;



            if (id > 0)
            {
                auteur = auteurService.GetAuteurById(id);
            }




            return View("Create", auteur);
        }


        [HttpPost]
        public IActionResult Create(int id, string name,int age)
        {

            if (id == 0)
            {
                Auteur auteur = new Auteur() { Name = name, Age = age };

                auteurService.AddAuteur(auteur);
            }
            else
            {
                Auteur auteur = auteurService.GetAuteurById(id);

                auteur.Name = name;
                auteur.Age = age;



                auteurService.UpdateAuteur(id, auteur);
            }


            return RedirectToAction("List");
        }


        public ActionResult<Auteur> View(int id = 0)
        {

            ViewBag.BooksTotal = auteurService.GetTotalBooksByAuteur(id);

            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            return View(auteurService.GetAuteurById(id));
        }



        public ActionResult<IEnumerable<Auteur>> Books(int id, [FromQuery] int page = 0)
        {
            // récupérer les livres dans la base de donées pour qu'elle puisse être affiché


            var limit = 5;
            var offset = limit * page;




            List<Book> ListBooks = this.bookService.GetBooksByAuteurService(limit: limit, offset: offset, auteurName: auteurService.GetAuteurById(id).Name);

           

            ViewBag.Limit = limit;
            ViewBag.BooksTotal = bookService.GetTotalBooksByAuteur(id);
            ViewBag.Pages = (int)Math.Ceiling((double)ViewBag.BooksTotal / limit);
            ViewBag.Page = page;
            ViewBag.Id = id;



            return View(ListBooks);
        }

    }
}

