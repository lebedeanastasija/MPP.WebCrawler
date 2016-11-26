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
            var j = 0;
            foreach (string rootUrl in rootUrls)
            {
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
                           urlsCrawlResult[pageUrl] = await CrawlUrl(pageUrl, nesting + 1);                     
                        }
                    }
                    catch(Exception e)
                    {
                        throw e;
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
