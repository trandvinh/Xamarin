using System;
using SQLite;
namespace NoteVTranizer.Models
{
     public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Filename { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool UseEmail { get; set; }
        public DateTime ExpiredDate { get; set; }
        public NotePriorityEnum Priority { get; set; }
        public bool Deleted { get; set; }
    }
}
