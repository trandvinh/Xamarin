using NoteVTranizer.Models;
using NoteVTranizer.Services;
using NoteVTranizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteVTranizer.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public List<Priority> PriorityList = new List<Priority>();
        public SettingsPage()
        {
            InitializeComponent();
            //BindingContext = new Settings();
            //BindingContext = new PriorityViewModel();
            //LoadSettings("1");
            //BindingContext = this;
            //LoadPriority();
            PriorityPicker.BindingContext = new PriorityViewModel();
            
        }
        async void LoadSettings(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                //// Retrieve the note and set it as the BindingContext of the page.
                Settings settings = await App.SettingsDB.GetSettingsAsync(id);
                if (settings == null)
                {
                    settings = new Settings();
                }
                //BindingContext = settings;
                EmailEditor.BindingContext = settings;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load note settings.");
            }
        }
        private void LoadPriority()
        {
            
            PriorityList.Add(new Priority { ID = 1, PriorityName = "Low" });
            PriorityList.Add(new Priority { ID = 2, PriorityName = "Medium" });
            PriorityList.Add(new Priority { ID = 3, PriorityName = "High" });

            PriorityPicker = new Picker();
            PriorityPicker.ItemsSource = PriorityList;
            PriorityPicker.ItemDisplayBinding = new Binding("PriorityName");
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.
            LoadSettings("1");
            LoadPriority();
        }
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var settings = (Settings)EmailEditor.BindingContext;
            var priority = (PriorityViewModel)PriorityPicker.BindingContext;
            settings.Date = DateTime.UtcNow;
            if (PriorityPicker != null)
            {
                Priority p = (Priority)PriorityPicker.SelectedItem;

            }
            if (!string.IsNullOrWhiteSpace(settings.Email))
            {
               // await App.SettingsDB.SaveSettingsAsync(settings);
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
        void OnPriorityPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
               // monkeyNameLabel.Text = picker.Items[selectedIndex];
            }
        }
        async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            //// Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}