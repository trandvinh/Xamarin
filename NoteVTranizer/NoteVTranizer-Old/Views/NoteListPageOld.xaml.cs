using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NoteVTranizer.Data;
using NoteVTranizer.Models;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace NoteVTranizer.Views
{
    public partial class NoteListPageOld : ContentPage
    {
        public NoteListPageOld()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.
            collectionView.ItemsSource = await App.NoteDB.GetNotesAsync();
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            // Navigate to the NoteEntryPage, without passing any data.
            await Shell.Current.GoToAsync(nameof(NoteEntryPage));
        }
        
        async void OnEmailNotesClicked(object sender, EventArgs e)
        {
            //Email a list of notes to a preset email
            //var settings = (Settings)BindingContext;
            int id = 1;
            //// Retrieve the note and set it as the BindingContext of the page.
            Settings settings = await App.SettingsDB.GetSettingsAsync(id);
            if (settings != null)
            {
                List<string> toList = new List<string>();
                toList.Add(settings.Email);
                await Email.ComposeAsync
                  (
                      new EmailMessage
                      {
                          Subject = "My Subject",
                          Body = "My Message" + DateTime.Now.ToString("G"),
                          To = toList,
                          Cc = null,
                          Bcc = null
                      }
                  );
            }
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the NoteEntryPage, passing the ID as a query parameter.
                Note note = (Note)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(NoteEntryPage)}?{nameof(NoteEntryPage.ItemId)}={note.ID.ToString()}");
            }
        }
    }
}