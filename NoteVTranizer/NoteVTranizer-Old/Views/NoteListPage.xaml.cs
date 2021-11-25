using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NoteVTranizer.Data;
using NoteVTranizer.Models;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace NoteVTranizer.Views
{
    public partial class NoteListPage : ContentPage
    {
        private string _highPriorityFormat = @"<p>   {0}.{1}</p><br />";
        public NoteListPage()
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

        private string CreateMessage()
        {
            string result = String.Empty;
            StringBuilder sb = new StringBuilder();
            int idx = 0;

            if ((collectionView != null) && (collectionView.ItemsSource != null))
            {
                sb.Append(@"<html>
                              <head>
                              </head>
                              <body>");
                sb.AppendFormat(@"<h2>Note List {0}</h2><br />", DateTime.Now.ToString("G"));
                foreach (Note note in collectionView.ItemsSource)
                {
                    if (note.UseEmail)
                    {
                        idx++;

                        if (idx == 1)
                        {
                            sb.AppendFormat(@"<p>&nbsp &nbsp *** {0}. {1}</p><br />", idx, note.Text);
                        }
                        else
                        {
                            sb.AppendFormat(@"<p>&nbsp &nbsp {0}. {1}</p><br />", idx, note.Text);
                        }
                    }
                }
                sb.Append("</body></html>");
                result = sb.ToString();
            }

            return result;
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
                          Subject = String.Format("NoteVTranizer {0}",DateTime.Now.ToString("G")),
                          Body = CreateMessage(),
                          To = toList,
                          Cc = null,
                          Bcc = null,
                          BodyFormat = EmailBodyFormat.Html
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