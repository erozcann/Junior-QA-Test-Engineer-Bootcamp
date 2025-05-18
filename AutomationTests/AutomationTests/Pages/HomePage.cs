using OpenQA.Selenium;
using System.Threading;

namespace AutomationTests.Pages
{
    public class HomePage : BasePage
    {
        private readonly By _loginButton = By.CssSelector("a[href='/login']");
        private readonly By _signupButton = By.CssSelector("a[href='/login']");
        private readonly By _searchInput = By.CssSelector("input[type='text']");
        private readonly By _searchButton = By.CssSelector("button[type='button']");
        private readonly By _signupLoginLink = By.CssSelector("a[href='/login']");

        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public void NavigateToLogin()
        {
            var signupLoginLink = Driver.FindElement(_signupLoginLink);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", signupLoginLink);
            Thread.Sleep(1000);
            signupLoginLink.Click();
            Thread.Sleep(2000);
        }

        public void NavigateToSignup()
        {
            var signupLoginLink = Driver.FindElement(_signupLoginLink);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", signupLoginLink);
            Thread.Sleep(1000);
            signupLoginLink.Click();
            Thread.Sleep(2000);
        }

        public void SearchProduct(string productName)
        {
            SendKeys(_searchInput, productName);
            Click(_searchButton);
            Thread.Sleep(2000);
        }
    }
} 