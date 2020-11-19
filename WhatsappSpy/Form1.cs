using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhatsappSpy
{
    public partial class Form1 : Form
    {

        public Form1()
        {


            InitializeComponent();
          
        }
        IWebDriver driver;
        public void CreateDriver()
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            driver = new ChromeDriver(service);
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
            CreateDriver();
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
                        string cevrimici = driver.FindElement(By.CssSelector("#main > header > div._33QME > div._2ruUq._3xjAz > span")).Text;
                        if (cevrimici == "çevrimiçi")
                        {
                            if (durum!="çevrimiçi")
                            {
                                durum = "çevrimiçi";
                                logList.Items.Add("Numara çevrimiçi =>" + DateTime.Now);
                            }
                        }
                    }
                   
                }
                else
                {
                    if (durum!="çevrimdışı")
                    {
                        durum = "çevrimdışı";
                        logList.Items.Add("Numara çevrimdışı =>" + DateTime.Now);

                    }
                }

            }
            catch (Exception ex)
            {

                logList.Items.Add(ex.Message);
            }


        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {

            try
            {
                string yol = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\WpCasus-durum.txt";
                using (StreamWriter sw= new StreamWriter(yol))
                {
                    foreach (var item in logList.Items)
                    {
                        sw.WriteLine(item.ToString());

                    }
                    sw.Close();
                    MessageBox.Show("Kaydedildi...");
                }
               

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void tmrKontrol_Tick(object sender, EventArgs e)
        {
            Kontrol();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            logList.Items.Clear();
        }

        private void btnKaydet_Click_1(object sender, EventArgs e)
        {
            try
            {
                string yol = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\WpCasus-durum.txt";
                using (StreamWriter sw = new StreamWriter(yol))
                {
                    foreach (var item in logList.Items)
                    {
                        sw.WriteLine(item.ToString());

                    }
                    sw.Close();
                    MessageBox.Show("Kaydedildi...");
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }


        
    }
}

