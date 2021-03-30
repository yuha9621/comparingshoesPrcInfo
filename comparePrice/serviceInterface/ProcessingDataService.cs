using comparePrice.vo;
using DiscordWebhook;
using System;
using System.Collections.Generic;
using System.Text;

namespace comparePrice.serviceInterface
{
    interface ProcessingDataService
    {
        /*
       author : yuha
       funcName : comparingData
       summary : 가격비교
       input : searchVo
       return : MstInfo
       */
        MstInfo comparingData(SearchVo searchVo);

        /*
        author : yuha
        funcName : makeMstContent
        summary : 웹훅 string content 생성
        input : SearchVo
        return : string
        */
        Webhook makeMstContent(SearchVo searchVo);
    }
}
