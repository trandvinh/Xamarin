using System;
using System.Collections.Generic;
using System.Text;

namespace NoteVTranizer.Models
{
    public enum NotePriorityEnum
    {
        None = 0,
        LOW = 1,
        MEDIUM = 2,
        HIGH = 3
    }
    public enum NoteSortByEnum
    {
        PRIORITY = 0,
        EXPIRED_DATE = 1,
        CREATED_DATE = 2
    }
}
