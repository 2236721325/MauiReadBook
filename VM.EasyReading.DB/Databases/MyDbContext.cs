using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.EasyReading.DB.Models;

namespace VM.EasyReading.DB.Databases
{
    public class EasyReadingDatabase
    {
        SQLiteAsyncConnection Database;

        public EasyReadingDatabase()
        {
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<StoreBook>();
        }
}
}
