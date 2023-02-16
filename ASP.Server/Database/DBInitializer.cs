using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ASP.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Reflection.Emit;
using System.Collections;

namespace ASP.Server.Database
{
    public class DbInitializer
    {
        public static void Initialize(LibraryDbContext bookDbContext)
        {

            if (bookDbContext.Books.Any())
                return;

            Genre SF, Classic, Romance, Thriller;

            SF = new Genre { Label = "Science fiction" };
            Classic = new Genre { Label = "Classic" };
            Romance = new Genre { Label = "Romance" };
            Thriller = new Genre { Label = "Thriller" };


            bookDbContext.Genres.AddRange(
               SF, Classic, Romance, Thriller
            );
            bookDbContext.SaveChanges();






            // Une fois les moèles complété Vous pouvez faire directement
            // new Book() { Author = "xxx", Name = "yyy", Price = n.nnf, Content = "ccc", Genres = new() { Romance, Thriller } }
            ICollection<Genre> genres = new List<Genre> { new Genre { Label = "Dramas" }, new Genre { Label = "Science fiction" } };



            bookDbContext.Books.AddRange(
                new Book { Titre = "48 law of power", Prix = 40, Genres = new List<Genre> { SF, Thriller , Romance} } , 
                new Book { Titre = "mastery  ", Prix = 29, Genres = new List<Genre> { Romance, Thriller } },
                new Book(){ Auteur = "xxx", Titre = "yyy", Prix = 20.99, Contenu = "« Ghislain Gilberti est un écrivain stratosphérique : bienvenue dans le monde maléfique de l'un des plus grands auteurs de polars contemporains. »Gérard Collard, La Griffe noireAu commencement, il y eut un enfant. Le petit Gabin Schwartz. Six ans. Son corps retrouvé dans un parc. Exsangue. Puis ce fut un agriculteur. Enterré vivant. Son index désignant le ciel. Puis un marchand ambulant, écrasé sous son stock.\nSale baptême du feu pour Seth Kohl, le chef du groupe chargé de l'enquête à la Brigade criminelle du SRPJ de Versailles. Comment avancer quand rien ne relie les victimes entre elles ? Alors que les corps s'accumulent, un lien se dessine enfin, inattendu, fragile et incomplet : le tueur pourrait bien s'inspirer des Danses macabres, ces fresques que l'on retrouve dans les vieilles églises, ou dans les bibliothèques des collectionneurs.\nMais chaque série de tableaux est différente. Laquelle est la bonne ? Le temps presse, et Seth Kohl est assailli par ses propres démons, qui l'invitent eux aussi à quelques pas de danse avec la mort", Genres = new List<Genre> {SF,Classic} }



            );
            // Vous pouvez initialiser la BDD ici

            bookDbContext.SaveChanges();
        }
    }
}