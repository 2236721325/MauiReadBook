using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.EasyReading.DB.Models;

namespace WM.EasyReading.DB.Databases
{
    public class MyDatabase
    {
        public SQLiteAsyncConnection Database;

        public  MyDatabase()
        {
            Task.Run(() => this.Init()).Wait();
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
