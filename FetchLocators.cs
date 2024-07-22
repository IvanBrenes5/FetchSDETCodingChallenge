using OpenQA.Selenium;

//***********************************
//* Created by: Ivan Brenes         *
//* Date created: 07/21/2024        *
//***********************************

namespace IvansFetchCodingChallenge
{
    /// <summary>
    /// This class stores locators for various webpage elements on the Fetch SDET challenge website.
    /// </summary>
    public class FetchLocators
    {
        /// <summary>
        /// Interacts with the reset button. There are two on the page, so the button with the reset text within is selected for use.
        /// </summary>
        public By resetButton = By.XPath("//button[text()='Reset']");

        /// <summary>
        /// Interacts with the weigh button via its id.
        /// </summary>
        public By weighButton = By.CssSelector("#weigh");

        /// <summary>
        /// Interacts with the result button from its parent class.
        /// </summary>
        public By resultButton = By.CssSelector(".result > button");

        /// <summary>
        /// Locates a list of buttons related to the gold bars.
        /// </summary>
        public By goldBars = By.CssSelector(".coins > *");

        /// <summary>
        /// Locates a list of the total amount of weighings done.
        /// </summary>
        public By weighings = By.CssSelector("div[class='game-info'] > ol > *");

        /// <summary>
        /// Locates the 9 available spaces on the left game board.
        /// </summary>
        public By leftGameBoard = By.CssSelector(".game-board > div > .board-row > input[data-side='left']");

        /// <summary>
        /// Locates the 9 available spaces on the right game board.
        /// </summary>
        public By rightGameBoard = By.CssSelector(".game-board > div > .board-row > input[data-side='right']");
    }
}
