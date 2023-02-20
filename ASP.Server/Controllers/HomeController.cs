using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ASP.Server.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ASP.Server.Service;

namespace ASP.Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BookService bookService;
        private GenreService genreService;
        private AuteurService auteurService;

        public HomeController(ILogger<HomeController> logger, BookService bookService, GenreService genreService, AuteurService auteurService)
        {
            _logger = logger;
            this.genreService = genreService;
            this.bookService = bookService;
            this.auteurService = auteurService;
        }

        public IActionResult Index()
        {

            ViewBag.TotalBooks = bookService.GetTotalBooks();
            ViewBag.TotalAuthors = auteurService.GetAllAuteurs().Count;
            ViewBag.TotalCategories = genreService.GetGenres().Count;
            ViewBag.TotalProfit = bookService.GetBooks().Sum(book => book.Prix);



            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
