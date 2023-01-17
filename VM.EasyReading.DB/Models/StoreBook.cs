using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace VM.EasyReading.DB.Models
{
    public class StoreBook
    {
        public int Id { get; set; }

        public String Url { get; set; }
        public string Author { get; set; }

        public string Title { get; set; }

        public string LastReadChapterUrl { get; set; }
        public string LastReadChapter { get; set; }
        public int LastReadChapterIndex { get; set; }

    }

}
