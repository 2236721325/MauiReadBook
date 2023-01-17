using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading;
using System.Web;
using VM.EasyReading.Service.Dtos;
using VM.EasyReading.Service.Services;

namespace VM.EasyReading.MyTests
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var seach = new SuperSearch();
            var result = await seach.SearchDetail(new BookSimpleDto("星际大祭司", "http://www.b520.cc/71_71958/", "酱油你好"));

            var r = JsonSerializer.Serialize(result, new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            });
            Console.WriteLine(r);
            seach.Dispose();
           




        }
    }
}


