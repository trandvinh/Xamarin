using System;
using System.Collections.Generic;
using System.Text;

namespace NoteVTranizer.Models
{
    public class DBInfo
    {
        public static string ACCESS_KEY = "sl.A8bkSVv6bdtXs0ItCNPwqnoLW3frswCzY8nLaCCJ5Tx4MBOuP7sw-5Jgdq2MkCLvF8wvcez76XNzxcKn_wIjRL-m9hfsmfab_eauc3GEsxuiIiL93X2tJq0WU431u-hsRFyzjbw";
    }
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
