using Diary.Services.ApplicationServices;
using Diary.Services.DataManager.DiaryDataManager;
using Moq;
using Xunit;
using System;
using System.Threading.Tasks;
using Diary.Services.Models;
using System.Collections.Generic;

namespace Diary.UnitTests.Services
{
    public class DiaryServiceTests
    {
        private readonly Mock<IDiaryDataManager> _diaryDataManagerMock = new Mock<IDiaryDataManager>();
        private readonly DiaryService _target;

        public DiaryServiceTests()
        {

            _target = new DiaryService(_diaryDataManagerMock.Object);
        }

        [Fact]
        public async void WhenCreatingNewDiaryEntry_ShouldCallAddEntrySuccessfully()
        {
            var entry = new Entry
            {
                userId = 1,
                title = "test",
                content = "test content",
                creationTime = DateTime.Now
            };

            _diaryDataManagerMock.Setup(x => x.AddEntry(entry));

            await _target.AddEntry(entry);

            _diaryDataManagerMock.Verify(x => x.AddEntry(entry), Times.Once);
        }

        [Fact]
        public async void WhenGettingEntryList_EntryIdShouldNotBeNull()
        {
            var userid = 1;
            var listEntry = new List<EntryList>()
            {
                new EntryList {
                    entryId = 1,
                    userId = userid,
                    title = "test",
                    content = "test content",
                    creationTime = DateTime.Now,
                author = "author"
                },
                new EntryList {
                    entryId = 2,
                    userId = userid,
                    title = "test 2",
                    content = "test content 2",
                    creationTime = DateTime.Now,
                    author = "author"
                }
            };

            _diaryDataManagerMock.Setup(x => x.GetEntryList(userid)).ReturnsAsync(listEntry);
            var result = await _target.GetEntryList(userid);

            _diaryDataManagerMock.Verify(x => x.GetEntryList(userid), Times.Once);
            Assert.NotNull(result[0].entryId);
            Assert.NotNull(result[1].entryId);
        }

        [Fact]
        public async void WhenDeletingDiaryEntry_ShouldCallDeleteEntrySuccessfully()
        {
            var entryId = 1;

            _diaryDataManagerMock.Setup(x => x.DeleteEntry(entryId));

            await _target.DeleteEntry(entryId);

            _diaryDataManagerMock.Verify(x => x.DeleteEntry(entryId), Times.Once);
        }

        [Fact]
        public async void WhenSearchingEntryList_ReturnedListCanBeEmpty()
        {
            var userid = 1;
            var searchString = "dummy search";
            var listEntry = new List<EntryList>();
            

            _diaryDataManagerMock.Setup(x => x.SearchEntryList(userid, searchString)).ReturnsAsync(listEntry);

            var result = await _target.SearchEntryList(userid, searchString);

            _diaryDataManagerMock.Verify(x => x.SearchEntryList(userid, searchString), Times.Once);
            Assert.Empty(result);
        }

        [Fact]
        public async void WhenSearchingEntryList_AndReturnsMatches_EntryIdShouldNotBeNull()
        {
            var userid = 1;
            var searchString = "test";
            var listEntry = new List<EntryList>()
            {
                new EntryList {
                    entryId = 1,
                    userId = userid,
                    title = "test",
                    content = "test content",
                    creationTime = DateTime.Now,
                author = "author"
                },
                new EntryList {
                    entryId = 2,
                    userId = userid,
                    title = "test 2",
                    content = "test content 2",
                    creationTime = DateTime.Now,
                    author = "author"
                }
            };

            _diaryDataManagerMock.Setup(x => x.SearchEntryList(userid, searchString)).ReturnsAsync(listEntry);
            var result = await _target.SearchEntryList(userid, searchString);

            _diaryDataManagerMock.Verify(x => x.SearchEntryList(userid, searchString), Times.Once);
            Assert.NotNull(result[0].entryId);
            Assert.NotNull(result[1].entryId);
        }

        [Fact]
        public async void WhenSharingDiaryEntry_ShouldCallShareEntrySuccessfully()
        {
            var entryId = 1;
            var shareToUserID = 2;

            _diaryDataManagerMock.Setup(x => x.ShareEntry(entryId, shareToUserID));

            await _target.ShareEntry(entryId, shareToUserID);

            _diaryDataManagerMock.Verify(x => x.ShareEntry(entryId, shareToUserID), Times.Once);
        }
    }
}
