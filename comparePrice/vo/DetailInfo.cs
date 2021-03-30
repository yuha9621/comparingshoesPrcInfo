using System;
using System.Collections.Generic;
using System.Text;

namespace comparePrice
{
    /*
      author : yuha
      className : DetailInfo
      summary : 결과 데이터 vo
    */
    class DetailInfo
    {
        public string sizeLabel { get; set; } // ex : "us4,220"
        public string krSize { get; set; }
        public string usSize { get; set; }
        public float stockXKrPrice { get; set; } // ex : 145000
        public float stockXUsPrice { get; set; } // ex : 130
        public float stockXLatestKrPrice { get; set; } // ex : 145000
        public float stockXLatestUsPrice { get; set; } // ex : 130
        public string stockXKrPriceLabel { get; set; } // ex : 145,000
        public string stockXUsPriceLabel { get; set; } // ex : $130
        public string stockXLatestKrPriceLabel { get; set; } // ex : 145,000
        public string stockXLatestUsPriceLabel { get; set; } // ex : $130

        public float kreamKrPrice { get; set; }
        public float kreamUsPrice { get; set; }
        public float kreamLatestKrPrice { get; set; }
        public string kreamKrPriceLabel { get; set; }
        public string kreamKrLatestPriceLabel { get; set; }
        public string kreamUsPriceLabel { get; set; }
        public string prdNm { get; set; }
        public int difference { get; set; }
        public string differenceLabel { get; set; }
        public float roi { get; set; }
        public string roiLabel { get; set; }
        public string latestRoiLabel { get; set; }
        public string latestYn { get; set; }
    }
}
