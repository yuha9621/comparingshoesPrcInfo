using System;
using System.Collections.Generic;
using System.Text;

namespace comparePrice.vo
{
    class SizeConstantMap
    {
        private Dictionary<string, string> UsToKrSizeYMap = new Dictionary<string, string>()
        {
            {"3.5", "225"},{"4", "230"},{"4.5", "235"},{"5", "240"},{"5.5", "245"},
            {"6", "250"},{"6.5", "255"},{"7", "260"}
        };

        private Dictionary<string, string> KrToUsSizeYMap = new Dictionary<string, string>()
        {
            {"225","3.5"},{"230","4"},{"235","4.5"},{"240","5"},
            {"245","5.5"},{"250","6"},{"255","6.5"},{"260","7"}
        };

        private Dictionary<string, string> UsToKrSizeWMap = new Dictionary<string, string>()
        {
            {"5", "215"},{"5.5", "220"},{"6", "225"},{"6.5", "230"},{"7", "235"},{"7.5", "240"},
            {"8", "245"},{"8.5", "250"},{"9", "255"},{"9.5", "260"},{"10", "265"},
            {"10.5", "270"},{"11", "275"},{"11.5", "280"},{"12", "285"}
        };

        private Dictionary<string, string> KrToUsSizeWMap = new Dictionary<string, string>()
        {
            {"215","5"},{"220","5.5"},{"225","6"},{"230","6.5"},{"235","7"},{"240","7.5"},
            {"245","8"},{"250","8.5"},{"255","9"},{"260","9.5"},{"265","10"},
            {"270","10.5"},{"275","11"},{"280","11.5"},{"285","12"}
        };

        private Dictionary<string, string> UsToKrSizeMap = new Dictionary<string, string>()
        {

            {"4", "220"},{"4.5", "225"},{"5", "230"},{"5.5", "235"},{"6", "240"},{"6.5", "245"},
            {"7", "250"},{"7.5", "255"},{"8", "260"},{"8.5", "265"},{"9", "270"},
            {"9.5", "275"},{"10", "280"},{"10.5", "285"},{"11", "290"},{"11.5", "295"},
            {"12", "300"},{"12.5", "305"},{"13", "310"},{"13.5", "315"},{"14", "320"},
            {"14.5", "325"},{"15", "330"},{"15.5", "335"},{"16", "340"},{"16.5", "345"},
            {"17", "350"},{"17.5", "355"},{"18", "360"}
        };

        private Dictionary<string, string> KrToUsSizeMap = new Dictionary<string, string>()
        {
            {"220","4"},{"225","4.5"},{"230","5"},{"235","5.5"},{"240","6"},{"245","6.5"},
            {"250","7"},{"255","7.5"},{"260","8"},{"265","8.5"},{"270","9"},
            {"275", "9.5"},{"280", "10"},{"285", "10.5"},{"290", "11"},{"295", "11.5"},
            {"300", "12"},{"305", "12.5"},{"310", "13"},{"315", "13.5"},{"320", "14"},
            {"325", "14.5"},{"330", "15"},{"335", "15.5"},{"340", "16"},{"345", "16.5"},
            {"350", "17"},{"355", "17.5"},{"360", "18"}
        };

        private Dictionary<string, string> clothesSizeMap = new Dictionary<string, string>()
        {
            {"M", "M"},{"S", "S"},{"L", "L"},{"XL", "XL"}
        };

        public Dictionary<string, string> getUsKrSizeMap () {
            return UsToKrSizeMap;
        }
        public Dictionary<string, string> getKrUsSizeMap()
        {
            return KrToUsSizeMap;
        }
        public Dictionary<string, string> getUsKrSizeWMap()
        {
            return UsToKrSizeWMap;
        }
        public Dictionary<string, string> getKrUsSizeWMap()
        {
            return KrToUsSizeWMap;
        }
        public Dictionary<string, string> getUsKrSizeYMap()
        {
            return UsToKrSizeYMap;
        }
        public Dictionary<string, string> getKrUsSizeYMap()
        {
            return KrToUsSizeYMap;
        }
        public Dictionary<string, string> getClothesSizeMap()
        {
            return clothesSizeMap;
        }
    }
}
