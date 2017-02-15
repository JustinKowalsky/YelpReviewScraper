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
            
            IWebElement myProductSpot = driverGC.FindElement(By.Id("find_desc"));
            myProductSpot.SendKeys(myProduct.Text);
            IWebElement myLocationSpot = driverGC.FindElement(By.Id("dropperText_Mast"));
            myLocationSpot.Clear();
            myLocationSpot.SendKeys(myLocation.Text);
            driverGC.FindElement(By.Id("header-search-submit")).Click();
            driverGC.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            //var dataKey =  driverGC.FindElement(By.ClassName("search-result")).GetAttribute("data-key");
            //IList<IWebElement> dataKeys = driverGC.FindElement(By.ClassName("search-result")).GetAttribute("data-key" + i).ToList();
            //MessageBox.Show(dataKeys.ToString());
            var dataKey = driverGC.FindElement(By.ClassName("search-result")).GetAttribute("data-key");
            int newDataKey = Int32.Parse(dataKey);
            MessageBox.Show(dataKey.ToString());
            for (int i=1; i<10; i++)
            {
                var starRating = driverGC.FindElement(By.ClassName("i-stars"));
                MessageBox.Show(starRating.GetAttribute("title"));
                driverGC.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
                var bizLocation = driverGC.FindElement(By.ClassName("secondary-attributes"));
                MessageBox.Show(bizLocation.Text);
                newDataKey = Int32.Parse(dataKey) + i;
                MessageBox.Show(newDataKey.ToString());

            }     
            
        }
    }
}