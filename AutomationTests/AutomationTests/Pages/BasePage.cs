using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationTests.Pages
{
    public class BasePage
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        protected void WaitForElement(By locator)
        {
            Wait.Until(d => 
            {
                try
                {
                    var element = d.FindElement(locator);
                    return element != null && element.Displayed && element.Enabled;
                }
                catch
                {
                    return false;
                }
            });
        }

        protected void Click(By locator)
        {
            WaitForElement(locator);
            Thread.Sleep(1000);
            Driver.FindElement(locator).Click();
        }

        protected void SendKeys(By locator, string text)
        {
            WaitForElement(locator);
            Thread.Sleep(1000);
            Driver.FindElement(locator).Clear();
            Driver.FindElement(locator).SendKeys(text);
        }

        protected string GetText(By locator)
        {
            WaitForElement(locator);
            return Driver.FindElement(locator).Text;
        }
    }
} 