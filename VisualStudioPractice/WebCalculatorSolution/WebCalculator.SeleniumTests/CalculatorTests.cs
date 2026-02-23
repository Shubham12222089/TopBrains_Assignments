using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebCalculator.SeleniumTests
{
    [TestFixture]
    public class CalculatorTests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:5170/Calculator/Index");
            // 🔁 CHANGE PORT if needed
        }

        [Test]
        public void Add_TwoNumbers_ShouldShowResult()
        {
            driver.FindElement(By.Id("a")).SendKeys("10");
            driver.FindElement(By.Id("b")).SendKeys("5");
            driver.FindElement(By.Id("op")).SendKeys("+");
            driver.FindElement(By.Id("calculate")).Click();

            string result = driver.FindElement(By.Id("result")).Text;

            Assert.That(result, Does.Contain("15"));
        }

        [Test]
        public void Subtraction_TwoNumbers_ShouldShowResult()
        {
            driver.FindElement(By.Id("a")).SendKeys("10");
            driver.FindElement(By.Id("b")).SendKeys("5");
            driver.FindElement(By.Id("op")).SendKeys("-");
            driver.FindElement(By.Id("calculate")).Click();

            string result = driver.FindElement(By.Id("result")).Text;

            Assert.That(result, Does.Contain("5"));
        }

        [Test]
        public void Multiplication_TwoNumbers_ShouldShowResult()
        {
            driver.FindElement(By.Id("a")).SendKeys("10");
            driver.FindElement(By.Id("b")).SendKeys("5");

            var dropdown = new SelectElement(driver.FindElement(By.Id("op")));
            dropdown.SelectByValue("*");

            driver.FindElement(By.Id("calculate")).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var result = wait.Until(d => d.FindElement(By.Id("result"))).Text;

            Assert.That(result, Does.Contain("Result: 50"));
        }

        [Test]
        public void Division_TwoNumbers_ShouldShowResult()
        {
            driver.FindElement(By.Id("a")).Clear();
            driver.FindElement(By.Id("a")).SendKeys("10");

            driver.FindElement(By.Id("b")).Clear();
            driver.FindElement(By.Id("b")).SendKeys("5");

            var dropdown = new SelectElement(driver.FindElement(By.Id("op")));
            dropdown.SelectByValue("/");

            driver.FindElement(By.Id("calculate")).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var result = wait.Until(d => d.FindElement(By.Id("result"))).Text;

            Assert.That(result.Trim(), Is.EqualTo("Result: 2"));
        }

        [TearDown]
        public void Cleanup()
        {
            driver.Quit();
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
            driver?.Dispose();
        }

    }
}
