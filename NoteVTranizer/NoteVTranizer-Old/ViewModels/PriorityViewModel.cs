using NoteVTranizer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace NoteVTranizer.ViewModels
{
    public class PriorityViewModel : BaseViewModel
    {

        public IList<Priority> PriorityList { get; set; }

        public PriorityViewModel()
        {
           
            try
            {
                PriorityList = new ObservableCollection<Priority>();
                PriorityList.Add(new Priority { ID = 1, PriorityName = "Low" });
                PriorityList.Add(new Priority { ID = 2, PriorityName = "Medium" });
                PriorityList.Add(new Priority { ID = 3, PriorityName = "High" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
               // throw;
            }
        }

    }
}
