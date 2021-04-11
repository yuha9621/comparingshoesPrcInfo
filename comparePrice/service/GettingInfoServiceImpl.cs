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
            Thread.Sleep(2000);
            
            var buttonListHead = _driver.FindElement(By.XPath("//ul[@class='select_list']"));
            var buttonListCnt = buttonListHead.FindElements(By.XPath("//*[contains(@class,'select_item')]")).Count;
            var buttonList= buttonListHead.FindElements(By.XPath("//*[contains(@class,'select_item')]"));
            buttonList[0].FindElement(By.TagName("button")).Click();
            Thread.Sleep(1000);

            for (int i = 1; i < buttonListCnt; i++)
            {
                sizeButton.Click();
                Thread.Sleep(2000);

                buttonList = _driver.FindElement(By.XPath("//ul[@class='select_list']")).FindElements(By.XPath("//*[contains(@class,'select_item')]"));
                string[] size = buttonList[i].FindElement(By.ClassName("size")).Text.Split('(');
                buttonList[i].FindElement(By.TagName("button")).Click();
                Thread.Sleep(2000);

                string latestSalePrc = _driver.FindElement(By.XPath("//*[contains(@class,'detail_price')]")).FindElement(By.ClassName("num")).Text;
                if (latestSalePrc.Equals("-")) {
                        latestSalePrc = "0";
                    }
                string price = _driver.FindElements(By.XPath("//*[contains(@class,'btn_division')]"))[1].FindElement(By.ClassName("num")).Text;

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
            //사이즈별 최근 판매가
            Dictionary<string, float> latestSalePrcMap = new Dictionary<string, float>();
            //
            Dictionary<string, float> latestSalePrcDiffMap = new Dictionary<string, float>();

            //최근 판매가 리스트 열어주는 버튼 
            var latestBtn = (_driver.FindElementsByXPath("//*[contains(@data-testid,'product-size-select')]"))[1];
            latestBtn.Click();
            Thread.Sleep(1000);

            //사이즈별 최근 판매가 리스트
            var latestHead = (_driver.FindElementsByXPath("//*[contains(@class,'select-control')]"))[1];
            var latestList = latestHead.FindElements(By.XPath(".//*[contains(@role,'menuitem')]"));

            // 최근 판매가 각 사이즈
            string sizeText2 = latestList[1].FindElement(By.ClassName("title")).Text.Replace("US ", "");

            //최근 판매가 메인 > 사이즈 타입 > 사이즈 매핑
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
                //각 사이즈를 눌러 최근가를 조회
                latestList[i].Click();
                Thread.Sleep(2000);

                latestBtn.Click();
                Thread.Sleep(2000);

                //최근 판매가
                string latestSalePrc = _driver.FindElementByXPath("//*[contains(@data-testid,'product-last-sale-button')]").FindElement(By.ClassName("sale-value")).Text;
                //현재가 (초록색)
                string buyPrc = _driver.FindElementsByXPath("//*[contains(@data-testid,'product-bidbuy-btn')]")[1].FindElement(By.ClassName("stat-value")).Text.Replace("$", "");
                string sizeKey;
               
                //최근판매가 없으면 추가x
                if (!latestSalePrc.Contains("-")) {
                    if (!buyPrc.Contains("-")) { 
                    latestSalePrc = latestSalePrc.Replace("$", "");
                    //맵핑 시 사이즈 키 구하기 ( 예 : US4 => 사이즈키 : 4 )
                    sizeKey= latestList[i].FindElement(By.ClassName("title")).Text.Replace("us ", "").Replace("US ", "").Replace(sizeType, "");

                    //최근 판매가 맵에 최근가 추가
                    latestSalePrcMap.Add(sizeKey, float.Parse(latestSalePrc));
                    //최근 판매가 차이 맵에 차이값 추가 ( 최근 판매가 - 현재가 )
                    latestSalePrcDiffMap.Add(sizeKey, float.Parse(latestSalePrc) - float.Parse(buyPrc));
                    }
                }

            }

            _driver.Url = after_url;
            /***2021-02-11 추가 끝****/

            var li = _driver.FindElements(By.ClassName("tile-inner"));
            //사이즈 타입
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
                    //세금 뺀 원 판매가
                    string price = li[i].FindElement(By.XPath("div[@class='tile-subvalue']/div")).Text.Replace("$", "");
                    float size ;
                    if (!price.Equals("Bid"))
                    {
                        if (sizeDefault)
                        {
                            //사이즈 4~12 사이인것들만 인덱스 맵 추가
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
            //두번째 메인 인덱스 맵 순회 
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

                //토탈값 (맨마지막)
                var totalPrc = (_driver.FindElementsByXPath("//*[contains(@data-testid,'bid-total')]"))[3].Text.Replace("$","");
                //원값 (맨 첫)
                var oriPrc = _driver.FindElement(By.ClassName("amount")).Text;
                var improtDutyPrc = (_driver.FindElementsByXPath("//*[contains(@data-testid,'bid-total')]"))[0].Text.Replace("$", "");
                var processingFeePrc = (_driver.FindElementsByXPath("//*[contains(@data-testid,'bid-total')]"))[1].Text.Replace("$", "");
                //원 값+PROCESSING FEE+SHIPPING
                var stockXUsPrice = float.Parse(oriPrc) + float.Parse(processingFeePrc) + 9;
                
                InfoMap.Add(size, new DetailInfo() { usSize = size, krSize = usKrSizeMap[size], stockXUsPrice = stockXUsPrice, stockXKrPrice = (float)Math.Round(stockXUsPrice * usdkrw), prdNm = prdNm });

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
            //인증키 : o1Y3dckO4UIfN0KtxAFy8KWUyt8dfVWz
            string url = "https://quotation-api-cdn.dunamu.com/v1/forex/recent?codes=FRX.KRWUSD";
            string json = webClient.DownloadString(url);

            JArray a = JArray.Parse(json);
            //JObject obj = JObject.Parse(json);
            string ex = a[0]["basePrice"].ToString();
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
