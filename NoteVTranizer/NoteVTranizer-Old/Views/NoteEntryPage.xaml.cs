using System;
using System.IO;
using NoteVTranizer.Models;
using NoteVTranizer.ViewModels;
using Xamarin.Forms;

namespace NoteVTranizer.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class NoteEntryPage : ContentPage
    {
        
        string _itemId;
        NoteEntryViewModel _noteEntryViewModel = null;
        public string ItemId
        {
            set
            {
                //LoadNote(value);
                _itemId = value;
                _noteEntryViewModel.LoadNote(_itemId);
            }
            get { return _itemId; }
        }

        public NoteEntryPage()
        {
            InitializeComponent();

            // Set the BindingContext of the page to a new Note.
            BindingContext = _noteEntryViewModel =new NoteEntryViewModel(_itemId);
        }
        #region OLD CODE
        //This code doesn't decouple the view and model
        //async void LoadNote(string itemId)
        //{
        //    try
        //    {
        //        int id = Convert.ToInt32(itemId);
        //        // Retrieve the note and set it as the BindingContext of the page.
        //        Note note = await App.NoteDB.GetNoteAsync(id);
        //        BindingContext = note;
        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine("Failed to load note.");
        //    }
        //}

        //async void OnSaveButtonClicked(object sender, EventArgs e)
        //{
        //    var note = (Note)BindingContext;
        //    note.CreatedDate = DateTime.UtcNow;
        //    if (!string.IsNullOrWhiteSpace(note.Text))
        //    {
        //        await App.NoteDB.SaveNoteAsync(note);
        //    }

        //    // Navigate backwards
        //    await Shell.Current.GoToAsync("..");
        //}

        //async void OnDeleteButtonClicked(object sender, EventArgs e)
        //{
        //    var note = (Note)BindingContext;
        //    await App.NoteDB.DeleteNoteAsync(note);

        //    // Navigate backwards
        //    await Shell.Current.GoToAsync("..");
        //}
        #endregion
    }
}