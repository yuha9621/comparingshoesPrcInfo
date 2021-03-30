using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using comparePrice.front;
using comparePrice.service;
using comparePrice.vo;
using DiscordWebhook;
using Newtonsoft.Json;

//Selenium Library
using OpenQA.Selenium.Chrome;

namespace comparePrice
{
    public partial class Form1 : Form
    {/*-------------------------*/
        protected ChromeDriverService _driverService = null;
        protected ChromeOptions _options = null;
        protected ChromeDriver _driver = null;

        private HttpClient _httpClient;
        /*---------------------------------*/
        List<ManualResetEvent> evetList = new List<ManualResetEvent>();
        public int cnt = 0;


        //GettingInfoServiceImpl gettingInfoService = new GettingInfoServiceImpl();
        ProcessingDataServiceImpl processingDataService = new ProcessingDataServiceImpl();

        public Form1()
        {
            InitializeComponent();

            _driverService = ChromeDriverService.CreateDefaultService();
            _driverService.HideCommandPromptWindow = true;

            _options = new ChromeOptions();
           // _options.AddArgument("disable-gpu");

           


    }
		private void exec(object sender, EventArgs e)
        {
            // [Thread start]
            
            //getting rawData => processingData => MstInfo
            //form2(input)
            SearchVo searchVo = new SearchVo();
            
            
            //form3(exec): 상세
            MstInfo mstInfo = processingDataService.comparingData(searchVo);
            searchVo.mstInfo = mstInfo;
            
            //webhook start
            //SearchVo searchVo = new SearchVo();
            _httpClient = new HttpClient();
            string url = "https://discordapp.com/api/webhooks/797639331011231744/GN1RSE99hoWUw4ga9PdJi2XuQp9ZvNyi-jajXfQtKdA5e7G1lxIvDFfIyEZQQV85E3kD";
            searchVo.webHookUrl = url;
            Webhook webhook = processingDataService.makeMstContent(searchVo);
            webhook.Username = "yuha";

            var content = new StringContent(JsonConvert.SerializeObject(webhook), Encoding.UTF8, "application/json");
            var response= _httpClient.PostAsync(url, content).Result;

            string test = "";

            /*
            //sending to discord START
            WebClient dwebClient = new WebClient();
            //string url = "https://discordapp.com/api/webhooks/797639331011231744/GN1RSE99hoWUw4ga9PdJi2XuQp9ZvNyi-jajXfQtKdA5e7G1lxIvDFfIyEZQQV85E3kD";
            NameValueCollection discordValues = new NameValueCollection();

            Webhook web = processingDataService.makeMstContent(searchVo);

            discordValues.Add("username", "yuhaFromC#");
            discordValues.Add("content", content);

            dwebClient.UploadValues(url,discordValues);
            //sending to discord END
            */
            //24시간 단위

            //[Thread End] --특정 스레드 종료 버튼을 누른 경우

            return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            Form2 frm2 = new Form2();
            frm2.ShowDialog();
            Form3 frm3= new Form3();
            frm3.ShowDialog(); */

            //evetList.Add(new ManualResetEvent(false));

            Thread cur_thread = Thread.CurrentThread;
            int i = cur_thread.ManagedThreadId;

            Debug.WriteLine("buttonclick[" + cnt +"]"+"["+i+"]");

    }

        private void button3_Click(object sender, EventArgs e)
        {
            GettingInfoServiceImpl gettingInfoService = new GettingInfoServiceImpl();
            SearchVo searchVo = new SearchVo();

            gettingInfoService.crawlingFromkream(searchVo);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

    }
}

/*
 사이즈 맵핑 constants 클래스
 웹훅 작성
 네이버 환율
 스레드 관리 창
 */