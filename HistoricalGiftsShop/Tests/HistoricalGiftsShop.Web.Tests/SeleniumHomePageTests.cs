namespace HistoricalGiftsShop.Web.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Remote;
    using Xunit;

    [Trait("Environment", "Localhost")]
    public class SeleniumHomePageTests : IClassFixture<SeleniumServerFactory<Startup>>, IDisposable
    {
        private const string ChromeDriverEnvironmentVariableName = "ChromeWebDriver";

        private readonly RemoteWebDriver browser;
        private readonly SeleniumServerFactory<Startup> serverFactory;

        public SeleniumHomePageTests(SeleniumServerFactory<Startup> server)
        {
            this.serverFactory = server;

            var options = new ChromeOptions();
            options.AddArguments("--headless", "--no-sandbox", "--ignore-certificate-errors");

            var chromeDriverLocation = string.IsNullOrEmpty(Environment.GetEnvironmentVariable(ChromeDriverEnvironmentVariableName)) ?
                System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) :
                Environment.GetEnvironmentVariable(ChromeDriverEnvironmentVariableName);
            this.browser = new ChromeDriver(chromeDriverLocation, options);
        }

        [Fact]
        public void HomePageShouldHaveH1Tag()
        {
            if (string.IsNullOrEmpty(this.serverFactory.RootUri))
            {
                this.serverFactory.RootUri = "https://localhost:5001";
            }

            try
            {
                // Arrange & Act
                this.browser.Navigate().GoToUrl(this.serverFactory.RootUri + "/Home/Index");

                // Assert
                Assert.Contains("Welcome to", this.browser.FindElementByCssSelector("h1").Text);
            }
            catch
            {
                var screenshot = (this.browser as ITakesScreenshot).GetScreenshot();
                screenshot.SaveAsFile("screenshot.png");
                throw;
            }
        }

        [Fact]
        public void HomePageShouldHaveDivWithOPCTextClass()
        {
            this.browser.Navigate().GoToUrl(this.serverFactory.RootUri + "/Home/Index");
            Assert.Contains("ОРС", this.browser.FindElementByClassName("opc-text").Text);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.browser?.Dispose();
            }
        }
    }
}
