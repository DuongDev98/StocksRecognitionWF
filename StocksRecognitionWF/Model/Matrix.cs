using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StocksRecognitionWF.Model
{
    public class Matrix
    {
        public Matrix(Point point, decimal value)
        {
            this.point = point;
            this.value = value;
        }
        public Point point { set; get; }
        public decimal value { set; get; }
    }
}
