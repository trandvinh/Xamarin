using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using NoteVTranizer.Models;

namespace NoteVTranizer.Data
{
    public class SettingsDatabase
    {
        readonly SQLiteAsyncConnection database;

        public SettingsDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Settings>().Wait();
        }

        public Task<List<Settings>> GetNotesAsync()
        {
            //Get all Settings.
            return database.Table<Settings>().ToListAsync();
        }

        public Task<Settings> GetSettingsAsync(int id)
        {
            // Get a specific note.
            return database.Table<Settings>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }
        public Settings GetSettings(int id)
        {
            Settings result = null;
            // Get a specific note.
            Task<Settings> settings = database.Table<Settings>()
                            .Where(i => i.ID == id).FirstOrDefaultAsync();
            //return database.Table<Settings>()
            if (settings != null)
            {
                result = settings.Result;
            }

            return result;
        }

        public Task<int> SaveSettingsAsync(Settings settings)
        {
            if (settings.ID != 0)
            {
                // Update an existing note.
                return database.UpdateAsync(settings);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(settings);
            }
        }

        //public Task<int> DeleteNoteAsync(Note note)
        //{
        //    // Delete a note.
        //    return database.DeleteAsync(note);
        //}
    }
}