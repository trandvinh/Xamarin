using System.ComponentModel;
using TestGithub.ViewModels;
using Xamarin.Forms;

namespace TestGithub.Views
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