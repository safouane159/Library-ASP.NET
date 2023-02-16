using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace ASP.Server.Database
{
    public class DbInitializer
    {
        public static void Initialize(LibraryDbContext bookDbContext)
        {

            if (bookDbContext.Books.Any())
                return;

            Genre SF, Classic, Romance, Thriller;
            bookDbContext.Genres.AddRange(
                new Genre { Label = "Science fiction"},
          //      Classic = new Genre{ },
        //        Romance = new Genre{ },
                 new Genre{ Label = "Dramas" }
            );
            bookDbContext.SaveChanges();

            // Une fois les moèles complété Vous pouvez faire directement
            // new Book() { Author = "xxx", Name = "yyy", Price = n.nnf, Content = "ccc", Genres = new() { Romance, Thriller } }
            bookDbContext.Books.AddRange(
                new Book { Titre = "48 law of power", Prix = 40} , 
                //new Book { titre = "48 law of power  ", prix = 40 },
              //  new Book { titre = "48 law of power  ", prix = 40 },
                new Book { Titre = "mastery  ", Prix = 29 }



            );
            // Vous pouvez initialiser la BDD ici

            bookDbContext.SaveChanges();
        }
    }
}