using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Text.RegularExpressions;

namespace YelpReviewScraper
{
    public partial class Form1 : Form
    {

        static IWebDriver driverGC;
        public Form1()
        {
            driverGC = new ChromeDriver(@"Z:\Justin\Documents\Visual Studio 2015\chromedriver_win32");
            driverGC.Navigate().GoToUrl("https://www.yelp.com");
            InitializeComponent();
        }

        private void startSearch_Click(object sender, EventArgs e)
        {
            //searches product and company and clicks search
            IWebElement myProductSpot = driverGC.FindElement(By.CssSelector("#find_desc"));
            myProductSpot.SendKeys(myProduct.Text);
            IWebElement myLocationSpot = driverGC.FindElement(By.CssSelector("#dropperText_Mast"));
            myLocationSpot.Clear();
            myLocationSpot.SendKeys(myLocation.Text);
            driverGC.FindElement(By.CssSelector("#header-search-submit")).Click();
            driverGC.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));

            List<IWebElement> shittyBiz = new List<IWebElement>();
            //var myEles = driverGC.FindElements(By.CssSelector("div.search-result"));
            for (int i = 0; i <= 1000; i++)
            {
                driverGC.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                var myEles = driverGC.FindElements(By.CssSelector("div.search-result"));
                int size = 1;
                for (int j = 0; j < size; ++j)
                {
                    driverGC.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                    myEles = driverGC.FindElements(By.CssSelector("div.search-result"));
                    size = myEles.Count();

                    driverGC.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
                    var starRating = " ";
                    try
                    {
                        starRating = myEles[j].FindElement(By.CssSelector("div.biz-rating > div.i-stars")).GetAttribute("title");
                    }
                    catch (OpenQA.Selenium.NoSuchElementException)
                    {
                        MessageBox.Show("No stars");
                        continue;
                    }
                    catch (OpenQA.Selenium.StaleElementReferenceException)
                    {
                        MessageBox.Show("Stale");
                        continue;
                    }
                    starRating = Regex.Replace(starRating, @"[A-Za-z\s]", string.Empty);
                    float stars = float.Parse(starRating);
                    MessageBox.Show(stars.ToString());

                    if (stars <= 3)
                    {
                        //shittyBiz.Add(starRating);
                        MessageBox.Show("Shitty");
                        driverGC.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                        var bizName = myEles[j].FindElement(By.CssSelector(".biz-name"));
                        MessageBox.Show(bizName.Text);
                        shittyBiz.Add(bizName);
                        var bizLocation = myEles[j].FindElement(By.CssSelector(".secondary-attributes"));
                        MessageBox.Show(bizLocation.Text);
                        shittyBiz.Add(bizLocation);
                    }
                    else
                    {
                        var bizName = myEles[j].FindElement(By.CssSelector(".biz-name"));
                        MessageBox.Show(bizName.Text);
                        driverGC.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                        MessageBox.Show("Too good");
                    }

                }
                try
                {
                    driverGC.FindElement(By.CssSelector("div.arrange_unit > a.next")).Click();
                }
                catch (OpenQA.Selenium.NoSuchElementException)
                {
                    MessageBox.Show("No more pages");
                    return;
                    //driverGC.Quit();
                }
            }

        }
    }
}