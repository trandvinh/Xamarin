using NoteVTranizer.Models;
using NoteVTranizer.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NoteVTranizer.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        string settingsId = string.Empty;
       
        public IList<SortByInfo> SortByInfoList { get; set; }
        public Command SaveCommand { get; }
        public Command ClearCommand { get; }
        public Command LoadSettingsCommand { get; }
        public async Task ExecuteLoadSettingsCommand()
        {
            IsBusy = true;

            try
            {
                int id = 1;
                //// Retrieve the note and set it as the BindingContext of the page.
                TheSettings = await App.SettingsDB.GetSettingsAsync(id);
                if (TheSettings == null)
                {
                    TheSettings = new Settings();
                }

                if (TheSettings != null)
                {
                    Email = TheSettings.Email;
                    List<SortByInfo> tempList = new List<SortByInfo>(SortByInfoList);
                    SortByInfo p = tempList.Find(x => x.ID == TheSettings.OrderByID);
                    if (p != null)
                    {
                        SelectSortByInfo = p;
                    }
                    //TheSettings.OrderByID = 1;
                    SortByInfo p2 = tempList.Find(x => x.ID == TheSettings.OrderByIDView);
                    if (p2 != null)
                    {
                        SelectSortByInfoView = p2;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Failed to load note settings. Exception error={0}.",ex.Message));
            }
        }
        private void CreateOrderByData()
        {
            SortByInfoList = new ObservableCollection<SortByInfo>();
            SortByInfoList.Add(new SortByInfo { ID = (int)NoteSortByEnum.PRIORITY, Name = "Priority (High-Low)" });
            SortByInfoList.Add(new SortByInfo { ID = (int)NoteSortByEnum.EXPIRED_DATE, Name = "Expiration Date" });
            SortByInfoList.Add(new SortByInfo { ID = (int)NoteSortByEnum.CREATED_DATE, Name = "Created Date" });
        }
       
        public SettingsViewModel()
        {
            
            SaveCommand = new Command(SaveSettings, ValidateSave);
            ClearCommand = new Command(ClearSettings);
            LoadSettingsCommand = new Command(async () => await ExecuteLoadSettingsCommand());
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
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
            get
            {
                string str = string.Empty;
                if (TheSettings != null)
                {
                    str = TheSettings.Email;
                }
                return str;
            }
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
        SortByInfo _selectSortByInfoView { get; set; }
        public SortByInfo SelectSortByInfoView
        {
            get { return _selectSortByInfoView; }
            set
            {
                if (SelectSortByInfoView != value)
                {
                    _selectSortByInfoView = value;
                    OnPropertyChanged();
                }
            }
        }
        public async void LoadSettings(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                //// Retrieve the note and set it as the BindingContext of the page.
                TheSettings = await App.SettingsDB.GetSettingsAsync(id);
                if (TheSettings == null)
                {
                    TheSettings = new Settings();
                }
                
                if (TheSettings != null)
                {
                    Email = TheSettings.Email;
                    List<SortByInfo> tempList = new List<SortByInfo>(SortByInfoList);
                    //TheSettings.OrderByID = 1;
                    SortByInfo p = tempList.Find(x => x.ID == TheSettings.OrderByID);
                    if (p != null)
                    {
                        SelectSortByInfo = p;
                    }

                    SortByInfo p2 = tempList.Find(x => x.ID == TheSettings.OrderByIDView);
                    if (p2 != null)
                    {
                        SelectSortByInfoView = p2;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Failed to load note settings. Exception error={0}.", ex.Message));
            }
        }      
        private async void SaveSettings()
        {

            if ((SelectSortByInfo != null) && (TheSettings != null))
            {              
                var settings = TheSettings;
                settings.OrderByID = SelectSortByInfo.ID;
                settings.OrderByIDView = SelectSortByInfoView.ID;
                await App.SettingsDB.SaveSettingsAsync(settings);
                await App.Current.MainPage.DisplayAlert("Info", "Settings saved", "OK");
                // Navigate backwards
                await Shell.Current.GoToAsync("..");
                //await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new NotesPage());
            }


        }
        private void ClearSettings()
        {
            if (TheSettings != null)
            {
                TheSettings.Date = DateTime.MinValue;
                TheSettings.Email = String.Empty;
                List<SortByInfo> tempList = new List<SortByInfo>(SortByInfoList);
                SelectSortByInfo = null;
                SelectSortByInfoView = null;
                OnPropertyChanged("SortByInfoName");
                OnPropertyChanged("Email");
            }
           // Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new NotesPage());
        }
    }
}
