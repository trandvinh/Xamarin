using NoteVTranizer.Models;
using NoteVTranizer.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;
using System.Windows.Input;

namespace NoteVTranizer.ViewModels
{
    public class NotesViewModel : BaseViewModel
    {
        private Note _selectedNote;
        private Settings _theSettings;

        public ICommand EmailCommand => new Command<Note>(EmailNote);
        public ICommand DeleteCommand => new Command<Note>(DeleteNote);
        public ObservableCollection<Note> Notes { get; private set; }
        public Command LoadNotesCommand { get; }
        public Command AddNoteCommand { get; }

        public Command EmailNotesCommand { get; }
        public Command<Note> NoteTapped { get; }


        public Settings TheSettings
        {
            get => _theSettings;
            set
            {
                _theSettings = value;
                OnPropertyChanged();
            }
        }
       
        public NotesViewModel()
        {
            Title = "NoteVTranizer";
         
            Notes = new ObservableCollection<Note>();
            LoadNotesCommand = new Command(async () => await ExecuteLoadNotesCommand());

            NoteTapped = new Command<Note>(OnNoteSelected);

            AddNoteCommand = new Command(OnAddNote);

            EmailNotesCommand = new Command(OnEmailNotes, ValidateEmailNotes);

            this.PropertyChanged +=
             (_, __) => EmailNotesCommand.ChangeCanExecute();
        }

        public bool ValidateEmailNotes()
        {
            bool canEmail = false;
            if (TheSettings != null)
            {
                canEmail = !String.IsNullOrWhiteSpace(TheSettings.Email);
            }
            return canEmail;
        }
        private async void GetSettings()
        {
            //var settings = (Settings)BindingContext;
            int id = 1;
            //// Retrieve the note and set it as the BindingContext of the page.
            TheSettings = await App.SettingsDB.GetSettingsAsync(id);
        }
        private async void EmailNote(Note note)
        {
            try
            {
                //App.Current.MainPage.DisplayAlert("Info", "Favorite Note", "Yes", "No");
                if (note != null)
                {
                    int id = 1;
                    //// Retrieve the note and set it as the BindingContext of the page.
                    Settings settings = await App.SettingsDB.GetSettingsAsync(id);
                    if (settings != null)
                    {
                        List<string> toList = new List<string>();
                        toList.Add(settings.Email);
                        string message = String.Format("{0} expired in {1}.", note.Text, note.ExpiredDate.Value.ToString("d"));
                        await Email.ComposeAsync
                          (
                              new EmailMessage
                              {
                                  Subject = note.Text,
                                  Body = CreateMessage(note),
                                  To = toList,
                                  Cc = null,
                                  Bcc = null,
                                  BodyFormat = EmailBodyFormat.Html
                              }
                          );
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void DeleteNote(Note note)
        {
            //string msg = "Will delete";
            if (note != null)
            {
               // msg = String.Format("Will delete '{0}' note", note.Text);
                if (note != null)
                {
                    var respond = await App.Current.MainPage.DisplayAlert("Warning", "Are you sure to delete?", "Yes", "No");
                    if (respond == true)
                    {
                        await App.NoteDB.DeleteNoteAsync(note);
                        Notes.Remove(note);
                        // Navigate backwards
                        await Shell.Current.GoToAsync("..");
                    }
                }
            }
            //App.Current.MainPage.DisplayAlert("Warning", msg, "Yes", "No");
        }
        async Task ExecuteLoadNotesCommand()
        {
            IsBusy = true;

            try
            {
                Notes.Clear();//Clear to make ObservableCollection raise event
                //if (TheSettings == null)
                {
                    GetSettings();
                    //TheSettings =  App.SettingsDB.GetSettings(1);
                }
                List<Note> tempNoteList = await App.NoteDB.GetNotesAsync();
                tempNoteList = tempNoteList.OrderByDescending(x => x.CreatedDate).ToList();
                if (TheSettings != null)
                {
                    if (TheSettings.OrderByIDView == (int)NoteSortByEnum.EXPIRED_DATE)
                    {
                        tempNoteList = tempNoteList.OrderByDescending(x => x.ExpiredDate).ToList();
                    }
                    else if (TheSettings.OrderByIDView == (int)NoteSortByEnum.PRIORITY)
                    {
                        tempNoteList = tempNoteList.OrderByDescending(x => x.Priority).ToList();
                    }
                }
               
                foreach (var note in tempNoteList)
                {
                    Notes.Add(note);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedNote = null;
            GetSettings();
        }

        public Note SelectedNote
        {
            get => _selectedNote;
            set
            {
                SetProperty(ref _selectedNote, value);
                OnNoteSelected(value);
            }
        }

        private async void OnAddNote(object obj)
        {
            // Must register NoteDetailPage (Routing.RegisterRoute(nameof(NoteEntryPage), typeof(NoteEntryPage));) in AppShell
            // to avoid error 'Unable to figure out route for:...'
            //await Shell.Current.GoToAsync(nameof(NoteEntryPage));
            //ItemId is empty to indicate create new Note.
            await Shell.Current.GoToAsync($"{nameof(NoteEntryPage)}?{nameof(NoteEntryViewModel.ItemId)}={String.Empty}");
        }
        private string CreateMessage(NoteSortByEnum sbi)
        {
            string result = String.Empty;
            StringBuilder sb = new StringBuilder();
            int idx = 0;

            if ((Notes != null) && (Notes.Count > 0))
            {
                
                List<Note> sortNotes = null;
                if (sbi == NoteSortByEnum.PRIORITY)
                {
                    sortNotes = new List<Note>(Notes.ToList().OrderByDescending(x => x.Priority));//OrderByDescending
                }
                else if (sbi == NoteSortByEnum.CREATED_DATE)
                {
                    sortNotes = new List<Note>(Notes.ToList().OrderBy(x => x.CreatedDate));
                }
                else if (sbi == NoteSortByEnum.EXPIRED_DATE)
                {
                    sortNotes = new List<Note>(Notes.ToList().OrderBy(x => x.ExpiredDate));
                }
                else //default is High priority
                {
                    sortNotes = new List<Note>(Notes.ToList().OrderBy(x => x.Priority));
                }
                sb.Append(@"<html>
                              <head>
                              </head>
                              <body>");
                sb.AppendFormat(@"<h2>Note List {0}</h2><br />", DateTime.Now.ToString("d"));
                foreach (Note note in sortNotes)
                {
                    if (note.UseEmail)
                    {
                        idx++;

                        if (note.Priority == NotePriorityEnum.HIGH)
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
            else
            {
                Debug.WriteLine("Note list is null/empty.");
            }
            return result;
        }
        private string CreateMessage(Note note)
        {
            string result = String.Empty;
            StringBuilder sb = new StringBuilder();

            if (note != null)
            {
                sb.Append(@"<html>
                              <head>
                              </head>
                              <body>");
                string noteMessage = string.Format(@"<h2>Note: {0}.</h2><br />", note.Text);
                if (note.ExpiredDate.HasValue)
                {
                    noteMessage = string.Format(@"<h2>Note: {0} expired {1}.</h2><br />", note.Text, note.ExpiredDate.Value.ToString("d"));
                }
                sb.Append(noteMessage);
                sb.AppendFormat(@"<p>&nbsp &nbsp Priority: {0}</p><br />", note.Priority);
                sb.AppendFormat(@"<p>&nbsp &nbsp Created Date: {0}</p><br />", note.CreatedDate.ToString("d"));
                sb.Append("</body></html>");
                result = sb.ToString();
            }
            else
            {
                Debug.WriteLine("Note is null.");
            }
            return result;
        }
        private async void OnEmailNotes()
        {
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
                          Subject = String.Format("NoteVTranizer {0}", DateTime.Now.ToString("d")),
                          Body = CreateMessage((NoteSortByEnum) settings.OrderByID),
                          To = toList,
                          Cc = null,
                          Bcc = null
                          ,BodyFormat = EmailBodyFormat.Html
                      }
                  );
            }
        }

        async void OnNoteSelected(Note note)
        {
            if (note == null)
                return;

            // This will push the NoteDetailPage onto the navigation stack
            //NoteDetailViewModel has [QueryProperty(nameof(ItemId), nameof(ItemId))] attribute to receive query string ItemId
            // Must register NoteDetailPage (Routing.RegisterRoute(nameof(NoteDetailPage), typeof(NoteDetailPage));) in AppShell
            // to avoid error 'Unable to figure out route for: NoteDetailPage?ItemId='
            //await Shell.Current.GoToAsync($"{nameof(NoteDetailPage)}?{nameof(NoteDetailViewModel.ItemId)}={note.ID.ToString()}");
            await Shell.Current.GoToAsync($"{nameof(NoteEntryPage)}?{nameof(NoteEntryViewModel.ItemId)}={note.ID.ToString()}");
        }
    }
}