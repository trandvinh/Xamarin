using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteVTranizer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteVTranizerPage : ContentPage
    {
        string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NoteVTranizer.txt");

        public NoteVTranizerPage()
        {
            InitializeComponent();

            // Read the file.
            if (File.Exists(_fileName))
            {
                editor.Text = File.ReadAllText(_fileName);
            }
        }

        void OnSaveButtonClicked(object sender, EventArgs e)
        {
            // Save the file.
            File.WriteAllText(_fileName, editor.Text);
        }

        void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            // Delete the file.
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
            editor.Text = string.Empty;
        }
    }
}