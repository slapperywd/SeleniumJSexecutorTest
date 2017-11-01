using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace SeleniumJSexecutorTest
{
  
    [TestClass]
    public class JavaScriptExecutorTest
    {
        IWebDriver driver;
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            driver = new ChromeDriver();
        }

        [TestMethod]
        public void HighlightTest()
        {
            driver.Navigate().GoToUrl("http://stackoverflow.com");
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            IWebElement usersNav = driver.FindElement(By.Id("nav-users"));
            js.ExecuteScript("arguments[0].style.backgroundColor = '" + "yellow" + "'", usersNav);
            
        }

        [TestMethod]
        public void DragNdropTest()
        {
            driver.Navigate().GoToUrl("http://cookbook.seleniumacademy.com/DragDropDemo.html");

            IWebElement draggable = driver.FindElement(By.Id("draggable"));
            IWebElement droppable = driver.FindElement(By.Id("droppable"));

            Actions builder = new Actions(driver);
            builder.DragAndDrop(draggable, droppable).Perform();
            Assert.AreEqual("Dropped!", droppable.Text);
        }

        [DeploymentItem(@"Resources")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\users.csv",
            "users#csv", DataAccessMethod.Sequential)]
        [TestMethod]
        public void CsvDataDrivenTest()
        {
            var name = TestContext.DataRow["user"].ToString();
            Assert.IsTrue(name.Length >= 3);
        }

        [TestCleanup]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
