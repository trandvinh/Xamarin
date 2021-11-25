
using NoteVTranizer.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NoteVTranizer.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            //OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            TestCommand = new Command(TestDropbox);
        }
        public Command TestCommand { get; }
        public ICommand OpenWebCommand { get; }
        private const string RedirectUri = "https://www.anysite.se/";
        private string oauth2State;
        //public async Task Authorize()
        public void Authorize()
        {
            //if (string.IsNullOrWhiteSpace(this.AccessToken) == false)
            //{
            //    // Already authorized
            //    this.OnAuthenticated?.Invoke();
            //    return;
            //}

            //if (this.GetAccessTokenFromSettings())
            //{
            //    // Found token and set AccessToken 
            //    return;
            //}

            // Run Dropbox authentication
            //this.oauth2State = Guid.NewGuid().ToString("N");
            //var authorizeUri = DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Token, "cb4zrba01i3e2vm"
                                                             //, new Uri(RedirectUri), this.oauth2State);
           //var result = DropboxOAuth2Helper.ParseTokenFragment(new Uri(authorizeUri.Url));
            //var webView = new WebView { Source = new UrlWebViewSource { Url = authorizeUri.AbsoluteUri } };
            //webView.Navigating += this.WebViewOnNavigating;
            //var contentPage = new ContentPage { Content = webView };
            //await Application.Current.MainPage.Navigation.PushModalAsync(contentPage);
        }
        private async void TestDropbox()
        {
            Authorize();
            //using (DropboxClient client = new DropboxClient(DBInfo.ACCESS_KEY))
            //using (DropboxClient client = new DropboxClient()
            //{
            //    try
            //    {
            //        bool more = true;
            //        var list = await client.Files.ListFolderAsync("");
            //        while (more)
            //        {
            //            foreach (var item in list.Entries.Where(i => i.IsFile))
            //            {
            //                // Process the file
            //            }
            //            more = list.HasMore;
            //            if (more)
            //            {
            //                list = await client.Files.ListFolderContinueAsync(list.Cursor);
            //            }
            //        }
            //    }
            //    catch
            //    {
            //        // Process the exception
            //    }
            //}

        }

    }
}