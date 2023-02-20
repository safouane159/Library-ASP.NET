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
    public class GenreController : Controller
    {
        private readonly LibraryDbContext libraryDbContext;
        private GenreService genreService;


        public GenreController(LibraryDbContext libraryDbContext, GenreService genreService)
        {
            this.libraryDbContext = libraryDbContext;
            this.genreService = genreService;
        }

        // A vous de faire comme BookController.List mais pour les genres !

        public ActionResult<IEnumerable<Genre>> List()
        {
            // récupérer les livres dans la base de donées pour qu'elle puisse être affiché


            List<Genre> ListGenres = this.genreService.GetGenres();


            return View(ListGenres);
        }



        [HttpDelete]
        public bool Delete([FromForm] int[] ids)
        {

            try
            {
                foreach (var id in ids)
                {
                    genreService.DeleteGenre(id);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }


            return true;

        }


        public ActionResult<Genre> CreateUpdateView(int id = 0)
        {
            Genre genre = null;

          

            if (id > 0)
            {
                genre = genreService.GetGenreById(id);
            }

          


            return View("Create", genre);
        }



        [HttpPost]
        public IActionResult Create(int id, string label)
        {




       



            if (id == 0)
            {
                Genre genre = new Genre() {Label = label};

                genreService.AddGenre(genre);
            }
            else
            {
                Genre genre = genreService.GetGenreById(id);

                genre.Label = label;


                genreService.UpdateGenre(id, genre);
            }


            return RedirectToAction("List");
        }



        public ActionResult<Genre> View(int id = 0)
        {

            // Il faut interoger la base pour récupérer tous les genres, pour que l'utilisateur puisse les slécétionné
            return View(genreService.GetGenreById(id));
        }



    }
}
