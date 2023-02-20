﻿using System.ComponentModel;
using System.Speech.Synthesis;
using System.Windows.Input;
using WPF.Reader.Model;

namespace WPF.Reader.ViewModel
{
    class ReadBook : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        // A vous de jouer maintenant
        public Book CurrentBook { get; init; }

        public ReadBook(Book book)
        {
            CurrentBook= book;
        }

    }

    /* Cette classe sert juste a afficher des donnée de test dans le designer */
    class InDesignReadBook : ReadBook
    {
        public InDesignReadBook() : base(new Book())
        {
        }
    }
}
