using NoteVTranizer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace NoteVTranizer.ViewModels
{
    public class NoteEntyViewModel : INotifyPropertyChanged
    {
        //ObservableCollection
        public NoteEntyViewModel()
        {
        }
        public event PropertyChangedEventHandler PropertyChanged;

        Note theNote;
        public Note TheNode
        {
            get => theNote;
            set
            {
                theNote = value;

                var args = new PropertyChangedEventArgs(nameof(TheNode));
                PropertyChanged?.Invoke(this, args);
            }
        }
        public Command SaveCommand { get; }
        public Command DeleteCommand { get; }
    }
}
