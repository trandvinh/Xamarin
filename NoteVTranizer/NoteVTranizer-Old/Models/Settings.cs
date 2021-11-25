using System;
using SQLite;

namespace NoteVTranizer.Models
{
    public class Settings
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }

        public NoteSortByEnum Sort { get; set; }
    }
}
