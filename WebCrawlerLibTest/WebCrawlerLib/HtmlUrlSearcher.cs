using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net.Http;

namespace WebCrawlerLib
{
    public class HtmlUrlSearcher
    {
        private static readonly HtmlUrlSearcher instance = new HtmlUrlSearcher();

        public static HtmlUrlSearcher Instance
        {
            get
            {
                return instance;
            }
        }

        public async Task<string> GetPageHtml(string pageUrl)
        {
            string pageHtml = "";
            HttpClient httpClient = new HttpClient();
            try
            {
                pageHtml = await httpClient.GetStringAsync(pageUrl);
            }
            catch (Exception e)
            {
                throw e;
            }
            
            return pageHtml.ToString();
        }

        public List<string> GetPageUrls(string pageHtml, string rootUrl)
        {
            HtmlWeb web = new HtmlWeb();
            List<string> pageUrls = new List<string>();
            HtmlDocument page = null;
            try
            {
                page = web.Load(rootUrl);
                page.LoadHtml(pageHtml);
            }
            catch(Exception e)
            {
                throw e;
            }
            HtmlNodeCollection links = page.DocumentNode.SelectNodes("//a");
            Uri root = new Uri(rootUrl);
            try
            { 
                foreach (var link in links)
                {
                    if (link.Attributes.Contains("href"))
                    {
                        string hrefValue = link.Attributes["href"].Value;
                        try
                        {
                            Uri temp = new Uri(root, hrefValue);
                            string absTemp = temp.AbsoluteUri;
                            pageUrls.Add(absTemp);
                        }
                        catch(Exception e)
                        {
                            throw e;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return pageUrls;
        }
    }
}
