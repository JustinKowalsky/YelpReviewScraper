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
            var myEles = driverGC.FindElements(By.CssSelector("div.search-result"));
            
            foreach (IWebElement business in myEles)
            {
                var starRating = business.FindElement(By.CssSelector("div.biz-rating > div.i-stars")).GetAttribute("title");
                starRating = Regex.Replace(starRating, @"[A-Za-z\s]", string.Empty);
                float stars = float.Parse(starRating);
                MessageBox.Show(stars.ToString());
                if (stars <= 3)
                {
                    MessageBox.Show("Shitty");
                    
                }
                else
                {
                    driverGC.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                    var bizLocation = business.FindElement(By.CssSelector(".secondary-attributes"));
                    MessageBox.Show(bizLocation.Text);
                    
                }
                
            }     
        }
    }
}