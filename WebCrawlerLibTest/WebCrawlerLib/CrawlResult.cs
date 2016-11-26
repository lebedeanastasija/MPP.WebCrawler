using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlerLib
{
    public class CrawlResult: Dictionary<string, CrawlResult>
    {
        private string GenerateTabSequence(int count)
        {
            return new string('\t', count);
        }

        public string ToString(int depth)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var key in Keys)
            {
                sb.Append(GenerateTabSequence(depth)).Append(key).Append('\n');
                var value = this[key];
                if (value != null)
                {
                    sb.Append(value.ToString(depth + 1));
                }
            }
            return sb.ToString();
        }

        public string ToString()
        {
            return ToString(0);
        }
    }
}
