# Ivan's Fetch SDET Coding Challenge
This is a submission of the Fetch SDET coding challenge. This will be written from the viewpoint of a Windows 11 installation, but this should be applicable to any operating system of your choosing.

## Requirements
The installations of the two programs are needed below:

Google Chrome - https://www.google.com/chrome/browser-tools/

Microsoft Visual Studio Community Edition - https://visualstudio.microsoft.com/downloads/

## Google Chrome Installation
Click on the link above to download Google Chrome. There should be a button on the top right corner to download Google Chrome on your preferred operating system. Download and install the application.

To verify its installation, open Google Chrome and head to the Fetch SDET challenge website: http://sdetchallenge.fetch.com/ . If it loads successfully, then Google Chrome has been installed successfully.

## Microsoft Visual Studio Installation
Click on the link above to download Microsoft Visual Studio. Download the community edition for your preferred operating system. Then, open up the installation file. 

When installing Microsoft Visual Studio, select the .NET desktop development option to be able to run the automation file. It will take several minutes and about 7.13 GB of space is required to be free on your drive as of the time of this writing. 

In the meanwhile, the code can be downloaded from this page: https://github.com/IvanBrenes5/FetchSDETCodingChallenge . In the main code page, click on the "Code" button and download the code as a .zip file. From your downloads folder, right click the .zip file and extract its contents into a separate folder.

Upon Microsoft Visual Studio's successful installation, go into the folder extracted from the zip file and find the IvansFetchCodingChallenge.sln file. Double-click it using Microsoft Visual Studio as the program to open the solution file. 

Upon opening the project, there might be a message saying "This project is targeting a version of .NET which is not installed." Use the provided install button to install the required version. This will also restart Visual Studio once the installation is finished. If this message does not appear, we can carry onto the next section.

On the solution explorer window to the right side of the screen, click on the triangle button to show the list of files contained. Right click on the project line called "IvansFetchCodingChallenge" and select "Manage NuGet Packages". The following packages should be installed for the automation to run successfully:
- Microsoft.NET.Test.Sdk by Microsoft
- NUnit by Charlie Poole, Rob Prouse
- NUnit.Analyzers by NUnit
- NUnit3TestAdapter by Charlie Poole, Terje Sandstrom
- Selenium.Support by Selenium Committers
- Selenium.WebDriver by Selenium Committers
- WebDriverManager by WebDriverManager

Once that is confirmed, click on the "View" button at the top and select "Test Explorer". In the test window, open the dropdowns to find a test called "Find the gold bar that weighs the least, with minimal weighings". It should be the only test. Click on the green triangle to begin the test. The initial build might take a while, but afterward, it should be able to run successfully without error.

To be able to view the output, click on the "View" button at the top and select "Output". In this window, click on the dropdown box and select "Tests" to be able to view the output of the test. It will show messages including:
 - Contents of the alert message
 - The gold bar that is the least weighed among the gold bars
 - The total amount of weighings. It should be a max of two weighings from the current code.
 - The list of weighing results.

 And that is all there is to it. I had a good time writing the automation for this challenge and I hope you enjoyed looking through the project. I am excited at the chance for this opportunity for a role at Fetch, and I look forward to hearing back from you. Until then, have a good one.

 ~Ivan Brenes