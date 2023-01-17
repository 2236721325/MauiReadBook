using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace WM.EasyReading.DB.Models
{
    public class StoreBook
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        public string Url { get; set; }
        public string Author { get; set; }

        public string Title { get; set; }

        public string LastReadChapterUrl { get; set; }
        public string LastReadChapter { get; set; }
        public int LastReadChapterIndex { get; set; }

    }

}
