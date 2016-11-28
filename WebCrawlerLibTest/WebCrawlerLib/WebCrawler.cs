using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using log4net.Appender;
using System.IO;

namespace WebCrawlerLib
{
    public class WebCrawler: IWebCrawler
    {
        private int maxNesting;


        private static readonly ILog logger = LogManager.GetLogger(typeof(WebCrawler));

        public WebCrawler(int nesting)
        {
            this.maxNesting = nesting;
            BasicConfigurator.Configure();
        }

        public async Task<CrawlResult> PerformCrawlingAsync(string[] rootUrls)
        {
            CrawlResult crawlResult = new CrawlResult();
            if (rootUrls.Length > 0)
            {
                
                foreach (string rootUrl in rootUrls)
                {
                    if (rootUrl != null)
                        crawlResult[rootUrl] = await CrawlUrl(rootUrl, 0);
                }
            }
            else
            {
                crawlResult["--invalid urls config info--"] = null;
                logger.Warn("\nWARN: --invalid urls config info--\n");
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
                                logger.Error(pageUrl + ": ", e);

                                if (urlsCrawlResult.Keys.Contains(pageUrl))
                                {
                                    urlsCrawlResult[pageUrl] = null;
                                }
                            }                                                   
                        }
                    }
                    catch(Exception e)
                    {
                        logger.Error(url + ": ", e);
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
