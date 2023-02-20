using CommunityToolkit.Mvvm.DependencyInjection;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WPF.Reader.Model;
using WPF.Reader.Service;

namespace WPF.Reader.ViewModel
{
    internal class ListBook : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ItemSelectedCommand { get; set; }

        // n'oublier pas faire de faire le binding dans ListBook.xaml !!!!
        public ObservableCollection<BookDTO> Books => Ioc.Default.GetRequiredService<LibraryService>().Books;

        public ObservableCollection<Genre> Genres => Ioc.Default.GetRequiredService<LibraryService>().Genres;

        private BookDTO _selectedBook;

        public BookDTO SelectedBook
        {
            get { return _selectedBook; }
            set
            {
                _selectedBook = value;
                
                /*foreach (Genre genre in Genres)
                {
                    _selectedBook.Genres.Add(genre);
                }
                */
                // Ajoutez le code ici pour effectuer une action lorsqu'un livre est sélectionné
                Ioc.Default.GetService<INavigationService>().Navigate<DetailsBook>(_selectedBook);

                //MessageBox.Show($"- Book ID: {_selectedBook.Id}, Titre: {_selectedBook.Titre}, Prix: {_selectedBook.Prix}, Genre: '{_selectedBook.Genres[0]}");
            }
        }

        public ListBook()
        {
            ItemSelectedCommand = new RelayCommand(book => { /* the livre devrais etre dans la variable book */
                Ioc.Default.GetService<INavigationService>().Navigate<DetailsBook>(book);
            });
        }
    }
}
