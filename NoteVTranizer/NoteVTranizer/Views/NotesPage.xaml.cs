using NoteVTranizer.Models;
using NoteVTranizer.ViewModels;
using NoteVTranizer.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteVTranizer.Views
{
    public partial class NotesPage : ContentPage
    {
        NotesViewModel _viewModel;

        public NotesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new NotesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}