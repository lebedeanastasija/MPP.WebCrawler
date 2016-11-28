using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using WebCrawlerLib;
using System.Configuration;
using System.IO;

namespace WebCrawlerWpf.ViewModel
{
    class WebCrawlerVM: INotifyPropertyChanged
    {
        
        private readonly WebCrawler webCrawler;
        private CrawlResult crawlResult;
        private int clicks;
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

        public ICommand ClickCommand 
        {
            get
            {
                return new Command(ClickButton);
            }
        }

        public int Clicks
        {
            get
            {
                return clicks;
            }
            set
            {
                clicks = value;
                RaisePropertyChangedEvent(nameof(Clicks));
            }
        }

        public WebCrawlerVM()
        {
            webCrawler = new WebCrawler(2);
            Clicks = 0;
        }

        private async void CrawlLinks()
        {
            string[] urls = new string[1];
            urls[0] = "https://translate.google.ru/";
            CrawlResult crawlResult = await webCrawler.PerformCrawlingAsync(urls);
            CrawlResult = crawlResult;
        }

        private void ClickButton()
        {
            int clicks = Clicks + 1;
            Clicks = clicks;
        }

        protected void RaisePropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            if(handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));           
        }
    }
}
