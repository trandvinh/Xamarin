using NoteVTranizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteVTranizer.Services
{
    public class PriorityDataService
    {
        readonly List<Priority> priorityList;

        public PriorityDataService()
        {
            priorityList = new List<Priority>();

            priorityList.Add(new Priority { ID = 1, PriorityName = "Low" });
            priorityList.Add(new Priority { ID = 2, PriorityName = "Medium" });
            priorityList.Add(new Priority { ID = 3, PriorityName = "High" });

        }

       

        //public async Task<Item> GetItemAsync(string id)
        //{
        //    return await Task.FromResult(priorityList.FirstOrDefault(s => s.Id == id));
        //}
        public async Task<IEnumerable<Priority>> GetPriorityListAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(priorityList);
        }
        public List<Priority>GetPriorityList()
        {
            return priorityList;
        }
    }
}