using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Trianglecoordinate
{
    class Program
    {
        static void Main(string[] args)
        {
            const double sqrt3 = 1.7320508075688772935;//定义根号3，后续计算用
            double m = 0;
            FileStream fs0 = new FileStream("北京Triangle.txt", FileMode.Create);
            StreamWriter sw0 = new StreamWriter(fs0);
            int numberofpoint = 3661;
            double[] x0 = new double[numberofpoint];
            double[] y0 = new double[numberofpoint];
            StreamReader rd = File.OpenText("北京XY.txt");
            for (int i = 0; i < numberofpoint; i++)
            {
                string line = rd.ReadLine();
                string[] data = line.Split(' ');
                x0[i] = double.Parse(data[0]);
                y0[i] = double.Parse(data[1]);
            }
            Console.WriteLine("请输入层次对应格网边长：");
            double L = double.Parse(Console.ReadLine());
            double[] itile = new double[numberofpoint];
            double[] jtile = new double[numberofpoint];
            double[] itri = new double[numberofpoint];
            double[] jtri = new double[numberofpoint];
            int[] SX = new int[numberofpoint];
            double[] xtri = new double[numberofpoint];
            double[] ytri = new double[numberofpoint];
          
            double S = sqrt3 * L / 2;
            for (int i = 0; i < x0.Length; i++)
            {
                itile[i] = y0[i] / S;
                itile[i] = Math.Floor(itile[i]);
                jtile[i] = (x0[i] - itile[i] % 2 * L * 0.5) / L;
                jtile[i] = Math.Floor(jtile[i]);
                //sw0.WriteLine("{0} {1}", itile[i], jtile[i]);
                double xtile = x0[i] - itile[i] % 2 * L * 0.5 - jtile[i] * L;
                double ytile = y0[i] - itile[i] * S;
                double Q = sqrt3 * Math.Abs(xtile - L / 2);
                itri[i] = itile[i];
                if (ytile > Q)
                {
                    jtri[i] = jtile[i];
                    SX[i] = 0;
                }
                else
                {
                    if (xtile < L / 2)
                    {
                        jtri[i] = jtile[i];
                        SX[i] = 1;
                    }
                    else
                    {
                        jtri[i] = jtile[i] + 1;
                        SX[i] = 1;
                    }
                }
                //sw0.WriteLine("{0} {1}", ihex[i], jhex[i]);
            }
            for (int j = 0; j < x0.Length; j++)
            {
                if (SX[j] == 0 && itri[j] % 2 == 0)
                {
                    xtri[j] = (2 * jtri[j] + 1) * L / 2.0;
                    ytri[j] = (itri[j] + 0.6666666666667) * sqrt3 * L / 2;
                }
                if (SX[j] == 0 && itri[j] % 2 != 0)
                {
                    xtri[j] = (jtri[j] + 1) * L ;
                    ytri[j] = (itri[j] + 0.6666666666667) * sqrt3 * L / 2;
                }
                if (SX[j] == 1 && itri[j] % 2 == 0)
                {
                    xtri[j] = jtri[j] * L;
                    ytri[j] = (itri[j] + 0.3333333333333) * sqrt3 * L / 2;
                }
                if (SX[j] == 1 && itri[j] % 2 != 0)
                {
                    xtri[j] = (2 * jtri[j] + 1) * L / 2;
                    ytri[j] = (itri[j] + 0.3333333333333) * sqrt3 * L / 2;
                }
                //sw0.WriteLine("{0} {1}", xhex[j], yhex[j]);
            }
            //////////////////////////////
            double X0;
            double Y0;
            double sumx = 0.0;
            double sumy = 0.0;
            for (int i = 0; i < xtri.Length; i++)
            {
                sumx += xtri[i];
                sumy += ytri[i];
            }
            X0 = sumx / xtri.Length;
            Y0 = sumy / xtri.Length;
            for (int i = 0; i < xtri.Length; i++)
            {
                sw0.WriteLine("{0} {1} {2}", Math.Round(xtri[i]  , 8), Math.Round(ytri[i] , 8), SX[i]);
            }
            sw0.Close();
            Console.ReadLine();
        }
    }
}
