using System;
using System.Collections.Generic;
using System.Text;

namespace comparePrice.vo
{
    class MstInfo
    {
        public string stockXUrl { get; set; }
        public string kreamXUrl { get; set; }
        public string prdNm { get; set; }
        public Dictionary<string, DetailInfo> detailInfoMap { get; set; }
        public string thumbnailUrl {get;set;}

    }
}
