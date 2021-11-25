using NoteVTranizer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace NoteVTranizer.ViewModels
{
    public class NoteEntryViewModel : BaseViewModel
    {
        string noteId = string.Empty;
        public string NoteId
        {
            get => noteId;
            set
            {
                noteId = value;            
                OnPropertyChanged();
            }
        }
        public IList<Priority> PriorityList { get; set; }
        public Priority _selectedPriority { get; set; }

        private void CreatePriorityData()
        {
            PriorityList = new ObservableCollection<Priority>();
            PriorityList.Add(new Priority { ID = 1, PriorityName = "Low" });
            PriorityList.Add(new Priority { ID = 2, PriorityName = "Medium" });
            PriorityList.Add(new Priority { ID = 3, PriorityName = "High" });
        }
        //public NoteEntryViewModel()
        //{
        //    SaveCommand = new Command(SaveNote, ValidateSave);
        //    DeleteCommand = new Command(DeleteNote);
        //    this.PropertyChanged +=
        //        (_, __) => SaveCommand.ChangeCanExecute();
        //    CreatePriorityData();
        //}
        //ObservableCollection
        public NoteEntryViewModel(string id)
        {
            noteId = id;
            if (String.IsNullOrEmpty(id))
            {
                TheNode = new Note();
            }
            SaveCommand = new Command(SaveNote, ValidateSave);
            DeleteCommand = new Command(DeleteNote);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            this.PropertyChanged +=
               (_, __) => DeleteCommand.ChangeCanExecute();
            CreatePriorityData();
           
        }
        public bool ValidateSave()
        {

            return (TheNode != null) && (SelectPriority!= null) && !String.IsNullOrEmpty(TheNode.Text);
        }
        public bool ValidateDelete()
        {

            return !String.IsNullOrEmpty(noteId);
        }

        string text;
        public string Text
        {
            get => TheNode.Text;
            set
            {
                if (TheNode != null)
                {
                    text=TheNode.Text = value;
                    OnPropertyChanged();
                }
            }
        }
       
        private bool _isDeleteVisible;
        public bool IsDeleteVisible
        {
            get
            {
                return _isDeleteVisible;
            }
            set
            {
                _isDeleteVisible = value;
                OnPropertyChanged();
            }
        }
        Note theNote;
        public Note TheNode
        {
            get => theNote;
            set
            {
                theNote = value;
                OnPropertyChanged();
            }
        }
        public Priority SelectPriority
        {
            get { return _selectedPriority; }
            set
            {
                if (SelectPriority != value)
                {
                    _selectedPriority = value;
                    OnPropertyChanged();
                }
            }
        }
        public Command SaveCommand { get; }
        public Command DeleteCommand { get; }
        public async void LoadNote(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                NoteId = itemId;
                IsDeleteVisible = !String.IsNullOrEmpty(NoteId);
                // Retrieve the note and set it as the BindingContext of the page.
                TheNode = await App.NoteDB.GetNoteAsync(id);
                if (TheNode != null)
                {
                    Text = TheNode.Text;
                    List<Priority> tempList = new List<Priority>(PriorityList);
                    Priority p = tempList.Find(x => x.ID == (int)TheNode.Priority);
                    if (p != null) {
                        SelectPriority= p;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load note.");
            }
        }
        //private async void OnSave()
        //{
        //    Item newItem = new Item()
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        Text = Text,
        //        Description = Description
        //    };

        //    await DataStore.AddItemAsync(newItem);

        //    // This will pop the current page off the navigation stack
        //    await Shell.Current.GoToAsync("..");
        //}
        private async void SaveNote()
        {
           
            if ((SelectPriority != null) && (TheNode != null))
            {
                //OnPropertyChanged(nameof(CityText));
                //CityText += DateTime.Now.ToString("G");
                var priorityName = SelectPriority.PriorityName;
                //var note = (Note)BindingContext;
                var note = TheNode;
                note.CreatedDate = DateTime.UtcNow;
                note.Priority = (NotePriorityEnum) SelectPriority.ID;
                if (!string.IsNullOrWhiteSpace(note.Text))
                {
                    await App.NoteDB.SaveNoteAsync(TheNode);
                }

                // Navigate backwards
                await Shell.Current.GoToAsync("..");
            }
        }
        private async void DeleteNote()
        {
            if (IsDeleteVisible)
            {
                if (TheNode != null)
                {
                    await App.NoteDB.DeleteNoteAsync(TheNode);

                    // Navigate backwards
                    await Shell.Current.GoToAsync("..");
                }
            }
            else
            {
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
