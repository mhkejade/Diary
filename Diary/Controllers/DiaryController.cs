using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diary.Services.ApplicationServices;
using Diary.Services.Models;

namespace Diary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaryController : ControllerBase
    {
        private readonly IDiaryService _diaryService;

        public DiaryController(IDiaryService diaryService)
        {
            _diaryService = diaryService ?? throw new NullReferenceException(nameof(diaryService));
        }

        [Route("add-entry")]
        [HttpPost]
        public async Task<IActionResult> AddEntry(Entry entry)
        {
            await _diaryService.AddEntry(entry);
            return Ok();
        }

        [Route("list/{userId}")]
        [HttpGet]
        public async Task<IActionResult> GetEntryList([FromRoute] int userId)
        {            
            return Ok(await _diaryService.GetEntryList(userId));
        }

        [Route("delete/{entryId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteEntry([FromRoute] int entryId)
        {
            await _diaryService.DeleteEntry(entryId);
            return Ok();
        }

        [Route("search/{userId}/{searchString}")]
        [HttpGet]
        public async Task<IActionResult> SearchEntryList([FromRoute] int userId, [FromRoute] string searchString)
        {
            return Ok(await _diaryService.SearchEntryList(userId, searchString));
        }

        [Route("share-entry/{entryId}/{sharedToUserId}")]
        [HttpPost]
        public async Task<IActionResult> ShareEntry([FromRoute] int entryId, [FromRoute] int sharedToUserId)
        {
            await _diaryService.ShareEntry(entryId, sharedToUserId);
            return Ok();
        }
    }
}
