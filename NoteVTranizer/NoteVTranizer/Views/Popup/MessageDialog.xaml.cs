using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteVTranizer.Views.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessageDialog : PopupPage
    {
        public MessageDialog(string title, string message, string OkText, Action OkAction = null, string CancelText = null, Action CancelAction = null)
        {
            InitializeComponent();
        }
    }
}