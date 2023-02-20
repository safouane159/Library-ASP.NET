using CommunityToolkit.Mvvm.DependencyInjection;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WPF.Reader.Model;
using WPF.Reader.Service;

namespace WPF.Reader.ViewModel
{
    public class ListGenre : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ItemSelectedCommand { get; set; }

        // n'oublier pas faire de faire le binding dans ListBook.xaml !!!!
        public ObservableCollection<Genre> Genres => Ioc.Default.GetRequiredService<LibraryService>().Genres;

        public ListGenre()
        {
            ItemSelectedCommand = new RelayCommand(genre => { /* the livre devrais etre dans la variable book */ });
        }
    }
}
