using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using WPF.Reader.Model;

namespace WPF.Reader.Service
{
    public class LibraryService
    {
        // A remplacer avec vos propre données !!!!!!!!!!!!!!
        // Pensé qu'il ne faut mieux ne pas réaffecter la variable Books, mais juste lui ajouter et / ou enlever des éléments
        // Donc pas de LibraryService.Instance.Books = ...
        // mais plutot LibraryService.Instance.Books.Add(...)
        // ou LibraryService.Instance.Books.Clear()
        public ObservableCollection<Book> Books { get; set; }


        public ObservableCollection<Genre> Genres { get; set; }

        // C'est aussi ici que vous ajouterez les requête réseau pour récupérer les livres depuis le web service que vous avez fait
        // Vous pourrez alors ajouter les livres obtenu a la variable Books !
        // Faite bien attention a ce que votre requête réseau ne bloque pas l'interface 

         public LibraryService(){
            Genres = new ObservableCollection<Genre>() {

             new Genre { Label = "Science fiction" },
             new Genre { Label = "Classic" },
             new Genre { Label = "Romance" },
             new Genre { Label = "Thriller" },
             new Genre { Label = "Action" },
             new Genre { Label = "Adventure" },
             new Genre { Label = "Dystopian" },
             new Genre { Label = "Philosophy" },
             new Genre { Label = "Psychology" },
             new Genre { Label = "Literature" },
             new Genre { Label = "Humor" },
             new Genre { Label = "Mystery" },
             new Genre { Label = "Coming-of-age" },
             new Genre { Label = "Fiction" },
             new Genre { Label = "Inspirational" }
        };
            Books=new ObservableCollection<Book>();
            var books = GetAllBooks();
            foreach(var book in books) Books.Add(book);

        Books=new ObservableCollection<Book>() {
           new Book { Titre = "48 law of power",
                      Prix = 40,
                      Auteur = "Robert Green",
                      Contenu = " honoring the Medicis' greatness. Shortly after the discovery",
                      Genres=new List<Genre> () { Genres[0] }
                    },




            new Book { Titre = "Pride and Prejudice",
                       Prix = 20,
                       Auteur = "Jane Austen",
                       Contenu = "for so many centuries, that it is truly ",
                       Genres=new List<Genre> () { Genres[0] }
                     },




            new Book { Titre = "1984",
                       Prix = 15,
                       Auteur = "George Orwell",
                       Contenu = " Over several weeks, Ninon de Lenclos hinkers, and politicians",
                        Genres=new List<Genre> () { Genres[0] }
                     },

          };
        } 
        public List<Book> GetAllBooks()
        {
            return new List<Book> { new Book { } };
        }
    }
}
