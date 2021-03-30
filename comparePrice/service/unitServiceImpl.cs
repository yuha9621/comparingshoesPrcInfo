using comparePrice.vo;
using DiscordWebhook;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Timers;

namespace comparePrice.service
{
    public class unitServiceImpl : IDisposable
    {
        int no = 0;
        ProcessingDataServiceImpl processingDataService = new ProcessingDataServiceImpl();
        SearchVo searchVo = new SearchVo();
        private HttpClient _httpClient;
        System.Timers.Timer timer = new System.Timers.Timer();
        string state = "normal";

        ~unitServiceImpl() {
            Debug.WriteLine("hhhh");
        }

        public void createThread(object inputVo)
        {

            Thread cur_thread = Thread.CurrentThread;
            int i = cur_thread.ManagedThreadId;
            Debug.WriteLine("start : {0}",i);

            InputVo inputData = (InputVo)inputVo;

            searchVo.kreamUrl = inputData.kreamUrl;
            searchVo.stockXUrl = inputData.stockXUrl;
            searchVo.threadNo = inputData.no;
            
            timer.Interval = inputData.interval;
            timer.Elapsed += new ElapsedEventHandler(exectimerThread);
            timer.Start();

        }

        public void exectimerThread(object sender, ElapsedEventArgs e)
        {
            try {
                MstInfo mstInfo = processingDataService.comparingData(searchVo);
                searchVo.mstInfo = mstInfo;
                _httpClient = new HttpClient();
                //string url = "https://discordapp.com/api/webhooks/797639331011231744/GN1RSE99hoWUw4ga9PdJi2XuQp9ZvNyi-jajXfQtKdA5e7G1lxIvDFfIyEZQQV85E3kD";
                string url = "https://discord.com/api/webhooks/797639847309213706/nTgGce1csWdzq4kH912JMD5T99KJVNfqKik8zJ48qPPd924ThOOPGj65ZH1FWbE_V4RT";
                searchVo.webHookUrl = url;
                Webhook webhook = processingDataService.makeMstContent(searchVo);
                webhook.Username = "yuha";

                var content = new StringContent(JsonConvert.SerializeObject(webhook), Encoding.UTF8, "application/json");
                var response = _httpClient.PostAsync(url, content).Result;

                searchVo.mstInfo = null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("error!!");
                this.state = "error";
                this.Dispose();
            }

        }

        public void runOnce (object inputVo)
        {
            try 
            {
                Thread cur_thread = Thread.CurrentThread;
                int i = cur_thread.ManagedThreadId;
                Debug.WriteLine("start : {0}", i);

                InputVo inputData = (InputVo)inputVo;

                searchVo.kreamUrl = inputData.kreamUrl;
                searchVo.stockXUrl = inputData.stockXUrl;
                searchVo.threadNo = inputData.no;

                MstInfo mstInfo = processingDataService.comparingData(searchVo);
                searchVo.mstInfo = mstInfo;
                _httpClient = new HttpClient();
                //string url = "https://discordapp.com/api/webhooks/797639331011231744/GN1RSE99hoWUw4ga9PdJi2XuQp9ZvNyi-jajXfQtKdA5e7G1lxIvDFfIyEZQQV85E3kD";
                string url = "https://discord.com/api/webhooks/797639847309213706/nTgGce1csWdzq4kH912JMD5T99KJVNfqKik8zJ48qPPd924ThOOPGj65ZH1FWbE_V4RT";
                searchVo.webHookUrl = url;
                Webhook webhook = processingDataService.makeMstContent(searchVo);
                webhook.Username = "yuha";

                var content = new StringContent(JsonConvert.SerializeObject(webhook), Encoding.UTF8, "application/json");
                var response = _httpClient.PostAsync(url, content).Result;

                searchVo.mstInfo = null;
            }
            catch (Exception e) {
                Debug.WriteLine("error!!!");
                this.state = "error";
                this.Dispose();
            }
            
        }

        public void Dispose() {
            timer.Dispose();
            if (processingDataService != null) {
                processingDataService.quitWebDriver();
                processingDataService = null;
            }
            
        }
        public string getState()
        {
            return this.state;
        }
    }
}
