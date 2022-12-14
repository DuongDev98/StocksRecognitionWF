using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using StocksRecognitionWF.Model;
using StocksRecognitionWF.Utils;

namespace StocksRecognitionWF
{
    public partial class Tester : Form
    {
        public Tester()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string[] files = System.IO.Directory.GetFiles(txtPath.Text, "*.input");
            DataTable dt = new DataTable();
            dt.Columns.Add("NAME", typeof(string));
            dt.Columns.Add("PATH", typeof(string));
            dt.Columns.Add("VER", typeof(int));
            grMain.AutoGenerateColumns = false;
            foreach (string file in files)
            {
                DataRow r = dt.NewRow();
                r["NAME"] = System.IO.Path.GetFileNameWithoutExtension(file);
                r["PATH"] = file;
                r["VER"] = 0;
                dt.Rows.Add(r);
            }
            grMain.DataSource = dt;
            grMain_SelectionChanged(null, null);
        }

        private void btnFlag_Click(object sender, EventArgs e)
        {
            //đọc danh sách từ file
            DataRow r = (grMain.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string fileName = r["PATH"].ToString();
            List<InputItem> lst = ReadData<InputItem>(fileName);
            List<Item> lstItems = new List<Item>();
            foreach (InputItem item in lst)
            {
                Item newItem = new Item();
                newItem.index = item.index;
                newItem.val = (decimal) item.val;
                newItem.swingHight = item.swingHigh;
                lstItems.Add(newItem);
            }

            //----------BEGIN
            List<OutputItem> lstOutput = new List<OutputItem>();
            int ver = (int)r["VER"] + 1;
            int no = 0;

            //OutputItem outItem = new OutputItem();
            //outItem.ver = ver;
            //outItem.index = (int)lstItems[i].index;
            //outItem.no = no;
            //outItem.val = (double)lstItems[i].val;
            //lstOutput.Add(outItem);

            for (int i = 0; i < lstItems.Count; i++)
            {
                List<Item> lstResult = null;
                List<Item> lstTemp = new List<Item>();
                //Mảng tối thiểu là vị trí hiện tại + 4
                for (int j = i; j < lstItems.Count; j++)
                {
                    Item item = lstItems[j];
                    lstTemp.Add(item);
                    if (j + 4 <= i)
                    {
                        if (Tool.IsBullishFlag(lstTemp))
                        {
                            lstResult = lstTemp;
                        }
                    }
                }
                if (lstResult != null)
                {
                    OutputItem outItem = new OutputItem();
                    outItem.ver = ver;
                    outItem.index = (int)lstItems[i].index;
                    outItem.no = no;
                    outItem.val = (double)lstItems[i].val;
                    lstOutput.Add(outItem);
                    no++;
                }
            }

            //----------END

            //ghi ra file output
            string outputFile = Path.Combine(Path.GetDirectoryName(fileName), Path.GetFileNameWithoutExtension(fileName)) + ".output";
            WriteStructData<OutputItem>(outputFile, lstOutput);

            r["VER"] = ver;
        }

        private void grMain_SelectionChanged(object sender, EventArgs e)
        {
            btnFlag.Enabled = grMain.SelectedRows.Count > 0;
        }

        public static List<T> ReadData<T>(string filename)
        {
            byte[] fileData = File.ReadAllBytes(filename);
            return RawDeserialize<T>(fileData);
        }

        private static List<T> RawDeserialize<T>(byte[] rawData)
        {
            int rawsize = Marshal.SizeOf(typeof(T));
            if (rawsize > rawData.Length)
                return null;

            List<T> lst = new List<T>();
            int position = 0;
            for (int i = 0; i < rawData.Length / rawsize; i++)
            {
                IntPtr buffer = Marshal.AllocHGlobal(rawsize);
                Marshal.Copy(rawData, position, buffer, rawsize);
                object retobj = Marshal.PtrToStructure(buffer, typeof(T));
                Marshal.FreeHGlobal(buffer);
                lst.Add((T)retobj);
                position += rawsize;
            }
            return lst;
        }

        private static byte[] getBytes(object aux)
        {
            int length = Marshal.SizeOf(aux);
            IntPtr ptr = Marshal.AllocHGlobal(length);
            byte[] myBuffer = new byte[length];

            Marshal.StructureToPtr(aux, ptr, true);
            Marshal.Copy(ptr, myBuffer, 0, length);
            Marshal.FreeHGlobal(ptr);

            return myBuffer;
        }

        public static void WriteStructData<T>(string filename, List<T> lst)
        {
            int bytesWritten = 0;
            using (FileStream myFileStream = new FileStream(filename, FileMode.Create))
            {
                foreach (T myData in lst)
                {
                    byte[] newBuffer = getBytes(myData);
                    myFileStream.Write(newBuffer, 0, newBuffer.Length);
                    bytesWritten += newBuffer.Length;
                }
                myFileStream.Flush();
                myFileStream.Close();
            }
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct InputItem
    {
        public int index;
        public double val;
        public int swingHigh;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct OutputItem
    {
        public int index;        
        public int no;
        public int ver;
        public double val;
    }
}