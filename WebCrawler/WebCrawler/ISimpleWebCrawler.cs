﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    public interface ISimpleWebCrawler
    {
        Task<List<CrawlResult>> PerformCrawlingAsync(List<string> rootUrls);
    }
}
