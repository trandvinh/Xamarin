using NoteVTranizer.Services;
using NoteVTranizer.Data;
using System;
using System.IO;
using Xamarin.Forms;

namespace NoteVTranizer
{
    public partial class App : Application
    {
        static NoteDatabase noteDatabase;
        static SettingsDatabase settingsDatabase;
        // Create the database connection as a singleton.
        public static NoteDatabase NoteDB
        {
            get
            {
                if (noteDatabase == null)
                {
                    noteDatabase = new NoteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));
                }
                return noteDatabase;
            }

        }
        public static SettingsDatabase SettingsDB
        {
            get
            {
                if (settingsDatabase == null)
                {
                    settingsDatabase = new SettingsDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NoteSettings.db3"));
                }
                return settingsDatabase;
            }

        }
        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            //FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
