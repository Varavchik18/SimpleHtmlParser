using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebClient.Tests
{
    [TestFixture]
    public class WebHelperTests
    {
        private WebHelper _webHelper;

        [SetUp]
        public void Setup()
        {
            _webHelper = new WebHelper();
        }

        [Test]
        public async Task RetrieveWebPageByUrlIfUrlIsValid_ReturnsPageContent()
        {
            // Arrange
            string validUrl = "https://example.com";

            // Act
            var pageContent = await _webHelper.RetrieveWebPageByUrlIfUrlIsValid(validUrl);

            // Assert
            Assert.IsNotNull(pageContent);
            Assert.IsTrue(pageContent.Length > 0);
        }

        [Test]
        public void RetrieveWebPageByUrlIfUrlIsValid_ThrowsException_WhenUrlIsEmpty()
        {
            // Arrange
            string emptyUrl = "";

            // Assert
            Assert.ThrowsAsync<ArgumentException>(() => _webHelper.RetrieveWebPageByUrlIfUrlIsValid(emptyUrl));
        }

        [Test]
        public void GetNumbersOfHtmlSymbols_ReturnsNumberOfSymbols()
        {
            // Arrange
            string htmlData = "<html><body><h1>Hello, World!</h1></body></html>";
            var temp = htmlData.Length;
            int expectedSymbolsCount = 48;

            // Act
            int symbolsCount = _webHelper.GetNumbersOfHtmlSymbols(htmlData);

            // Assert
            Assert.AreEqual(expectedSymbolsCount, symbolsCount);
        }

        [Test]
        public void GetTagsFromHtmlWhatContainsValue_ThrowsException_WhenHtmlContentIsNull()
        {
            // Arrange
            string nullHtmlContent = null;
            string searchValue = "div";

            // Assert
            Assert.Throws<ArgumentException>(() => _webHelper.GetTagsFromHtmlWhatContainsValue(searchValue, nullHtmlContent));
        }

        [Test]
        public void GetTagsFromHtmlWhatContainsValue_ThrowsException_WhenSearchValueIsNull()
        {
            // Arrange
            string htmlContent = "<div>Hello, <span>World!</span></div>";
            string nullSearchValue = null;

            // Assert
            Assert.Throws<ArgumentException>(() => _webHelper.GetTagsFromHtmlWhatContainsValue(nullSearchValue, htmlContent));
        }
    }
}
