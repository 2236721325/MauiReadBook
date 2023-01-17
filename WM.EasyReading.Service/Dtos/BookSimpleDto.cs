using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.EasyReading.DB.Models;

namespace WM.EasyReading.Service.Dtos
{
    public class BookSimpleDto
    {
        public BookSimpleDto(string title, string url, string author)
        {
            Title = title;
            Url = url;
            Author = author;
        }
        public BookSimpleDto(StoreBook book)
        {
            Url = book.Url;
            Author = book.Author;
            Title = book.Title;
        }
        public string Url { get; set; }
        public string Author { get; set; }

        public string Title { get; set; }
    }
}
