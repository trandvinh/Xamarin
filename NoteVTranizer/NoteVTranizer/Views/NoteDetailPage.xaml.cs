using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteVTranizer.Views
{

    public partial class NoteDetailPage : ContentPage
    {
        
        public NoteDetailPage()
        {
            InitializeComponent();
            BindingContext = new ViewModels.NoteDetailViewModel();
        }
    }
}