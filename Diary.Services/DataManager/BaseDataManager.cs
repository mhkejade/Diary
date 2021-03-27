using System;
using System.Data;
using System.Data.SqlClient;

namespace Diary.Services.DataManager
{
    public abstract class BaseDataManager
    {
        private static string _diaryConnectionString;

        public static void Configure(string diaryConnectionString)
        {
            if (string.IsNullOrWhiteSpace(diaryConnectionString))
            {
                throw new ApplicationException("Invalid diary connection string.");
            }

            _diaryConnectionString = diaryConnectionString;
        }
        protected IDbConnection GetDiaryDbConnection() => new SqlConnection(_diaryConnectionString);
    }
}
