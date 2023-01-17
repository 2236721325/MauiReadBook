using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Formats.Asn1;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WM.EasyReading.Service.Dtos;

namespace WM.EasyReading.Service.Services
{
    public class SuperSearch : ISuperSearch, IDisposable
    {
        public SuperSearch()
        {
            _HttpClient = new HttpClient();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            _HttpClient.DefaultRequestHeaders.Add("Accept",
                "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");

            _HttpClient.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:73.0) Gecko/20100101 Firefox/73.0");

            // 先添加这一个，才能使用 UTF 以外的其他编码 

        }
        private HttpClient _HttpClient;
        public void Dispose()
        {
            _HttpClient?.Dispose();
        }

        public async Task<List<BookSimpleDto>> Search(string search)
        {


            var response = await _HttpClient.GetAsync($"http://www.b520.cc/modules/article/search.php?searchkey={search}").ConfigureAwait(false);
            // 读取字符流
            var result = await response.Content.ReadAsStreamAsync();
            // 使用指定的字符编码读取字符流， 默认编码：UTF-8，其他如：GBK
            var stream = new StreamReader(result, Encoding.GetEncoding("gbk"));
            // 字符流转为字符串并返回
            //Console.Write(stream.ReadToEnd());



            var list = new List<BookSimpleDto>();

            var doc = new HtmlDocument();
            doc.Load(stream);
            //*[@id="hotcontent"]/table/tbody/tr[2]
            var nodes = doc.DocumentNode.SelectNodes("//div[@id='hotcontent']/table/tr");

            Console.WriteLine(nodes);
            for (int i = 1; i < nodes.Count() && i <= 3; i++)
            {
                var tdNodes = nodes[i].SelectNodes("./td");
                var title = tdNodes[0].InnerText;
                var urlNode = tdNodes[0].SelectSingleNode("./a");
                var url = urlNode.Attributes[0].Value;
                var author = tdNodes[2].InnerText;

                list.Add(new BookSimpleDto(title, url, author));

            }
            return list;


        }
        public async Task<BookDetailDto> SearchDetail(BookSimpleDto dto)
        {
            var response = await _HttpClient.GetAsync(dto.Url).ConfigureAwait(false);
            // 读取字符流
            var result = await response.Content.ReadAsStreamAsync();
            // 使用指定的字符编码读取字符流， 默认编码：UTF-8，其他如：GBK
            var stream = new StreamReader(result, Encoding.GetEncoding("gbk"));
            // 字符流转为字符串并返回
            //Console.Write(stream.ReadToEnd());


            var chapters = new List<BookChapterDto>();
            var doc = new HtmlDocument();
            doc.Load(stream);
            //*[@id="hotcontent"]/table/tbody/tr[2]
            var ddNodes = doc.DocumentNode.SelectNodes("//div[@id='list']/dl/dd");
            foreach (var ddNode in ddNodes)
            {
                var anode = ddNode.SelectSingleNode("./a");
                var name = anode.InnerText;
                var url = anode.Attributes[0].Value;
                chapters.Add(new BookChapterDto(name, url));

            }
            return new BookDetailDto(dto, chapters);
        }
        public async Task<BookChapterDto> SearchChapterContent(BookChapterDto dto)
        {
            var response = await _HttpClient.GetAsync(dto.Url).ConfigureAwait(false);
            // 读取字符流
            var result = await response.Content.ReadAsStreamAsync();
            // 使用指定的字符编码读取字符流， 默认编码：UTF-8，其他如：GBK
            var stream = new StreamReader(result, Encoding.GetEncoding("gbk"));
            var doc = new HtmlDocument();
            doc.Load(stream);


            var pnodes = doc.DocumentNode.SelectNodes("//div[@id='content']/p");
            var name = doc.DocumentNode.SelectSingleNode("//div[@class='bookname']/h1").InnerText;
            var chapter = pnodes.Select(e => e.InnerText).ToList();
            return new BookChapterDto(chapter, name, dto.Url);

        }

    }
}
