using NoteVTranizer.Models;
using NoteVTranizer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace NoteVTranizer.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class NoteEntryViewModel : BaseViewModel
    {
        private string itemId;
        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                NoteId = itemId = value;
                LoadNoteId(value);
            }
        }
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
       

        private void CreatePriorityData()
        {
            PriorityDataService priorityDataSvc = new PriorityDataService();
            PriorityList = priorityDataSvc.GetPriorityList();
        }
        //public async void LoadNote(string itemId)
        //{
        //    try
        //    {
        //        int id = Convert.ToInt32(noteId);
        //        IsDeleteVisible = !String.IsNullOrEmpty(noteId);
        //        // Retrieve the note and set it as the BindingContext of the page.
        //        TheNode = await App.NoteDB.GetNoteAsync(id);
        //        if (TheNode != null)
        //        {
        //            Text = TheNode.Text;
        //            List<Priority> tempList = new List<Priority>(PriorityList);
        //            if (TheNode.ExpiredDate == DateTime.MinValue)
        //            {
        //                //SelectDueDate = DateTime.Now;
        //                TheNode.ExpiredDate = DateTime.Now;
        //                //OnPropertyChanged("TheNode");
        //            }

        //            Priority p = tempList.Find(x => x.ID == (int)TheNode.Priority);
        //            if (p != null)
        //            {
        //                SelectPriority = p;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine("Failed to load note.");
        //    }
        //}
        public async void LoadNoteId(string itemId)
        {
            try
            {
                if (!String.IsNullOrEmpty(itemId))
                {
                    Title = "Update/Delete Note";
                    IsDeleteVisible = !String.IsNullOrEmpty(noteId);
                    TheNote = await App.NoteDB.GetNoteAsync(Convert.ToInt32(noteId));
                    if (TheNote != null)
                    {
                        Text = TheNote.Text;
                        if (TheNote.ExpiredDate == null || TheNote.ExpiredDate == DateTime.MinValue)
                        {
                            TheNote.ExpiredDate = DateTime.Now;
                        }
                        //Description = note.CreatedDate.ToString("G");
                        List<Priority> tempList = new List<Priority>(PriorityList);
                        Priority p = tempList.Find(x => x.ID == (int)TheNote.Priority);
                        if (p != null)
                        {
                            SelectPriority = p;
                        }
                    }
                }
                else
                {
                    Title = "Create Note";
                    TheNote = new Note();
                    TheNote.UseEmail = true;
                    TheNote.ExpiredDate = DateTime.Now.AddDays(7);
                    Priority p1 = new Priority { ID = 2, PriorityName = "Medium" };//Can't use new Priority to select. ?????
                    ////SelectPriority = p;
                    ////electPriority.ID = (int)NotePriorityEnum.MEDIUM;
                    TheNote.Priority = NotePriorityEnum.MEDIUM;
                    List<Priority> tempList = new List<Priority>(PriorityList);
                    Priority p = tempList.Find(x => x.ID == (int)NotePriorityEnum.MEDIUM);
                    if (p != null)
                    {
                        SelectPriority = p;
                    }
                    OnPropertyChanged("TheNote");
                    //OnPropertyChanged("SelectPriority");
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Note");
            }
        }
        //public NoteEntryViewModel(string id)
        //{
        //    noteId = id;
        //    if (String.IsNullOrEmpty(id))
        //    {
        //        TheNode = new Note();
        //        TheNode.ExpiredDate = null;
        //    }
        //    SaveCommand = new Command(SaveNote, ValidateSave);
        //    DeleteCommand = new Command(DeleteNote);
        //    this.PropertyChanged +=
        //        (_, __) => SaveCommand.ChangeCanExecute();
        //    this.PropertyChanged +=
        //       (_, __) => DeleteCommand.ChangeCanExecute();
        //    CreatePriorityData();
        //}
        public NoteEntryViewModel()
        {
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
            bool canSave = (TheNote != null) && (SelectPriority != null) && !String.IsNullOrEmpty(TheNote.Text);

            return canSave;
        }
        public bool ValidateDelete()
        {

            return !String.IsNullOrEmpty(noteId);
        }

        string text;
        public string Text
        {
            get
            {
                string str = string.Empty;
                if (TheNote != null)
                {
                    str = TheNote.Text;
                }
                return str;
            }
            set
            {
                if (TheNote != null)
                {
                    text=TheNote.Text = value;
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
        public Note TheNote
        {
            get => theNote;
            set
            {
                theNote = value;
                OnPropertyChanged();
            }
        }
        public Priority _selectedPriority { get; set; }
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
        public DateTime _selectDueDate { get; set; }
        public DateTime SelectDueDate
        {
            get { return _selectDueDate; }
            set
            {
                if (SelectDueDate != value)
                {
                    _selectDueDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public Command SaveCommand { get; }
        public Command DeleteCommand { get; }   
        private async void SaveNote()
        {
           
            if ((SelectPriority != null) && (TheNote != null))
            {
                var priorityName = SelectPriority.PriorityName;
                //var note = (Note)BindingContext;
                var note = TheNote;
                note.CreatedDate = DateTime.Now;
                note.Priority = (NotePriorityEnum) SelectPriority.ID;
                if (TheNote.ExpiredDate == null || TheNote.ExpiredDate == DateTime.MinValue)
                {
                    note.ExpiredDate = DateTime.Now;
                }
                if (!string.IsNullOrWhiteSpace(note.Text))
                {
                    await App.NoteDB.SaveNoteAsync(TheNote);
                }

                // Navigate backwards
                await Shell.Current.GoToAsync("..");
            }
        }
        private async void DeleteNote()
        {
            //The button can be Delete/Cancel
            //If not Delete, the action will be Cancel.
            if (IsDeleteVisible)
            {
                if (TheNote != null)
                {
                    var respond = await App.Current.MainPage.DisplayAlert("Warning", "Are you sure to delete?", "Yes", "No");
                    if (respond == true)
                    {
                        await App.NoteDB.DeleteNoteAsync(TheNote);
                        // Navigate backwards
                        await Shell.Current.GoToAsync("..");
                    }
                }
            }
            else
            {
                await Shell.Current.GoToAsync(".."); //Cancel, go back to note list.
            }
        }
    }
}
