using NoteVTranizer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace NoteVTranizer.ViewModels
{
    public class NoteListViewModel : BaseViewModel
    {
        public ObservableCollection<Note> NoteList { get; set; }


        public ICommand LoadCommand { get; protected set; }
        public NoteListViewModel()
        {
            //LoadCommand = new ICommand (async () => 
            //var tempNoteList = await App.NoteDB.GetNotesAsync();
            LoadCommand = new Command(async () =>
            {
                var result = await App.NoteDB.GetNotesAsync();
                NoteList = new ObservableCollection<Note>(result);
            });

            LoadCommand.Execute(null);
        }
    }
}
