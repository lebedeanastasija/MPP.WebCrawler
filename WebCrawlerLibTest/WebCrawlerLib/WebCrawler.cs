using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlerLib
{
    public class WebCrawler: IWebCrawler
    {
        public int maxNesting;

        public WebCrawler(int nesting)
        {
            this.maxNesting = nesting;
        }

        public async Task<CrawlResult> PerformCrawlingAsync(string[] rootUrls)
        {
            CrawlResult crawlResult = new CrawlResult();          

            foreach (string rootUrl in rootUrls)
            {
               if(rootUrl != null)
                    crawlResult[rootUrl] = await CrawlUrl(rootUrl, 0);
            }
            return crawlResult;
        }

        private async Task<CrawlResult> CrawlUrl(string url, int nesting)
        {
            CrawlResult urlsCrawlResult;

            if (nesting <= maxNesting)
            {
                urlsCrawlResult = new CrawlResult();

                if(nesting < maxNesting)
                { 
                    string pageHtml = await HtmlUrlSearcher.Instance.GetPageHtml(url);       

                    try
                    { 
                        foreach (string pageUrl in HtmlUrlSearcher.Instance.GetPageUrls(pageHtml, url))
                        {               
                            try
                            {
                                urlsCrawlResult[pageUrl] = await CrawlUrl(pageUrl, nesting + 1);
                            } 
                            catch(Exception e)
                            {
                                Console.WriteLine(e.Message);
                                if (urlsCrawlResult.Keys.Contains(pageUrl))
                                {
                                    urlsCrawlResult[pageUrl] = null;
                                }
                            }                                                   
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            else
            {
                urlsCrawlResult = null;
            }

            return urlsCrawlResult;
        }
    }
}
