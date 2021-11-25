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
        public int OrderByID { get; set; }
        public int OrderByIDView { get; set; }
    }
}
