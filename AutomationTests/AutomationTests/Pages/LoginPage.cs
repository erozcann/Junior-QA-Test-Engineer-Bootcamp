using OpenQA.Selenium;

namespace AutomationTests.Pages
{
    public class LoginPage : BasePage
    {
        private readonly By _emailInput = By.CssSelector("input[data-qa='login-email']");
        private readonly By _passwordInput = By.CssSelector("input[data-qa='login-password']");
        private readonly By _loginButton = By.CssSelector("button[data-qa='login-button']");
        private readonly By _errorMessage = By.CssSelector(".login-form p");

        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        public void Login(string email, string password)
        {
            SendKeys(_emailInput, email);
            SendKeys(_passwordInput, password);
            Click(_loginButton);
        }

        public string GetErrorMessage()
        {
            return GetText(_errorMessage);
        }
    }
} 