using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;

//***********************************
//* Created by: Ivan Brenes         *
//* Date created: 07/21/2024        *
//***********************************

namespace IvansFetchCodingChallenge
{
    /// <summary>
    /// This class handles the methods necessary to properly set up and teardown tests ran by its children.
    /// </summary>
    public class SuperAutomationHandler
    {
        //This is the driver responsible for interacting with the Google Chrome™ browser.
        public ChromeDriver driver;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            TestContext.Progress.WriteLine("Starting test...");

            //Initialize the driver and automatically use the right version to interact with.
            driver = new ChromeDriver();
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());

            //While the Fetch react site loads incredibly quick, it is a good idea to set a general amount of time to wait until an element is found.
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            //Maximize the window to be on the safe side and view as much of the webpage as possible.
            driver.Manage().Window.Maximize();
        }
         
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            try
            {
                driver.Close();
                TestContext.Progress.WriteLine("Testing has ended, and the Google Chrome browser has closed.");
            }
            catch (Exception ex)
            {
                TestContext.Error.WriteLine(ex.ToString());
                TestContext.Error.WriteLine("Catastrophically, the browser could not close. Attempting to shut down the WebDriver...");
            }
            finally
            {
                try
                {
                    driver.Quit();
                    TestContext.Progress.WriteLine("The background WebDriver executable has successfully stopped.");
                }
                catch (Exception exc)
                {
                    string errorMessage = "Catastrophically, the background WebDriver could not close.";
                    TestContext.Error.WriteLine(errorMessage);
                    throw new Exception(errorMessage);
                }
            }
        }
    }
}
