using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
//using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium.IE;

namespace WebApplication.Tests.Controllers
{
    class SeleniumBasedTest
    {
        [TestClass]
        public class MySeleniumTests
        {
            private TestContext testContextInstance;
            private IWebDriver driver;
            private string appURL;

            public MySeleniumTests()
            {
            }

            [TestMethod]
            [TestCategory("Chrome")]
            public void TheBingSearchTest()
            {
                driver.Navigate().GoToUrl(appURL + "/Account/Register");
                driver.FindElement(By.Id("Email")).SendKeys("");
                driver.FindElement(By.Id("Password")).SendKeys("");               
                driver.FindElement(By.Id("Register")).Click();
                Assert.IsTrue(driver.FindElement(By.XPath("//li[(text())]")).Text.Contains("Email field is required"));
            }

            /// <summary>
            ///Gets or sets the test context which provides
            ///information about and functionality for the current test run.
            ///</summary>
            public TestContext TestContext
            {
                get
                {
                    return testContextInstance;
                }
                set
                {
                    testContextInstance = value;
                }
            }

            [TestInitialize()]
            public void SetupTest()
            {
                //appURL = "";
                appURL = "https://webapplication-qa.azurewebsites.net/";

                string browser = "Chrome";
                switch (browser)
                {
                    case "Chrome":
                        driver = new ChromeDriver();
                        break;
                    //case "Firefox":
                    //    driver = new FirefoxDriver();
                    //    break;
                    //case "IE":
                    //    driver = new InternetExplorerDriver();
                    //    break;
                    default:
                        driver = new ChromeDriver();
                        break;
                }

            }

            [TestCleanup()]
            public void MyTestCleanup()
            {
                driver.Quit();
            }
        }
    }
}
