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
                    auteurService.DeleteAuteur(id);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }


            return true;

        }



    }
}

