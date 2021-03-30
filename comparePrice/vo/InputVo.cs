using System;
using System.Collections.Generic;
using System.Text;

namespace comparePrice.vo
{
    public class InputVo
    {
        public string kreamUrl { get; set; }
        public string stockXUrl { get; set; }
        public int intervalHr { get; set; }
        public int intervalMm { get; set; }
        public int interval { get; set; }
        public int no { get; set; }
        public string registDate { get; set; }

        public void cleanInputData() {
            this.kreamUrl = "";
            this.stockXUrl = "";
            this.intervalHr = 0;
            this.intervalMm = 0;
            this.interval = 0;
        }
    }
}
