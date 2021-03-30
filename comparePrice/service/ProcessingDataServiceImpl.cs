using comparePrice.serviceInterface;
using comparePrice.vo;
using System;
using System.Collections.Generic;
using System.Linq;
using DiscordWebhook;
using System.Diagnostics;

namespace comparePrice.service
{
    class ProcessingDataServiceImpl : ProcessingDataService
    {
        GettingInfoServiceImpl gettingInfoService = new GettingInfoServiceImpl();
        SizeConstantMap sizeConstantMap = new SizeConstantMap();

        ~ProcessingDataServiceImpl() {
            Debug.WriteLine("pro");
        }

        /*
       author : yuha
       funcName : comparingData
       summary : 가격비교
       input : searchVo
       return : MstInfo
       */
        public MstInfo comparingData(SearchVo searchVo)
        {
            Dictionary<string, string> usKrSizeMap = sizeConstantMap.getUsKrSizeMap();
            float usdKrw = gettingInfoService.getUseKrw();
            searchVo.usdkrw = usdKrw;
   
            MstInfo mstInfo = new MstInfo();
            Debug.WriteLine("test0");
            Dictionary<string, DetailInfo> stockXInfoMap = gettingInfoService.crawlingFromStockx(searchVo);
            Dictionary<string, DetailInfo> kreamInfoMap = gettingInfoService.crawlingFromkream(searchVo);

            Dictionary<string, DetailInfo> detailInfoMap = new Dictionary<string, DetailInfo>();

            string sizeType = searchVo.sizeType.Replace(" ", "");

            foreach (string key in stockXInfoMap.Keys) {

                    DetailInfo detailInfo = new DetailInfo();
                /*kreamX 최근 판매가 널인경우 추가 2021-02-18*/
                if (kreamInfoMap.ContainsKey(key) && (int)kreamInfoMap[key].kreamLatestKrPrice == 0)
                {
                    detailInfo.latestYn = "N";
                }
                else {
                    detailInfo.latestYn = "Y";
                }
                /*kreamX 최근 판매가 널인경우 추가 끝 2021-02-18*/
                int difference = 0;
                    detailInfo.sizeLabel = "US " + key +sizeType+ " , " + stockXInfoMap[key].krSize; // us 4, 220
                    detailInfo.stockXUsPriceLabel = "$" + stockXInfoMap[key].stockXUsPrice.ToString();
                    detailInfo.stockXLatestUsPriceLabel = "$" + stockXInfoMap[key].stockXLatestUsPrice.ToString();
                    detailInfo.stockXKrPriceLabel = "₩" + String.Format("{0:#,###}", (int)stockXInfoMap[key].stockXKrPrice);
                    detailInfo.stockXLatestKrPriceLabel = "₩" + String.Format("{0:#,###}", (int)stockXInfoMap[key].stockXLatestKrPrice);
                    difference = 0 - (int)stockXInfoMap[key].stockXKrPrice;
                    int latestDifference = 0 - (int)stockXInfoMap[key].stockXLatestKrPrice;

                if (kreamInfoMap.ContainsKey(key))
                    {
                        detailInfo.kreamKrPriceLabel = "₩" + String.Format("{0:#,###}", (int)kreamInfoMap[key].kreamKrPrice);
                        detailInfo.kreamKrLatestPriceLabel = "₩" + String.Format("{0:#,###}", (int)kreamInfoMap[key].kreamLatestKrPrice);
                        difference += (int)kreamInfoMap[key].kreamKrPrice;
                        latestDifference += (int)kreamInfoMap[key].kreamLatestKrPrice;
                    if (difference > 0)
                        {
                            detailInfo.differenceLabel = "₩" + String.Format("{0:#,###}", Math.Abs(difference));
                            detailInfo.roiLabel=((int)(((difference / kreamInfoMap[key].kreamKrPrice))*100)).ToString()+"%";
                            detailInfo.latestRoiLabel = ((int)(((latestDifference / kreamInfoMap[key].kreamLatestKrPrice)) * 100)).ToString() + "%";
                            detailInfoMap.Add(key, detailInfo);
                    }
                        else {
                            detailInfo.differenceLabel = "No Result";
                            detailInfo.roiLabel = "No Result";
                        }
                        
                    }
                    else
                    {
                        detailInfo.kreamKrPriceLabel = "No Result";
                        difference = 0;
                        detailInfo.differenceLabel = "No Result";
                        detailInfo.roiLabel = "No Result";
                    }
                    detailInfo.difference = difference;
                    //detailInfoMap.Add(key, detailInfo);
                
            }
            mstInfo.thumbnailUrl = searchVo.thumbnailUrl;
            mstInfo.detailInfoMap = detailInfoMap;
            mstInfo.prdNm=stockXInfoMap.First().Value.prdNm;

            gettingInfoService.quitDriver();
            mstInfo.thumbnailUrl = searchVo.thumbnailUrl;

            return mstInfo;
        }

        /*
        author : yuha
        funcName : makeMstContent
        summary : 웹훅 string content 생성
        input : SearchVo
        return : string
        */
        public Webhook makeMstContent(SearchVo searchVo)
        {
            Webhook webhook = new Webhook(searchVo.webHookUrl);
            MstInfo mstInfo = searchVo.mstInfo;
            Dictionary<string, DetailInfo> detailMap = mstInfo.detailInfoMap;

            List<Embed> embeds = new List<Embed>();
            List<EmbedField> fileds = new List<EmbedField>();
            Embed embed1 = new Embed();
            EmbedThumbnail thumbnail = new EmbedThumbnail();

            string title = "product name : [" + mstInfo.prdNm + "]\n";
            string description = "stockX url : [stockX]("+searchVo.stockXUrl+")\nkream url : [kream]("+searchVo.kreamUrl+")";
            string thumbnailUrl = mstInfo.thumbnailUrl;
            thumbnail.Url = thumbnailUrl;

            if (detailMap.Count > 0)
            {
                
                foreach (string key in detailMap.Keys)
                {
                    EmbedField field = new EmbedField();
                    field.Name = detailMap[key].sizeLabel;
                    if (detailMap[key].latestYn.Equals("N"))
                    { /*kreamX 최근 판매가 널인경우 추가 2021-02-18*/
                        field.Value += detailMap[key].stockXUsPriceLabel
                            + " | " + detailMap[key].stockXKrPriceLabel
                            + " | " + detailMap[key].kreamKrPriceLabel
                            + " | " + detailMap[key].differenceLabel + " | " + detailMap[key].roiLabel;
                    }
                    else
                    {
                        field.Value += detailMap[key].stockXUsPriceLabel
                            + "(" + detailMap[key].stockXLatestUsPriceLabel + ")"
                            + " | " + detailMap[key].stockXKrPriceLabel
                            + "(" + detailMap[key].stockXLatestKrPriceLabel + ")"
                            + " | " + detailMap[key].kreamKrPriceLabel
                            + "(" + detailMap[key].kreamKrLatestPriceLabel + ")"
                            + " | " + detailMap[key].differenceLabel + " | " + detailMap[key].roiLabel
                            + "(" + detailMap[key].latestRoiLabel + ")";
                    }
                    fileds.Add(field);
                }

                embed1.Fields = fileds;
                
            }
            else {
                description += "\n\n**No Mapped fields**";
            }

            embed1.Title = title;
            embed1.Description = description;
            embed1.Thumbnail = thumbnail;

            embeds.Add(embed1);

            webhook.Embeds = embeds;

            /*
            string content = "";

            content = "product name : [" + mstInfo.prdNm + "]\n";
            Dictionary<string, DetailInfo> detailMap = mstInfo.detailInfoMap;
            content += "[size] | [stockX_price] | [stockX_Krprice] | [kream_Krprice] | [difference_price] \n";
            foreach (string key in detailMap.Keys)
            {
                content += detailMap[key].sizeLabel + " | " + detailMap[key].stockXUsPriceLabel + " | " + detailMap[key].stockXKrPriceLabel +
                     " | " + detailMap[key].kreamKrPriceLabel + " | " + detailMap[key].differenceLabel + "\n";
            }
            */

            return webhook;
        }
        public void quitWebDriver() {
            gettingInfoService.quitDriver();
        }
    }
}
