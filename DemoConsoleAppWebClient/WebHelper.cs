using System;
using System.Net;
using System.Threading.Tasks;

namespace DemoConsoleAppWebClient
{
    public class WebHelper
    {
        private WebClient Client { get; set; }

        public WebHelper()
        {
            this.Client = new WebClient();
        }

        public async Task<string> RetrieveWebPageByUrlIfUrlIsValid(string url)
        {

            try
            {
                ArgumentException.ThrowIfNullOrEmpty(url, nameof(url));
                var webSource = await this.Client.DownloadStringTaskAsync(url);
                return webSource;
            }
            catch (ArgumentException exception)
            {
                throw;
            }
        }
    }
}
