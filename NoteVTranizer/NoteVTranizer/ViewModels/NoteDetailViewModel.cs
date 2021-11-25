using NoteVTranizer.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NoteVTranizer.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class NoteDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        public string Id { get; set; }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                //var item = await DataStore.GetItemAsync(itemId);
                var note =  await App.NoteDB.GetNoteAsync(Convert.ToInt32(itemId));
                Id = note.ID.ToString();
                Text = note.Text;
                Description = note.CreatedDate.ToString("G");
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Note");
            }
        }
    }
}
