using comparePrice.vo;
using System;
using System.Collections.Generic;
using System.Text;

namespace comparePrice.serviceInterface
{
    interface GettingInfoService
{
        /*
       author : yuha
       funcName : crawlingFromStockx
       summary : stockx 크롤링
       input : searchVo.useKrw
       return : Dictionary<string, DetailInfo>
       */
        Dictionary<string, DetailInfo>  crawlingFromStockx(SearchVo searchVo);
        /*
       author : yuha
       funcName : crawlingFromkream
       summary : kream 크롤링
       input :  searchVo
       return : Dictionary<string, DetailInfo>
       */
        Dictionary<string, DetailInfo> crawlingFromkream(SearchVo searchVo);

        /*
       author : yuha
       funcName : getUseKrw
       summary : 환율 정보 가져오기
       input :  
       return : float
       */
        float getUseKrw();

    }

}
