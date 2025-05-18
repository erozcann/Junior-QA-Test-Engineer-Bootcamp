using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace AutomationTests.Utils
{
    public static class ExtentManager
    {
        private static ExtentReports _extent;
        private static ExtentSparkReporter _sparkReporter; // Değişiklik burada

        public static ExtentReports GetExtent()
        {
            if (_extent == null)
            {
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestResults", "ExtentReport.html");
                Console.WriteLine("Rapor yolu: " + fullPath);  // 🔥 BU SATIR

                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                _sparkReporter = new ExtentSparkReporter(fullPath);
                _extent = new ExtentReports();
                _extent.AttachReporter(_sparkReporter);
            }
            return _extent;
        }

    }
}