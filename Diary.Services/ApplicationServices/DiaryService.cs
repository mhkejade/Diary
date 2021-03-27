using Diary.Services.DataManager.DiaryDataManager;
using Diary.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diary.Services.ApplicationServices
{
    public class DiaryService : IDiaryService
    {
        private readonly IDiaryDataManager _diaryDataManager;

        public DiaryService(IDiaryDataManager diaryDataManager)
        {
            _diaryDataManager = diaryDataManager ?? throw new NullReferenceException(nameof(diaryDataManager));
        }

        public async Task AddEntry(Entry entry) => await _diaryDataManager.AddEntry(entry);
        public async Task DeleteEntry(int entryId) => await _diaryDataManager.DeleteEntry(entryId);
        public async Task ShareEntry(int entryId, int sharedToUserId) => await _diaryDataManager.ShareEntry(entryId, sharedToUserId);
        public async Task<List<EntryList>> GetEntryList(int userId) => await _diaryDataManager.GetEntryList(userId);
        public async Task<List<EntryList>> SearchEntryList(string searchString) => await _diaryDataManager.SearchEntryList(searchString);
    }
}
