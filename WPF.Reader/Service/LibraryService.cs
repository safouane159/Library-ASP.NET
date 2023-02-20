using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using WPF.Reader.Model;
using WPF.Reader.Api;
using System.Linq;
using System.Windows;
using System.Threading.Tasks;

namespace WPF.Reader.Service
{
    public class LibraryService
    {
        // A remplacer avec vos propre données !!!!!!!!!!!!!!
        // Pensé qu'il ne faut mieux ne pas réaffecter la variable Books, mais juste lui ajouter et / ou enlever des éléments
        // Donc pas de LibraryService.Instance.Books = ...
        // mais plutot LibraryService.Instance.Books.Add(...)
        // ou LibraryService.Instance.Books.Clear()
        public ObservableCollection<BookDTO> Books { get; set; } = new ObservableCollection<BookDTO>();


        public ObservableCollection<Genre> Genres { get; set; } = new ObservableCollection<Genre>();

        public int ListBooksSize = 0;


        // C'est aussi ici que vous ajouterez les requête réseau pour récupérer les livres depuis le web service que vous avez fait
        // Vous pourrez alors ajouter les livres obtenu a la variable Books !
        // Faite bien attention a ce que votre requête réseau ne bloque pas l'interface 

        public LibraryService()
        {
            

            // retrieves Books and Genres with API
            //getAllBooks();
            new Task(() => getAllBooks()).Start(); 
            //getGenres();
            new Task(() => getGenres()).Start();

        }
        public async void getAllBooks(int limit = 5, int offset = 0)
        {
            var dataApi = await new BookApi().BookGetBooksWithHttpInfoAsync();
            ListBooksSize = dataApi.Data.Count;
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.Books.Clear();
            });

            dataApi = await new BookApi().BookGetBooksWithHttpInfoAsync(limit, offset);
            Application.Current.Dispatcher.Invoke(() => {
                foreach (BookDTO book in dataApi.Data)
                {
                    this.Books.Add(book);
                }
            });  
        }

        public async void getGenres()
        {

            Application.Current.Dispatcher.Invoke(() =>
            {
                this.Genres.Clear();
            });

            var dataApi = await new GenreApi().GenreGetGenresWithHttpInfoAsync();
            Application.Current.Dispatcher.Invoke(() => {
                foreach (Genre genre in dataApi.Data)
                {
                    this.Genres.Add(genre);
                }
            });
        }

        public Book getBook(int id)
        {
            if (id == 0) return null;
            var book = new BookApi().BookGetBook(id);
            return book;
        }

        public void getBooksByGenre(string genre)
        {
            var dataApi = new BookApi().BookGetBooksByGenre(label: genre);

            if (dataApi != null && genre != "--Select--")
            {
                this.Books.Clear();

                foreach (BookDTO book in dataApi)
                {
                    this.Books.Add(book);
                }
                ListBooksSize = this.Books.Count;
            }
            else
            {
                this.getAllBooks();
            }

        }

    }
}
