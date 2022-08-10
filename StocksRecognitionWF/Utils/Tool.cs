using StocksRecognitionWF.Model;
using System;
using System.Collections.Generic;

namespace StocksRecognitionWF.Utils
{
    public class Tool
    {
        //Xét ma trận 8 dòng 10 cột
        const int numberOfCols = 10, numberOfRows = 8;
        public static bool IsBullishFlag(List<Item> source)
        {
            List<Point> lstItems = new List<Point>();
            decimal minValue = GetMinValue(source), maxValue = GetMaxValue(source), minIndex = GetMinIndex(source), maxIndex = GetMaxIndex(source);
            foreach (Item item in source)
            {
                //min
                if (minValue > item.val) minValue = item.val;
                if (minIndex > item.index) minIndex = item.index;
                //max
                if (maxValue < item.val) maxValue = item.val;
                if (maxIndex < item.index) maxIndex = item.index;
            }

            //Trục ox
            List<decimal> lstOx = new List<decimal>();
            for (decimal i = 0; i <= numberOfCols; i++)
            {
                lstOx.Add(Math.Round(minIndex + ((maxIndex - minIndex) * i / numberOfCols), 6));
            }

            //Trục oy
            List<decimal> lstOy = new List<decimal>();
            for (decimal i = 0; i <= numberOfRows; i++)
            {
                lstOy.Add(Math.Round(minValue + ((maxValue - minValue) * i / numberOfRows), 6));
            }

            //y = ax + b
            //phân đoạn trục ox
            decimal tempMatrix = (maxIndex - minIndex) / (numberOfCols * 2);
            for (int i = 1; i < source.Count; i++)
            {
                Item item1 = source[i - 1];
                Point diem1 = new Point(item1.index, item1.val);
                Item item2 = source[i];
                Point diem2 = new Point(item2.index, item2.val);

                //Kiểm tra dữ liệu
                //Viết pt đường thẳng
                decimal a = (diem2.y - diem1.y) / (diem2.x - diem1.x), b = diem1.y - a * diem1.x;

                //thay x = (x điểm 1, trục ox, x điểm 2) tính y
                List<decimal> lstTempX = new List<decimal>();
                decimal tempX = diem1.x;
                while (tempX <= diem2.x)
                {
                    lstTempX.Add(tempX);
                    tempX += tempMatrix;
                }
                if (!lstTempX.Contains(diem2.x)) lstTempX.Add(diem2.x);

                foreach (decimal tempXf in lstTempX)
                {
                    decimal tempY = Math.Round(a * tempXf + b, 6);
                    Point point = new Point(GetPosition(tempXf, lstOx), GetPosition(tempY, lstOy));

                    //điền vào lstMaxtrix kiểm tra điểm đã có thì bỏ qua
                    if (!matrixContains(point, lstItems))
                    {
                        lstItems.Add(point);
                    }
                }
            }

            //tính điểm kiểm tra điều kiện
            List<Matrix> lstMatrix = GetMatrixBullishFlag();
            decimal total = 0;
            foreach (Matrix matrix in lstMatrix)
            {
                if (matrix.value == 1)
                {
                    total += matrix.value;
                }
            }
            decimal pointTotal = 0;
            foreach (Point point in lstItems)
            {
                foreach (Matrix matrix in lstMatrix)
                {
                    if (matrix.point.x == point.x && matrix.point.y == point.y)
                    {
                        pointTotal += matrix.value;
                    }
                }
            }
            decimal phanTram = pointTotal / total * 100;
            Console.WriteLine(phanTram);
            if (phanTram > 40)
            {
                return true;
            }
            return false;
        }

        private static List<Matrix> GetMatrixBullishFlag()
        {
            List<Matrix> lstMatrix = new List<Matrix>();
            for (int x = 0; x < numberOfCols; x++)
            {
                for (int y = 0; y < numberOfRows; y++)
                {
                    Matrix matrix = null;
                    if ((x == 0 && y == 0) || (x == 0 && y == 1) || (x == 1 && y == 1) || (x == 1 && y == 2)
                        || (x == 2 && y == 2) || (x == 2 && y == 3) || (x == 3 && y == 3) || (x == 3 && y == 4)
                        || (x == 4 && y == 4) || (x == 4 && y == 5) || (x == 5 && y == 5) || (x == 5 && y == 6)
                        || (x == 6 && y == 6) || (x == 6 && y == 7) || (x == 7 && y == 7) || (x == 7 && y == 6)
                        || (x == 8 && y == 6) || (x == 8 && y == 5) || (x == 8 && y == 4)
                        || (x == 9 && y == 5) || (x == 9 && y == 4) || (x == 9 && y == 3) || (x == 9 && y == 2))
                    {
                        matrix = new Matrix(new Point(x, y), 1);
                    }
                    else if ((x == 1 && y == 0) || (x == 2 && y == 1) || (x == 3 && y == 2) || (x == 4 && y == 3)
                        || (x == 5 && y == 4) || (x == 6 && y == 5) || (x == 7 && y == 5) || (x == 8 && y == 3)
                        || (x == 9 && y == 1)
                        || (x == 0 && y == 2) || (x == 1 && y == 3) || (x == 2 && y == 4) || (x == 3 && y == 5)
                        || (x == 4 && y == 6) || (x == 5 && y == 7))
                    {
                        matrix = new Matrix(new Point(x, y), 0.5M);
                    }
                    else if ((x == 3 && y == 0) || (x == 8 && y == 0)
                        || (x == 4 && y == 1) || (x == 5 && y == 1) || (x == 6 && y == 1) || (x == 7 && y == 1)
                        || (x == 5 && y == 2) || (x == 6 && y == 2) || (x == 7 && y == 2)
                        || (x == 0 && y == 5) || (x == 0 && y == 6) || (x == 3 && y == 7))
                    {
                        matrix = new Matrix(new Point(x, y), -0.5M);
                    }
                    else if ((x == 4 && y == 0) || (x == 5 && y == 0) || (x == 6 && y == 0) || (x == 7 && y == 0)
                        || (x == 1 && y == 6)
                        || (x == 0 && y == 7) || (x == 1 && y == 7) || (x == 2 && y == 7))
                    {
                        matrix = new Matrix(new Point(x, y), -1);
                    }
                    else if (x == 9 && y == 7)
                    {
                        matrix = new Matrix(new Point(x, y), -2);
                    }
                    else
                    {
                        matrix = new Matrix(new Point(x, y), 0);
                    }
                    lstMatrix.Add(matrix);
                }
            }
            return lstMatrix;
        }

        //private static List<Point> GetBullishFlagTemplate()
        //{
        //    List<Point> lstKetQuaDung = new List<Point>();
        //    lstKetQuaDung.AddRange(new Point[] { new Point(0, 1), new Point(0, 2) });
        //    lstKetQuaDung.AddRange(new Point[] { new Point(1, 2), new Point(1, 3) });
        //    lstKetQuaDung.AddRange(new Point[] { new Point(2, 3), new Point(2, 4) });
        //    lstKetQuaDung.AddRange(new Point[] { new Point(3, 4), new Point(3, 5) });
        //    lstKetQuaDung.AddRange(new Point[] { new Point(4, 5), new Point(4, 6) });
        //    lstKetQuaDung.AddRange(new Point[] { new Point(5, 6), new Point(5, 7) });
        //    lstKetQuaDung.AddRange(new Point[] { new Point(6, 7), new Point(6, 8) });
        //    lstKetQuaDung.AddRange(new Point[] { new Point(7, 7), new Point(7, 7) });
        //    lstKetQuaDung.AddRange(new Point[] { new Point(8, 7), new Point(8, 6) });
        //    lstKetQuaDung.AddRange(new Point[] { new Point(9, 6), new Point(9, 5) });
        //    return lstKetQuaDung;
        //}

        static bool matrixContains(Point p, List<Point> lst)
        {
            foreach (Point temp in lst)
            {
                if (temp.x == p.x && temp.y == p.y) return true;
            }
            return false;
        }

        private static int GetPosition(decimal val, List<decimal> lst)
        {
            int pos = 0;
            foreach (decimal item in lst)
            {
                if (val > item) pos = lst.IndexOf(item);
            }
            return pos;
        }

        private static decimal GetMinValue(List<Item> source)
        {
            decimal kq = source[0].val;
            foreach (Item item in source)
            {
                if (kq > item.val) kq = item.val;
            }
            return kq;
        }
        private static decimal GetMaxValue(List<Item> source)
        {
            decimal kq = source[0].val;
            foreach (Item item in source)
            {
                if (kq < item.val) kq = item.val;
            }
            return kq;
        }
        private static decimal GetMinIndex(List<Item> source)
        {
            decimal kq = source[0].index;
            foreach (Item item in source)
            {
                if (kq > item.index) kq = item.index;
            }
            return kq;
        }
        private static decimal GetMaxIndex(List<Item> source)
        {
            decimal kq = source[0].index;
            foreach (Item item in source)
            {
                if (kq < item.index) kq = item.index;
            }
            return kq;
        }

        //public static void print(List<Point> lst, List<Point> template = null)
        //{
        //    //in mảng
        //    for (int y = numberOfCols; y >= 0; y--)
        //    {
        //        for (int x = 0; x <= numberOfCols; x++)
        //        {
        //            Point p = new Point(x, y);
        //            if (matrixContains(p, lst))
        //            {
        //                if (template != null && template.Count > 0)
        //                {
        //                    if (matrixContains(p, template))
        //                    {
        //                        Console.Write("*");
        //                    }
        //                    else
        //                    {
        //                        Console.Write(" ");
        //                    }
        //                }
        //                else
        //                {
        //                    Console.Write("*");
        //                }
        //            }
        //            else
        //            {
        //                Console.Write(" ");
        //            }
        //        }
        //        Console.WriteLine("");
        //    }
        //    Console.WriteLine("------");
        //}
    }
}
