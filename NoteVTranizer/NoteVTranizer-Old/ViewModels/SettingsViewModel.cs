using NoteVTranizer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace NoteVTranizer.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        string settingsId = string.Empty;
        //public string SettingId
        //{
        //    get => noteId;
        //    set
        //    {
        //        noteId = value;            
        //        OnPropertyChanged();
        //    }
        //}
        public IList<SortByInfo> SortByInfoList { get; set; }
        

        private void CreateOrderByData()
        {
            SortByInfoList = new ObservableCollection<SortByInfo>();
            SortByInfoList.Add(new SortByInfo { ID = 1, Name = "Priority" });
            SortByInfoList.Add(new SortByInfo { ID = 2, Name = "Expiration Date" });
            SortByInfoList.Add(new SortByInfo { ID = 3, Name = "Created Date" });
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
        public SettingsViewModel(string id)
        {
            settingsId = id;
            if (String.IsNullOrEmpty(id))
            {
                TheSettings = new Settings();
            }
            //else
            //{
            //    LoadSettings(id);
            //}
            //SaveCommand = new Command(SaveNote, ValidateSave);
            //DeleteCommand = new Command(DeleteNote);
            //this.PropertyChanged +=
            //    (_, __) => SaveCommand.ChangeCanExecute();
            //this.PropertyChanged +=
            //   (_, __) => DeleteCommand.ChangeCanExecute();
            CreateOrderByData();
           
        }
        public bool ValidateSave()
        {
            return true;
            //return (TheSettings != null) && (SelectPriority!= null) && !String.IsNullOrEmpty(TheSettings.Text);
        }
        public bool ValidateDelete()
        {
            return true;
            //return !String.IsNullOrEmpty(noteId);
        }

        string email;
        public string Email
        {
            get => TheSettings.Email;
            set
            {
                if (TheSettings != null)
                {
                    email = TheSettings.Email = value;
                    OnPropertyChanged();
                }
            }
        }

        string sortByInfoName;
        public string SortByInfoName
        {
            get => SelectSortByInfo.Name;
            set
            {
                if (SelectSortByInfo != null)
                {
                    sortByInfoName = SelectSortByInfo.Name = value;
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
        Settings theSettings;
        public Settings TheSettings
        {
            get => theSettings;
            set
            {
                theSettings = value;
                OnPropertyChanged();
            }
        }
        SortByInfo _selectSortByInfo { get; set; }
        public SortByInfo SelectSortByInfo
        {
            get { return _selectSortByInfo; }
            set
            {
                if (SelectSortByInfo != value)
                {
                    _selectSortByInfo = value;
                    OnPropertyChanged();
                }
            }
        }
        public Command SaveCommand { get; }
        public Command DeleteCommand { get; }

        public async void LoadSettings(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                //// Retrieve the note and set it as the BindingContext of the page.
                theSettings = await App.SettingsDB.GetSettingsAsync(id);
                if (theSettings == null)
                {
                    theSettings = new Settings();
                }
                
                if (theSettings != null)
                {
                    Email = theSettings.Email;
                    List<SortByInfo> tempList = new List<SortByInfo>(SortByInfoList);
                    //theSettings.OrderByID = 1;
                    //SortByInfo p = tempList.Find(x => x.ID == theSettings.OrderByID);
                    //if (p != null)
                    //{
                    //    SelectSortByInfo = p;
                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load note settings.");
            }
        }
        //public async void LoadNote(string itemId)
        //{
        //    try
        //    {
        //        int id = Convert.ToInt32(itemId);
        //        NoteId = itemId;
        //        IsDeleteVisible = !String.IsNullOrEmpty(NoteId);
        //        // Retrieve the note and set it as the BindingContext of the page.
        //        TheNode = await App.NoteDB.GetNoteAsync(id);
        //        if (TheNode != null)
        //        {
        //            Text = TheNode.Text;
        //            List<Priority> tempList = new List<Priority>(OrderByList);
        //            Priority p = tempList.Find(x => x.ID == (int)TheNode.Priority);
        //            if (p != null) {
        //                SelectPriority= p;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine("Failed to load note.");
        //    }
        //}
       
        private async void SaveSettings()
        {
           
            //if ((SelectPriority != null) && (TheNode != null))
            //{
            //    //OnPropertyChanged(nameof(CityText));
            //    //CityText += DateTime.Now.ToString("G");
            //    var priorityName = SelectPriority.PriorityName;
            //    //var note = (Note)BindingContext;
            //    var note = TheNode;
            //    note.CreatedDate = DateTime.UtcNow;
            //    note.Priority = (NotePriorityEnum) SelectPriority.ID;
            //    if (!string.IsNullOrWhiteSpace(note.Text))
            //    {
            //        await App.NoteDB.SaveNoteAsync(TheNode);
            //    }

            //    // Navigate backwards
            //    await Shell.Current.GoToAsync("..");
            //}
        }
        //private async void DeleteNote()
        //{
        //    if (IsDeleteVisible)
        //    {
        //        if (TheNode != null)
        //        {
        //            await App.NoteDB.DeleteNoteAsync(TheNode);

        //            // Navigate backwards
        //            await Shell.Current.GoToAsync("..");
        //        }
        //    }
        //    else
        //    {
        //        await Shell.Current.GoToAsync("..");
        //    }
        //}
    }
}
