using DiscordWebhook;
using System;
using System.Collections.Generic;
using System.Text;

namespace comparePrice.vo
{
    /*
    author : yuha
    className : SearchVo
    summary : 조건 파라미터
    */
    class SearchVo
    {
        public float usdkrw { get; set; }
        public string before_url { get; set; }
        public string after_url { get; set; }
        public string prdNm { get; set; }
        Dictionary<string, DetailInfo> stockXInfo {get;set;}
        Dictionary<string, DetailInfo> kreamInfo { get; set; }
        public MstInfo mstInfo { get; set; }
        public string webHookUrl { get; set; }
        public string kreamUrl { get; set; }
        public string stockXUrl { get; set; }
        public string thumbnailUrl { get; set; }
        public int threadNo { get; set; }
        public string sizeType { get; set; }
        public  bool clothesYn { get; set; }
        /*
       author : yuha
       funcName : crawlingFromStockx
       summary : stockX url 가공
       input : string
       return : string
       */
        public string makeStockXUrl(string before_url) {
            string root_url = "https://stockx.com/";
            string after_url = "";
            string identifier = "";

            identifier = before_url.Split(new string[] { root_url }, StringSplitOptions.None)[1];
            after_url = root_url + "buy/" + identifier;

            this.prdNm = identifier;

            return after_url;
        }

    }
}
