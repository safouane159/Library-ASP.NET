using CommunityToolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF.Reader.Model;
using WPF.Reader.Service;

namespace WPF.Reader.ViewModel
{
    internal class ListBook : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ItemSelectedCommand { get; set; }
        public ICommand PrevPageCommand { get; set; }
        public ICommand NextPageCommand { get; set; }

        // n'oublier pas faire de faire le binding dans ListBook.xaml !!!!
        public ObservableCollection<BookDTO> Books => Ioc.Default.GetRequiredService<LibraryService>().Books;

        public ObservableCollection<Genre> Genres => Ioc.Default.GetRequiredService<LibraryService>().Genres;

        public int ListBookSize => Ioc.Default.GetRequiredService<LibraryService>().ListBooksSize;

        public List<Genre> InitGenres
        {
            get
            {
                return new() { new Genre() { Id=-1, Label="--Select--" } };
            }
        }

        private BookDTO _selectedBook;

        private Genre _selectedGenre;

        private int _pageSize = 5;
        private int _currentPage = 1;
        private int _totalPages;
        public ObservableCollection<BookDTO> DisplayedBooks { get; private set; }



        public BookDTO SelectedBook
        {
            get { return _selectedBook; }
            set
            {
                _selectedBook = value;

                //var genreIds = value.Genres.Select(genre => genre.Id).ToList();

                
                // Ajoutez le code ici pour effectuer une action lorsqu'un livre est sélectionné
                //Ioc.Default.GetService<INavigationService>().Navigate<DetailsBook>(_selectedBook);

                //MessageBox.Show($"- Book ID: {_selectedBook.Id}, Titre: {_selectedBook.Titre}, Prix: {_selectedBook.Prix}, Genre: '{_selectedBook.Genres[0]}");
            }
        }

        public Genre SelectedGenre
        {
            get { return _selectedGenre; }

            set
            {
                _selectedGenre = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedGenre)));

                Ioc.Default.GetRequiredService<LibraryService>().getBooksByGenre(_selectedGenre.Label);
                //var genreIds = value.Genres.Select(genre => genre.Id).ToList();


                // Ajoutez le code ici pour effectuer une action lorsqu'un livre est sélectionné
                //Ioc.Default.GetService<INavigationService>().Navigate<ListBook>();

                //MessageBox.Show($"- Book ID: {_selectedBook.Id}, Titre: {_selectedBook.Titre}, Prix: {_selectedBook.Prix}, Genre: '{_selectedBook.Genres[0]}");
            }
        }

        public ListBook()
        {
            ItemSelectedCommand = new RelayCommand(book => {
                /* the livre devrais etre dans la variable book */
                //var selectedBook = (SelectionChangedEventArgs)book;
                Ioc.Default.GetService<INavigationService>().Navigate<DetailsBook>(SelectedBook);
            });

            // To display the first element of combobox "--Select--"
            SelectedGenre = InitGenres.First();

            DisplayedBooks = new ObservableCollection<BookDTO>(Books.Take(_pageSize));

            PrevPageCommand = new RelayCommand(_ => {
                _currentPage--;
                var nbPages = (ListBookSize/5) + ((ListBookSize%5) > 0 ? 1 : 0);
                
                if (_currentPage > 0)
                {
                    var offset = (_currentPage-1)*5;
                    Ioc.Default.GetRequiredService<LibraryService>().getAllBooks(offset: offset);
                }
                else
                {
                    _currentPage++;
                }
               

            });

            NextPageCommand = new RelayCommand(book => {
                _currentPage++;
                var nbPages = (ListBookSize/5) + ( (ListBookSize%5) > 0 ? 1 : 0) ;


                
                if ( _currentPage <= nbPages  )
                {
                    var offset = (_currentPage-1)*5;
                    Ioc.Default.GetRequiredService<LibraryService>().getAllBooks(offset: offset);

                }
                else
                {
                    _currentPage--;
                }
                
            });
        }
    }
}
