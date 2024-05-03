using System;
using System.Net;
using System.Threading.Tasks;

namespace DemoConsoleAppWebClient
{
    public class WebHelper
    {
        private WebClient Client { get; set; }
        public string HtmlData { get; set; }
        public long ElapsedTimeToExecute { get; set; }

        public WebHelper()
        {
            this.Client = new WebClient();
        }
    }
}
