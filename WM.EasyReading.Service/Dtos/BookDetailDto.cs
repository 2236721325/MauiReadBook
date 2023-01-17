namespace WM.EasyReading.Service.Dtos
{
    public class BookDetailDto
    {
        public BookSimpleDto Simples { get; set; }
        public List<BookChapterDto> BookChapters { get; set; }
        public BookDetailDto(BookSimpleDto simples, List<BookChapterDto> bookChapters)
        {
            Simples = simples;
            BookChapters = bookChapters;
        }
    }
}
