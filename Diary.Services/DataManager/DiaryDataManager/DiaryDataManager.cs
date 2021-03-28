using Diary.Services.Models;
using Dapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Diary.Services.DataManager.DiaryDataManager
{
    public class DiaryDataManager : BaseDataManager, IDiaryDataManager
    {
        public async Task AddEntry(Entry entry)
        {
            var sql = @"
            INSERT INTO dbo.Entries
            (
                UserId,
                Title,
                Content,
                CreationTime
            )
            VALUES
            (
                @userId,
	            @title,
	            @content,
	            GETDATE()
            )";

            using var db = GetDiaryDbConnection();
            await db.ExecuteAsync(sql, new
            {
                entry.userId,
                entry.title,
                entry.content
            });
        }

        public async Task<List<EntryList>> GetEntryList(int userId)
        {
            var sql = @"
            SELECT
	            E.EntryId,
	            E.UserId,
	            E.Title,
	            E.Content,
	            E.CreationTime,
	            U.UserName Author,
                0 isSharedToMe,
	            Sharing.sharedTo sharedToFriends
            FROM dbo.Entries E
            INNER JOIN dbo.Users U ON U.UserId = E.UserId
            OUTER APPLY
            (
	            SELECT STUFF((
	            SELECT
		            ',' + U.UserName 
	            FROM dbo.EntrySharing ES 
	            INNER JOIN dbo.Users U ON U.UserId = ES.SharedToUserId
	            WHERE
		            ES.EntryId = E.EntryId
	            FOR XML PATH('')),1,1,'') sharedTo
            ) Sharing
            WHERE
	            E.UserId = @userId
            AND ISNULL(E.IsDeleted,0) = 0

            UNION

            SELECT
	            E.EntryId,
	            E.UserId,
	            E.Title,
	            E.Content,
	            E.CreationTime,
	            U.UserName Author,
                1 isSharedToMe,
                NULL sharedToFriends
            FROM dbo.Entries E
            INNER JOIN dbo.EntrySharing ES ON ES.EntryId = E.EntryId
            INNER JOIN dbo.Users U ON U.UserId = E.UserId
            WHERE
	            ES.SharedToUserId = @userId
            AND ISNULL(E.IsDeleted,0) = 0

            ORDER BY E.CreationTime";

            using var db = GetDiaryDbConnection();
            var result = await db.QueryAsync<EntryList>(sql, new
            {
                userId
            });

            return result.ToList();
        }


        public async Task DeleteEntry(int entryId)
        {
            var sql = @"UPDATE dbo.Entries SET IsDeleted = 1, DeletedDate = GETDATE() Where EntryId = @entryId";

            using var db = GetDiaryDbConnection();
            await db.ExecuteAsync(sql, new
            {
                entryId
            });
        }


        public async Task<List<EntryList>> SearchEntryList(int userId, string searchString)
        {
            var sql = @"
            SELECT
	            E.EntryId,
	            E.UserId,
	            E.Title,
	            E.Content,
	            E.CreationTime,
	            U.UserName Author,
                0 isSharedToMe,
	            Sharing.sharedTo sharedToFriends
            FROM dbo.Entries E
            INNER JOIN dbo.Users U ON U.UserId = E.UserId
            OUTER APPLY
            (
	            SELECT STUFF((
	            SELECT
		            ',' + U.UserName 
	            FROM dbo.EntrySharing ES 
	            INNER JOIN dbo.Users U ON U.UserId = ES.SharedToUserId
	            WHERE
		            ES.EntryId = E.EntryId
	            FOR XML PATH('')),1,1,'') sharedTo
            ) Sharing
            WHERE
	            E.Content LIKE '%' + @searchString + '%'
            AND E.UserID = @userId
            AND ISNULL(E.IsDeleted,0) = 0";

            using var db = GetDiaryDbConnection();
            var result = await db.QueryAsync<EntryList>(sql, new
            {
                userId,
                searchString
            });

            return result.ToList();
        }

        public async Task ShareEntry(int entryId, int sharedToUserId)
        {
            var sql = @"
            INSERT INTO dbo.EntrySharing
            (
                EntryId,
                SharedToUserId,
                CreatedDate
            )
            VALUES
            (
                @entryId,        -- EntryId - int
                @sharedToUserId,        -- SharedToUserId - int
                GETDATE() -- CreatedDate - datetime
            )";

            using var db = GetDiaryDbConnection();
            await db.ExecuteAsync(sql, new
            {
                entryId,
                sharedToUserId
            });
        }
    }
}
