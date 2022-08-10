using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StocksRecognitionWF.Model
{
    public class Item
    {
        public decimal index { set; get; }
        public decimal val { set; get; }
        public DateTime time { set; get; }
        public int swingHight { set; get; }
    }
}
