using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diary.Services.Models
{
    public class Entry
    {
        public int? entryId { get; set; }
        public int userId { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime creationTime { get; set; }
    }

    public class EntryList : Entry
    {
        public string author { get; set; }
        public string sharedToFriends { get; set; }
        public bool isSharedToMe { get; set; } = false;        
    }
}
