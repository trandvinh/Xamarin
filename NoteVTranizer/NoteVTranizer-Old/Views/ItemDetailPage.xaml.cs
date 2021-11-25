using NoteVTranizer.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace NoteVTranizer.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}