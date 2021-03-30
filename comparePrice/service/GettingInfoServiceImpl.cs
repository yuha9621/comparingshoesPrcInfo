using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using comparePrice.serviceInterface;
using comparePrice.vo;
using Newtonsoft.Json.Linq;


//Selenium Library
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;

namespace comparePrice.service
{
    class GettingInfoServiceImpl : GettingInfoService
    {
        protected ChromeDriverService _driverService = null;
        protected ChromeOptions _options = null;
        protected ChromeDriver _driver = null;
        SizeConstantMap sizeConstantMap = new SizeConstantMap();
        int cnt1 = 0;

        public GettingInfoServiceImpl() {
            _driverService = ChromeDriverService.CreateDefaultService();
            _driverService.HideCommandPromptWindow = true;

            _options = new ChromeOptions();
            // _options.AddArgument("disable-gpu");
            
        }

        /*
       author : yuha
       funcName : crawlingFromkream
       summary : kream 크롤링
       input :  searchVo
       return : Dictionary<string, DetailInfo>
       */
        public Dictionary<string, DetailInfo> crawlingFromkream(SearchVo searchVo)
        {
            /*kream 정보 수집 시작*/ 
            _driver = new ChromeDriver(_driverService, _options);
            _driver.Navigate().GoToUrl(searchVo.kreamUrl); // 웹 사이트에 접속합니다.
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            //searchVo.kreamUrl = "https://kream.co.kr/products/23219";

            var thumbnailUrl = _driver.FindElementsByClassName("product_img")[0].GetAttribute("src");
            searchVo.thumbnailUrl = thumbnailUrl;
            string sizeType = searchVo.sizeType;

            Dictionary<string, string> krUsSizeMap = sizeConstantMap.getKrUsSizeMap();

            if (sizeType.Contains("W"))
            {
                krUsSizeMap = sizeConstantMap.getKrUsSizeWMap();
            }
            else if (sizeType.Contains("Y"))
            {
                krUsSizeMap = sizeConstantMap.getKrUsSizeYMap();
            }
            else if (searchVo.clothesYn) { 
                krUsSizeMap = sizeConstantMap.getClothesSizeMap();

            }

            var sizeButton = _driver.FindElement(By.XPath("//a[@class='btn_size']"));
            //var buttonList = _driver.FindElements(By.XPath("//button[@class='select_link']"));

            Dictionary<string, DetailInfo> InfoMap = new Dictionary<string, DetailInfo>();

            //buttonList[0].Click();
            sizeButton.Click();
            Thread.Sleep(5000);

            var buttonListHead = _driver.FindElement(By.XPath("//ul[@class='select_list']"));
            var buttonListCnt = buttonListHead.FindElements(By.XPath("//li[@class='select_item']")).Count;
            var buttonList= buttonListHead.FindElements(By.XPath("//li[@class='select_item']"));

            for (int i = 1; i < buttonList.Count; i++)
            {
                if (buttonList[i].GetAttribute("disabled")==null) {
                    buttonList[i].Click();
                    Thread.Sleep(5000);

                    string latestSalePrc = _driver.FindElement(By.XPath("//*[contains(@class,'detail_price')]")).FindElement(By.ClassName("num")).Text;
                    if (latestSalePrc.Equals("-")) {
                        latestSalePrc = "0";
                    }
                    string[] size = buttonList[i].FindElement(By.ClassName("size")).Text.Split('(');
                    var priceElement = _driver.FindElement(By.ClassName("high_price")).FindElement(By.ClassName("num"));
                    string price = priceElement.Text;
                    string usSize = krUsSizeMap[size[0]];
                    /*
                    if (size.Length > 1)
                    {
                        usSize = Regex.Replace(size[1], @"\D", "");
                    }*/
                    if (!price.Equals("-")) {
                        if (InfoMap.ContainsKey(usSize))
                        {
                            InfoMap[usSize] = new DetailInfo() { krSize = size[0], usSize = usSize, kreamKrPrice = float.Parse(price), kreamLatestKrPrice = float.Parse(latestSalePrc) };
                        }
                        else
                        {
                            InfoMap.Add(usSize, new DetailInfo() { krSize = size[0], usSize = usSize, kreamKrPrice = float.Parse(price), kreamLatestKrPrice = float.Parse(latestSalePrc) });
                        }

                    }
                }
            }

            Thread.Sleep(3000);
            _driver.Quit();

            return InfoMap;
        }

        /*
       author : yuha
       funcName : crawlingFromStockx
       summary : stockx 크롤링
       input : searchVo.useKrw
       return : Dictionary<string, DetailInfo>
       */
        public Dictionary<string, DetailInfo> crawlingFromStockx(SearchVo searchVo)
        {
            /*stockX 정보 수집 시작*/
            _driver = new ChromeDriver(_driverService, _options);

            string url = searchVo.stockXUrl;
            string after_url = searchVo.makeStockXUrl(url);
            int j=0;
            bool sizeDefault = false;
            searchVo.clothesYn = false;
            searchVo.sizeType = " ";

            // _driver.Navigate().GoToUrl(after_url); // 웹 사이트에 접속합니다.
            _driver.Navigate().GoToUrl(url); // 웹 사이트에 접속합니다.
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            Dictionary<string, string> usKrSizeMap = sizeConstantMap.getUsKrSizeMap();
            float usdkrw = searchVo.usdkrw;
            Thread.Sleep(3000);
            //첫 화면 팝업 
            var popElement = _driver.FindElementByXPath("//*[contains(@aria-label,'Close')]");
            popElement.Click();
            Thread.Sleep(5000);

            string prdNm = searchVo.prdNm;
            string sizeType = " ";

            /***2021-02-11 추가****////
            Dictionary<string, float> latestSalePrcMap = new Dictionary<string, float>();
            Dictionary<string, float> latestSalePrcDiffMap = new Dictionary<string, float>();
            var latestBtn = (_driver.FindElementsByXPath("//*[contains(@data-testid,'product-size-select')]"))[1];
            latestBtn.Click();
            Thread.Sleep(1000);

            var latestHead = (_driver.FindElementsByXPath("//*[contains(@class,'select-control')]"))[1];
            var latestList = latestHead.FindElements(By.XPath(".//*[contains(@role,'menuitem')]"));

            string sizeText2 = latestList[1].FindElement(By.ClassName("title")).Text.Replace("US ", "");

            if (sizeText2.Contains("W"))
            {
                sizeType = "W";
                searchVo.sizeType = sizeType;
                usKrSizeMap = sizeConstantMap.getUsKrSizeWMap();
            }
            else if (sizeText2.Contains("Y"))
            {
                sizeType = "Y";
                searchVo.sizeType = sizeType;
                usKrSizeMap = sizeConstantMap.getUsKrSizeYMap();
            }
            else if ("SMLXL".Contains(sizeText2))
            {

                sizeType = " ";
                searchVo.sizeType = sizeType;
                usKrSizeMap = sizeConstantMap.getClothesSizeMap();
                searchVo.clothesYn = true;

            }
            else
            {
                sizeDefault = true;
            }

            for (int i = 1; i < latestList.Count; i++)
            {
                latestList[i].Click();
                Thread.Sleep(2000);

                latestBtn.Click();
                Thread.Sleep(2000);
                string latestSalePrc = _driver.FindElementByXPath("//*[contains(@data-testid,'product-last-sale-button')]").FindElement(By.ClassName("sale-value")).Text;
                string buyPrc = _driver.FindElementsByXPath("//*[contains(@data-testid,'product-bidbuy-btn')]")[1].FindElement(By.ClassName("stat-value")).Text.Replace("$", "");
                string sizeKey;

                if (!latestSalePrc.Contains("-")) {
                    if (!buyPrc.Contains("-")) { 
                    latestSalePrc = latestSalePrc.Replace("$", "");
                    sizeKey= latestList[i].FindElement(By.ClassName("title")).Text.Replace("us ", "").Replace("US ", "").Replace(sizeType, "");

                    latestSalePrcMap.Add(sizeKey, float.Parse(latestSalePrc));
                    latestSalePrcDiffMap.Add(sizeKey, float.Parse(latestSalePrc) - float.Parse(buyPrc));
                    }
                }
            }


            _driver.Url = after_url;
            /***2021-02-11 추가 끝****/

            var li = _driver.FindElements(By.ClassName("tile-inner"));
            string sizeText = li[0].FindElement(By.ClassName("tile-value")).Text.Replace("US ", "");
            //List<string> testList = new List<string>();
            List<int> index = new List<int>();

            Dictionary<string, DetailInfo> InfoMap = new Dictionary<string, DetailInfo>();

            if (sizeText.Contains("W"))
            {
                sizeType = "W";
                searchVo.sizeType = sizeType;
                usKrSizeMap = sizeConstantMap.getUsKrSizeWMap();
            }
            else if (sizeText.Contains("Y"))
            {
                sizeType = "Y";
                searchVo.sizeType = sizeType;
                usKrSizeMap = sizeConstantMap.getUsKrSizeYMap();
            }
            else if ("SMLXL".Contains(sizeText))
            {
                
                sizeType = " ";
                searchVo.sizeType = sizeType;
                usKrSizeMap = sizeConstantMap.getClothesSizeMap();
                searchVo.clothesYn = true;

            }
            else {
                sizeDefault = true;
            }

                for (int i = 0; i < li.Count; i++)
                {
                    string price = li[i].FindElement(By.XPath("div[@class='tile-subvalue']/div")).Text.Replace("$", "");
                    float size ;
                    if (!price.Equals("Bid"))
                    {
                        if (sizeDefault)
                        {
                            size = float.Parse(li[i].FindElement(By.ClassName("tile-value")).Text.Replace("US ", "").Replace(sizeType, ""));
                            if (size >= 4 && size <= 12)
                                {
                                    index.Add(i);
                                }
                            }
                        else
                        {
                            index.Add(i);
                        }

                    }
                }
 

            foreach (int i in index) {
                var innerList = _driver.FindElements(By.ClassName("tile-inner"));
                string test = innerList[i].FindElement(By.ClassName("tile-value")).Text;
                string size = innerList[i].FindElement(By.ClassName("tile-value")).Text.Replace("US ","").Replace(sizeType, "");        
                try
                {
                    innerList[i].Click();
                }
                catch (ElementClickInterceptedException ex)
                {
                    _driver.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                    Thread.Sleep(1000);
                    innerList[i].Click();
                }
                Thread.Sleep(3000);

                var totalPrc = (_driver.FindElementsByXPath("//*[contains(@data-testid,'bid-total')]"))[2].Text.Replace("$","");
                InfoMap.Add(size, new DetailInfo() { usSize = size, krSize = usKrSizeMap[size], stockXUsPrice = float.Parse(totalPrc)+9, stockXKrPrice = (float)Math.Round((float.Parse(totalPrc)+9) * usdkrw), prdNm = prdNm });

                if (latestSalePrcMap.ContainsKey(size)) {
                    InfoMap[size].stockXLatestUsPrice = InfoMap[size].stockXUsPrice+latestSalePrcDiffMap[size];
                    InfoMap[size].stockXLatestKrPrice = (float)Math.Round((InfoMap[size].stockXLatestUsPrice) * usdkrw);
                }

                var backBtn = _driver.FindElements(By.ClassName("buy-sell-size"));
                if (backBtn.Count > 1)
                {
                    backBtn[1].Click();
                }
                else {
                    backBtn[0].Click();
                }
                
                Thread.Sleep(2000);
            }

            Thread.Sleep(3000);
            _driver.Quit();

            return InfoMap;
        }

        /*
       author : yuha
       funcName : getUseKrw
       summary : 환율 정보 가져오기
       input :  
       return : float
       */
        public float getUseKrw()
        {
            WebClient webClient = new WebClient();

            string url = "https://api.exchangeratesapi.io/latest?base=USD";
            string json = webClient.DownloadString(url);

            JObject obj = JObject.Parse(json);
            string ex = obj["rates"]["KRW"].ToString();
            float usdkrw = float.Parse(ex);

            return usdkrw;
        }

        public void quitDriver()
        {
            if (_driver != null) {
                _driver.Quit();
            }
           
        }

    }
}
