using CommunityToolkit.Mvvm.DependencyInjection;
using System.ComponentModel;
using System.Windows.Input;
using WPF.Reader.Model;
using WPF.Reader.Service;

namespace WPF.Reader.ViewModel
{
    public class DetailsBook : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ReadCommand { get; init; } = new RelayCommand(x => { /* A vous de définir la commande */ });

        // n'oublier pas faire de faire le binding dans DetailsBook.xaml !!!!
        public BookDTO CurrentBook { get; init; }

        public DetailsBook(BookDTO book)
        {
            CurrentBook = book;
            ReadCommand = new RelayCommand(x => {
                var curr = Ioc.Default.GetRequiredService<LibraryService>().getBook(CurrentBook.Id);
                Ioc.Default.GetRequiredService<INavigationService>().Navigate<ReadBook>(curr);
            });
        }
    }

    /* Cette classe sert juste a afficher des donnée de test dans le designer */
    public class InDesignDetailsBook : DetailsBook
    {
        public InDesignDetailsBook() : base(new BookDTO() /*{ Title = "Test Book" }*/) { }
    }
}
