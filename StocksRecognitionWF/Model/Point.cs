using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StocksRecognitionWF.Model
{
    public class Point
    {
        public Point() { }
        public Point(decimal x, decimal y) { this.x = x; this.y = y; }
        public decimal x { set; get; }
        public decimal y { set; get; }
    }
}
