using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using WebCrawlerLib;

namespace WebCrawlerWpf.ViewModel
{
    class WebCrawlerVM: INotifyPropertyChanged
    {
        private readonly WebCrawler webCrawler;
        private CrawlResult crawlResult;
        public event PropertyChangedEventHandler PropertyChanged;
        
        public CrawlResult CrawlResult
        {
            get
            {
                return crawlResult;
            }
            set
            {
                crawlResult = value;
                RaisePropertyChangedEvent(nameof(CrawlResult));
            }
        }

        public ICommand CrawlCommand
        {
            get
            {
                return new Command(CrawlLinks);
            }
        }

        public WebCrawlerVM()
        {
            webCrawler = new WebCrawler(1);
        }

        private async void CrawlLinks()
        {
            string[] urls = new string[1];
            urls[0] = "https://translate.google.ru/";
            CrawlResult crawlResult = await webCrawler.PerformCrawlingAsync(urls);
            CrawlResult = crawlResult;
        }

        protected void RaisePropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            if(handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
           
        }
    }
}
