using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
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

        public int GetNumbersOfHtmlSymbols(string htmlData) => htmlData.Length;

        public List<string> GetTagsFromHtmlWhatContainsValue(string searchValue, string htmlContent)
        {
            if (string.IsNullOrEmpty(htmlContent) || string.IsNullOrEmpty(searchValue))
            {
                throw new ArgumentException("HTML content and search value cannot be null or empty.");
            }

            string pattern = $@"<[^>]*{searchValue}[^>]*>";

            MatchCollection tagMatches = Regex.Matches(htmlContent, pattern, RegexOptions.IgnoreCase);

            List<string> results = new List<string>();

            foreach (Match match in tagMatches)
            {
                results.Add(match.Value);
            }

            return results;
        }

    }
}
