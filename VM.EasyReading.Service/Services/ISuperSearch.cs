using HtmlAgilityPack;
using System.Net.Http;
using System.Text;
using VM.EasyReading.Service.Dtos;

namespace VM.EasyReading.Service.Services
{
    public interface ISuperSearch
    {
        public Task<List<BookSimpleDto>> Search(string search);
        public Task<BookDetailDto> SearchDetail(BookSimpleDto dto);
        public Task<BookChapterDto> SearchChapterContent(BookChapterDto dto);


    }
}
