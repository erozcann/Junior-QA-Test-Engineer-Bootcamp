using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationTests.Pages
{
    public class SignupPage : BasePage
    {
        private readonly By _nameInput = By.CssSelector("input[data-qa='signup-name']");
        private readonly By _emailInput = By.CssSelector("input[data-qa='signup-email']");
        private readonly By _signupButton = By.CssSelector("button[data-qa='signup-button']");
        private readonly By _titleMr = By.CssSelector("input[value='Mr']");
        private readonly By _passwordInput = By.CssSelector("input[data-qa='password']");
        private readonly By _daySelect = By.CssSelector("select[data-qa='days']");
        private readonly By _monthSelect = By.CssSelector("select[data-qa='months']");
        private readonly By _yearSelect = By.CssSelector("select[data-qa='years']");
        private readonly By _newsletterCheckbox = By.CssSelector("input[data-qa='newsletter']");
        private readonly By _specialOffersCheckbox = By.CssSelector("input[data-qa='optin']");
        private readonly By _firstNameInput = By.CssSelector("input[data-qa='first_name']");
        private readonly By _lastNameInput = By.CssSelector("input[data-qa='last_name']");
        private readonly By _companyInput = By.CssSelector("input[data-qa='company']");
        private readonly By _address1Input = By.CssSelector("input[data-qa='address']");
        private readonly By _address2Input = By.CssSelector("input[data-qa='address2']");
        private readonly By _countrySelect = By.CssSelector("select[data-qa='country']");
        private readonly By _stateInput = By.CssSelector("input[data-qa='state']");
        private readonly By _cityInput = By.CssSelector("input[data-qa='city']");
        private readonly By _zipcodeInput = By.CssSelector("input[data-qa='zipcode']");
        private readonly By _mobileNumberInput = By.CssSelector("input[data-qa='mobile_number']");
        private readonly By _createAccountButton = By.CssSelector("button[data-qa='create-account']");
        private readonly By _errorMessage = By.CssSelector(".signup-form p");
        private readonly By _accountCreatedMessage = By.CssSelector("h2[data-qa='account-created']");

        public SignupPage(IWebDriver driver) : base(driver)
        {
        }

        public void FillSignupForm(string name, string email, string password)
        {
            // İlk adım - Temel bilgiler
            SendKeys(_nameInput, name);
            SendKeys(_emailInput, email);
            Console.WriteLine("Adım: Ad ve e-posta girildi");
            Click(_signupButton);
            Thread.Sleep(3000);

            // İkinci adım - Detaylı bilgiler
            Click(_titleMr);
            SendKeys(_passwordInput, password);
            var daySelect = new SelectElement(Driver.FindElement(_daySelect));
            daySelect.SelectByValue("1");
            var monthSelect = new SelectElement(Driver.FindElement(_monthSelect));
            monthSelect.SelectByValue("1");
            var yearSelect = new SelectElement(Driver.FindElement(_yearSelect));
            yearSelect.SelectByValue("1990");
            Console.WriteLine("Adım: Şifre ve doğum tarihi girildi");

            // Checkbox'lara tıklamadan geç

            // Adres bilgilerini doldur
            SendKeys(_firstNameInput, "Ali");
            SendKeys(_lastNameInput, "Veli");
            SendKeys(_companyInput, "Test Ltd");
            SendKeys(_address1Input, "123 Main St");
            Console.WriteLine("Adım: Adres1 girildi");
            SendKeys(_address2Input, "Apt 4B");
            Console.WriteLine("Adım: Adres2 girildi");
            var countrySelect = new SelectElement(Driver.FindElement(_countrySelect));
            countrySelect.SelectByText("United States");
            SendKeys(_stateInput, "NY");
            SendKeys(_cityInput, "New York");
            SendKeys(_zipcodeInput, "10001");
            SendKeys(_mobileNumberInput, "5551234567");
            Console.WriteLine("Adım: Adres bilgileri girildi");

            // Hesabı oluştur
            WaitForElement(_createAccountButton);
            var createAccountButton = Driver.FindElement(_createAccountButton);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", createAccountButton);
            Thread.Sleep(1000);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", createAccountButton);
            Console.WriteLine("Adım: Create Account tıklandı");
            Thread.Sleep(3000);

            // 10 saniyeye kadar 'Account Created!' mesajını bekle, yoksa hata mesajı kontrol et
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
                wait.Until(d => d.FindElement(_accountCreatedMessage).Displayed);
            }
            catch
            {
                // Hata mesajı var mı kontrol et
                try
                {
                    var error = GetErrorMessage();
                    Console.WriteLine("Hata mesajı: " + error);
                }
                catch
                {
                    Console.WriteLine("Hesap oluşturulamadı, hata mesajı da bulunamadı.");
                }
                throw;
            }
        }

        public string GetErrorMessage()
        {
            return GetText(_errorMessage);
        }
    }
} 