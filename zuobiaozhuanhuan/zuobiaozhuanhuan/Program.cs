using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Diagnostics;

namespace zuobiaozhuanhuan
{
    class Program
    {
        public static FileStream fs0 = new FileStream("澳门XY.txt", FileMode.Create);
        public static StreamWriter sw0 = new StreamWriter(fs0);
        public static void BLtoXY(double L, double B)
        {
            double x = L * 20037508.34 / 180.0;
            double y = Math.Log(Math.Tan((90 + B) * Math.PI / 360.0)) / (Math.PI / 180.0);
            y = y * 20037508.34 / 180.0;
            sw0.WriteLine("{0} {1}",x , y );
        }
        static void Main(string[] args)
        {
            int numberofpoint = 71;
            int[] SX = new int[numberofpoint]; 
            double[] x0 = new double[numberofpoint];//4为点的个数
            double[] y0 = new double[numberofpoint];
            StreamReader rd = File.OpenText("澳门BL.txt");
            for (int i = 0; i < numberofpoint; i++)
            {
                string line = rd.ReadLine();
                string[] data = line.Split(',');
                SX[i] = int.Parse(data[0]);
                x0[i] = double.Parse(data[1]);
                y0[i] = double.Parse(data[2]);
            }
            for (int i = 0; i < numberofpoint; i++)
            {
                BLtoXY(x0[i], y0[i]);
            }
            sw0.Close();
            rd.Close();
        }
    }
}
