using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XmlConfiguration;
using System.Xml.XPath;

namespace WebCrawlerWpf.ViewModel
{
    public class ConfigReader
    {
        private static ConfigReader instance;

        public int MaxCrawlNestity
        {
            get;
            private set;
        }

        public List<string> RootResources
        {
            get;
            private set;
        }

        private XPathDocument document;

        private ConfigReader(string fileName)
        {
            document = new System.Xml.XPath.XPathDocument(fileName);
            RootResources = new List<string>();
        }

        public static ConfigReader Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new ConfigReader(@"D:\BSUIR\sem_5\СПП\lab4\MPP.WebCrawler\WebCrawlerWpf\WebCrawlerWpf\ViewModel\congfig.xml");
                }
                return instance;
            }
        }

        public void Read()
        {
            foreach (System.Xml.XPath.XPathNavigator child in document.CreateNavigator().Select("root/*"))
            {
                if(child.Name == "depth")
                {
                    int maxCrawlNestity;
                    if (int.TryParse(child.InnerXml, out maxCrawlNestity))
                    {
                        MaxCrawlNestity = maxCrawlNestity;
                    }
                    else
                    {
                        MaxCrawlNestity = 2;
                    }
                }
            }

           
            foreach (System.Xml.XPath.XPathNavigator child in document.CreateNavigator().Select("root/rootResources/*"))
            {
                if (child.Name == "resource")
                {
                    Uri uriResult;
                    bool isValidUri = Uri.TryCreate(child.InnerXml, UriKind.Absolute, out uriResult)
                        && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                    if (isValidUri)
                    {
                        RootResources.Add(child.InnerXml);
                    }                    
                }
            }  
            
            if(MaxCrawlNestity == null)
            {
                MaxCrawlNestity = 2;
            }         
        }
    }
}
