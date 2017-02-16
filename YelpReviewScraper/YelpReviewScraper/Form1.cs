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
using System.IO;

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
            String locationText = myLocationSpot.Text;
            String productText = myProductSpot.Text;
            
            driverGC.FindElement(By.CssSelector("#header-search-submit")).Click();
            driverGC.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            List<IWebElement> shittyBiz = new List<IWebElement>();
            List<String> newShitBiz = new List<string>();
            var numPages = (driverGC.FindElement(By.CssSelector(".page-of-pages")).Text);
            numPages = Regex.Match(numPages, @"\d+$").Value;
            int numberPages = Int32.Parse(numPages);
            MessageBox.Show(numberPages.ToString());

            for (int i = 1; i <= 2; i++)
            {
                Uri myURL = new Uri("https://www.yelp.com/search?find_desc=" + myProduct.Text + "&find_loc=" + myLocation.Text + "&start=" + (i * 10), UriKind.Absolute);
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
                    //IWebElement newStarRating;
                    try
                    {
                        starRating = myEles[j].FindElement(By.CssSelector("div.biz-rating > div.i-stars")).GetAttribute("title");
                    }
                    catch (OpenQA.Selenium.NoSuchElementException)
                    {
                        //MessageBox.Show("No stars");
                        continue;
                    }
                    starRating = Regex.Replace(starRating, @"[A-Za-z\s]", string.Empty);
                    float stars = float.Parse(starRating);
                    //MessageBox.Show(stars.ToString());

                    if (stars <= 3)
                    {
                        newShitBiz.Add(starRating);
                        //MessageBox.Show("Shitty");
                        driverGC.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                        var bizName = myEles[j].FindElement(By.CssSelector(".biz-name"));
                        //MessageBox.Show(bizName.Text);
                        newShitBiz.Add(bizName.Text);
                        var bizLocation = myEles[j].FindElement(By.CssSelector(".secondary-attributes"));
                        //MessageBox.Show(bizLocation.Text);
                        newShitBiz.Add(bizLocation.Text);
                        newShitBiz.Add("");
                    }
                    else
                    {
                       // MessageBox.Show("Too good");
                        var bizName = myEles[j].FindElement(By.CssSelector(".biz-name"));
                       // MessageBox.Show(bizName.Text);
                        driverGC.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));                       
                    }
                    
                }
                //MessageBox.Show("Ready for next page");
                driverGC.Navigate().GoToUrl(myURL);
            }
            using (StreamWriter writer = new StreamWriter(@"C:\Users\Justin\Desktop\newFile.txt"))
            {
                foreach (string s in newShitBiz)
                {
                    // writer.Write(s); // Writes in same line
                    writer.WriteLine(s);// Writes in next line
                }

            }
            MessageBox.Show("End");
            driverGC.Quit();
        }
    }
}