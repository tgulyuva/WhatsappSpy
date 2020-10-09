using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhatsappSpy
{
    public partial class Form1 : Form
    {
        IWebDriver driver = new ChromeDriver();

        public Form1()
        {
           

            InitializeComponent();
          
        }

        public bool IsTestElementPresent(By element)
        {
            try
            {
                driver.FindElement(element);
                return true;
                
            }
            catch (NoSuchElementException)
            {

                return false;
            }



        }

        public async void button1_Click(object sender, EventArgs e)
        {
           
            await Task.Run(async () => {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Lütfen Numara Giriniz.");
                    return;
                }
                else {
                    driver.Navigate().GoToUrl("https://web.whatsapp.com/");
                    await Task.Delay(10000);
                    driver.Navigate().GoToUrl("https://api.whatsapp.com/send?phone="+textBox1);
                    driver.FindElement(By.CssSelector("#action-button")).Click();
                    await Task.Delay(2000);
                    driver.FindElement(By.CssSelector("#fallback_block > div > div > a")).Click();


                }

            });
            tmrKontrol.Enabled = true;
            
           
            }
        string durum = "";
        public void Kontrol()
        {
            try
            {
                if (IsTestElementPresent(By.CssSelector("#main > header > div._33QME > div._2ruUq._3xjAz"))) 
                {
                    if (IsTestElementPresent(By.CssSelector("#main > header > div._33QME > div._2ruUq._3xjAz > span")))
                    {
                        string cevrimici = driver.FindElement(By.CssSelector("main > header > div._33QME > div._2ruUq._3xjAz > span")).Text;
                        if (cevrimici == "çevrimiçi")
                        {
                            if (durum!="çevrimiçi")
                            {
                                durum = "çevrimiçi";
                                logList.Items.Add("Numara çevrimiçi =>" + textBox1 + "=>" + DateTime.Now);
                            }
                        }
                    }
                   
                }
                else
                {
                    if (durum!="çevrimdışı")
                    {
                        durum = "çevrimdışı";
                        logList.Items.Add("Numara çevrimdışı =>" + textBox1 + "=>" + DateTime.Now);

                    }
                }

            }
            catch (Exception ex)
            {

                logList.Items.Add(ex.Message);
            }


            }
        //#main > header > div._33QME > div._2ruUq._3xjAz
        //#main > header > div._33QME > div._2ruUq._3xjAz > span
    }
}

