using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

//***********************************
//* Created by: Ivan Brenes         *
//* Date created: 07/21/2024        *
//* Total runtime: 15.3 seconds     *
//***********************************

namespace IvansFetchCodingChallenge
{
    /// <summary>
    /// This class is responsible for the automation test of the Fetch SDET challenge website. 
    /// </summary>
    public class FetchTestingAutomation : SuperAutomationHandler
    {
        #region Initialization, Setup, and Teardown

        /// <summary>
        /// An object responsible for the "By" locators when attempting to interact with the website.
        /// </summary>
        FetchLocators locators;

        /// <summary>
        /// An object necessary to induce waiting until certain actions have taken place.
        /// </summary>
        DefaultWait<IWebDriver> wait;

        /// <summary>
        /// An integer used to get the amount of weighings. Used for waiting until the weighing process has finished.
        /// </summary>
        int lastWeighingsAmount;

        [OneTimeSetUp]
        public void FetchOneTimeSetup()
        {
            //Initialize the object containing the webpage locators.
            locators = new FetchLocators();

            //Initialize the waiting object.
            wait = new DefaultWait<IWebDriver>(driver);
            wait.Timeout = TimeSpan.FromSeconds(10);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
        }

        [SetUp]
        public void Setup()
        {
            //Tell the browser to go to the Fetch challenge site.
            driver.Url = "http://sdetchallenge.fetch.com/";
        }

        [TearDown]
        public void Teardown()
        {
            //Reset the game boards.
            ClickAndWaitUntilResettingBoardsHaveFinished();
            lastWeighingsAmount = 0;
        }
        #endregion

        #region Fetch Test Automation
        [Test]
        [TestCase(TestName = "Find the gold bar that weighs the least, with minimal weighings.")]
        public void FetchMinimalBarWeighing()
        {
            #region Arrange the test case.
            //This will store the amount of weighings that are conducted.
            IList<IWebElement> weighingsList = driver.FindElements(locators.weighings);

            //This will be incredibly useful, as the test will need to wait until the count updates to proceed in checking the result of weighing.
            lastWeighingsAmount = weighingsList.Count;

            //Create references to the gold bars; one list will hold every bar, and 
            IList<IWebElement> goldBarsList = driver.FindElements(locators.goldBars);

            if(goldBarsList.Count != 9)
            {
                string errorMessage = "The amount of gold bars are not equal to the requirement of 9 gold bars.";
                TestContext.Error.WriteLine(errorMessage);
                throw new Exception(errorMessage);
            }

            //An even division of 3 sets of 3 gold bars will be stored.
            List<IWebElement> firstSetOfBars = new List<IWebElement>() { };
            List<IWebElement> secondSetOfBars = new List<IWebElement>() { };
            List<IWebElement> thirdSetOfBars = new List<IWebElement>() { };
            DivideGoldBarsIntoSets(goldBarsList, firstSetOfBars, secondSetOfBars, thirdSetOfBars);

            //Create references of the inputs for each of the boards.
            IList<IWebElement> leftGameBoardSpaces = driver.FindElements(locators.leftGameBoard);
            IList<IWebElement> rightGameBoardSpaces = driver.FindElements(locators.rightGameBoard);
            #endregion

            #region Act: Discover the gold bar that weighs the least.
            //Fill in the game board with the first set of bars on the left and second set on the right.
            for (int index = 0; index < firstSetOfBars.Count; index++)
            {
                string barNumber = firstSetOfBars[index].Text;
                leftGameBoardSpaces[index].SendKeys(barNumber);
            }
            for (int index = 0; index < secondSetOfBars.Count; index++)
            {
                string barNumber = secondSetOfBars[index].Text;
                rightGameBoardSpaces[index].SendKeys(barNumber);
            }

            //This will click the weigh button and wait until the amount of weighings has updated to see the result.
            string compareResult = ClickAndWaitUntilWeighingHasFinished();

            //Store a reference to the least weighed gold bar.
            IWebElement leastWeighedGoldBar;

            if (compareResult.Equals(">"))
            {
                leastWeighedGoldBar = CompareGoldBars(secondSetOfBars, leftGameBoardSpaces, rightGameBoardSpaces);

            }
            else if (compareResult.Equals("<"))
            {
                leastWeighedGoldBar = CompareGoldBars(firstSetOfBars, leftGameBoardSpaces, rightGameBoardSpaces);
            }
            else if (compareResult.Equals("="))
            {
                leastWeighedGoldBar = CompareGoldBars(thirdSetOfBars, leftGameBoardSpaces, rightGameBoardSpaces);
            }
            else
            {
                leastWeighedGoldBar = null;
                string errorMessage = $"Unexpected compare result was detected: {compareResult}";
                TestContext.Error.WriteLine(errorMessage);
                throw new Exception(errorMessage);
            }

            //Update the weighingsList.
            weighingsList = driver.FindElements(locators.weighings);

            //Click on the gold bar that weighs less than the others.
            leastWeighedGoldBar.Click();
            #endregion

            #region Assert that the alert message appears and log the relevant messages.
            //Tell the browser driver to save and handle the alert message that appears.
            string popupMessage = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();
            Assert.IsNotNull(popupMessage);

            TestContext.Progress.WriteLine($"In quotes, the popup message was: '{popupMessage}'");
            TestContext.Progress.WriteLine($"In quotes, the gold bar that weighed least was: '{leastWeighedGoldBar.Text}'");
            TestContext.Progress.WriteLine($"In quotes, the total amount of weighings were: '" + weighingsList.Count + "'");
            TestContext.Progress.WriteLine("The weighings list is displayed below: ");
            foreach (IWebElement weighing in weighingsList)
            {
                TestContext.Progress.WriteLine(weighing.Text);
            }
            #endregion
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// This method is used to divide the gold bars into the 3 sets given from the arguments.
        /// </summary>
        /// <param name="goldBarsList">The total amount of gold bars from the webpage.</param>
        /// <param name="firstSetOfBars">The first set of gold bars to be populated.</param>
        /// <param name="secondSetOfBars">The second set of gold bars to be populated.</param>
        /// <param name="thirdSetOfBars">The third set of gold bars to be populated.</param>
        private void DivideGoldBarsIntoSets(IList<IWebElement> goldBarsList, List<IWebElement> firstSetOfBars, List<IWebElement> secondSetOfBars, List<IWebElement> thirdSetOfBars)
        {
            for (int index = 0; index < goldBarsList.Count; index++)
            {
                if (index % 3 == 0)
                {
                    firstSetOfBars.Add(goldBarsList[index]);
                }
                else if (index % 3 == 1)
                {
                    secondSetOfBars.Add(goldBarsList[index]);
                }
                else
                {
                    thirdSetOfBars.Add(goldBarsList[index]);
                }
            }
        }

        /// <summary>
        /// This method clicks the weigh button and waits until the weighed list is populated with an additional spot to be able to view the comparation result.
        /// </summary>
        /// <returns>Comparation result between the two game boards.</returns>
        private string ClickAndWaitUntilWeighingHasFinished()
        {
            //Click the weigh button.
            driver.FindElement(locators.weighButton).Click();

            //Wait until the amount of weighings increases by 1.
            wait.Until(x => x.FindElements(locators.weighings).Count == (lastWeighingsAmount + 1));
            lastWeighingsAmount = lastWeighingsAmount + 1;

            return driver.FindElement(locators.resultButton).Text;
        }

        /// <summary>
        /// This method clicks the reset button and waits until the game boards are cleared.
        /// </summary>
        private void ClickAndWaitUntilResettingBoardsHaveFinished()
        {
            //Reset the game boards.
            driver.FindElement(locators.resetButton).Click();

            //Wait until the board has been reset. The first space will usually have a value, so seeing it as a blank will indicate a successful reset.
            wait.Until(x => x.FindElements(locators.leftGameBoard)[0].Text.Equals(""));
        }

        /// <summary>
        /// This method contains the second weighing operation to determine which gold bar in the isolated set is the one that weighs least.
        /// </summary>
        /// <param name="goldBarsSet">The set of gold bars with a bar weighing least among the rest of the gold bars.</param>
        /// <param name="leftBoard">A reference to populate the left game board.</param>
        /// <param name="rightBoard">A reference to populate the right game board.</param>
        /// <returns></returns>
        private IWebElement CompareGoldBars(List<IWebElement> goldBarsSet, IList<IWebElement> leftBoard, IList<IWebElement> rightBoard)
        {
            //Reset the game boards.
            ClickAndWaitUntilResettingBoardsHaveFinished();

            //Take the first bar and compare it against the second bar.
            leftBoard[0].SendKeys(goldBarsSet[0].Text);
            rightBoard[0].SendKeys(goldBarsSet[1].Text);
            string compareResult = ClickAndWaitUntilWeighingHasFinished();

            if (compareResult.Equals(">"))
            {
                return goldBarsSet[1];
            }
            else if (compareResult.Equals("<"))
            {
                return goldBarsSet[0];
            }
            else
            {
                return goldBarsSet[2];
            }
        }
        #endregion
    }
}
