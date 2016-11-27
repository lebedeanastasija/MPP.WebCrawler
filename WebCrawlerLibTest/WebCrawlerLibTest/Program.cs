using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawlerLib;

namespace WebCrawlerLibTest
{
    class Program
    {
        static void Main(string[] args)
        {
            WebCrawler crawler = new WebCrawler(1);
            string[] urls = new string[1];
            urls[0] = "https://translate.google.ru/";
            try
            { 
                var crawlResult = crawler.PerformCrawlingAsync(urls).Result;
                Console.Write(crawlResult.ToText());
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            Console.Read();
        }
    }
}
