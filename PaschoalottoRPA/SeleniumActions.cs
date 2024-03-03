using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using SeleniumExtras.WaitHelpers;
using System.Text.RegularExpressions;




namespace PaschoalottoRPA
{
    internal class SeleniumActions
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public SeleniumActions()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
        }

        public (int wpmData, int keyStrokesData, float accuracyData, int correctWordsData, int wrongWordsData, string status) GetDataFromWebsite()
        {
            try
            {
                OpenAndPrepareWebsite("https://10fastfingers.com/typing-test/portuguese");

                ReadOnlyCollection<IWebElement> words = FindElements("//div[@id='row1']/span");

                IWebElement wordInput = FindElement("//input[@id='inputfield']");

                foreach (var word in words)
                {
                    wordInput.SendKeys(word.Text);
                    wordInput.SendKeys(Keys.Space);
                }

                wait.Until(ExpectedConditions.AlertIsPresent());
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();


                var data = GetData();

                return data;
            }
            catch (Exception)
            {

                return (0, 0, 0, 0, 0, "Error");
            }


        }
        public void OpenAndPrepareWebsite(string url)
        {
            
            driver.Navigate().GoToUrl(url);
            try
            {
                IWebElement allowCookies = FindElement("//div[@id='CybotCookiebotDialogBodyButtonsWrapper']/button[text() = 'Allow all']");
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].click();", allowCookies);
            }
            catch (Exception)
            {
                //Continue code...
            }
        }

        public IWebElement FindElement(string xpath)
        {
            IWebElement element = driver.FindElement(By.XPath(xpath));
            return element;
        }

        public ReadOnlyCollection<IWebElement> FindElements(string xpath)
        {
            ReadOnlyCollection<IWebElement> elements = driver.FindElements(By.XPath(xpath));

            return elements;
        }

        public (int wpmData, int keyStrokesData, float accuracyData, int correctWordsData, int wrongWordsData, string status) GetData()
        {

            try
            {
                IWebElement wpm = FindElement("//td[@id='wpm']");
                IWebElement keyStrokes = FindElement("//tr[@id='keystrokes']/td[@class='value']");
                IWebElement accuracy = FindElement("//tr[@id='accuracy']/td[@class='value']");
                IWebElement correctWords = FindElement("//tr[@id='correct']/td[@class='value']");
                IWebElement wrongWords = FindElement("//tr[@id='wrong']/td[@class='value']");


                int wpmData = int.Parse(wpm.Text.Substring(0, 3));
                int keyStrokesData = int.Parse(Regex.Replace(keyStrokes.Text, @"\([^)]*\)", "").Trim());
                float accuracyData = float.Parse(accuracy.Text.Replace("%", ""));
                int correctWordsData = int.Parse(correctWords.Text);
                int wrongWordsData = int.Parse(wrongWords.Text);


                return (wpmData, keyStrokesData, accuracyData, correctWordsData, wrongWordsData, "Success");
            }
            catch (Exception)
            {

                return (0, 0, 0, 0, 0, "Error");

            }
        }
    }
}
