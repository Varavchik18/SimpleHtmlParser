using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsoleAppWebClient.Models
{
    public class WebPage
    {
        public string? Url { get; set; }
        public string? HtmlData { get; set; }
        public long ElapsedTimeToRetrieve { get; set; }
    }
}
