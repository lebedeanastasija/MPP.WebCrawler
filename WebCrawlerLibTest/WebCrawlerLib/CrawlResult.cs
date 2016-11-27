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

        public string ToText(int nestity)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var key in Keys)
            {
                sb.Append(GenerateTabSequence(nestity)).Append(key).Append('\n');
                CrawlResult value;
                if (this.TryGetValue(key, out value))
                {
                    sb.Append(value.ToText(nestity + 1));
                }
            }
            return sb.ToString();
        }

        public string ToText()
        {
            return ToText(0);
        }
    }
}
