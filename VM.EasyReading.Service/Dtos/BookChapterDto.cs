namespace VM.EasyReading.Service.Dtos
{
    public class BookChapterDto
    {
        public BookChapterDto(string name, string url)
        {
            Name = name;
            Url = url;
        }
        public BookChapterDto(List<string> paragraphs, string name, string url)
        {
            Paragraphs = paragraphs;
            Name = name;
            Url = url;
        }

        public List<string> Paragraphs { get; set; }

        public string Name { get; set; }
        public string Url { get; set; }
    }
}
