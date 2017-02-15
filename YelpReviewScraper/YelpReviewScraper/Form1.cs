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
            //driverGC.FindElement(By.XPath("//*[@id='wrap']/div[4]/div[1]/div/div[2]/div/div[2]/div[1]/ul/li[4]")).Click();
            //driverGC.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(15));
            //driverGC.FindElement(By.XPath("//*[@id='wrap']/div[4]/div[1]/div/div[2]/div/div[2]/div[2]/div[1]/ul/li[2]/label/span")).Click();
            //driverGC.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            var starRating = driverGC.FindElement(By.ClassName("i-stars"));
            MessageBox.Show(starRating.GetAttribute("title"));
            driverGC.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            var bizLocation = driverGC.FindElement(By.ClassName("secondary-attributes"));
            MessageBox.Show(bizLocation.Text);
            
            
        }
    }
}