using Diary.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diary.Services.DataManager.DiaryDataManager
{
    public interface IDiaryDataManager
    {
        Task AddEntry(Entry entry);
        Task DeleteEntry(int entryId);
        Task ShareEntry(int entryId, int sharedToUserId);
        Task<List<EntryList>> GetEntryList(int userId);
        Task<List<EntryList>> SearchEntryList(string searchString);       
    }
}
