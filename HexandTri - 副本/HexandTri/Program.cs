using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Diagnostics;

namespace HexandTri
{

    class Program
    {
        public static double sqrt3 = 1.7320508075689;
        public static double m = 0;
        public static FileStream fs0 = new FileStream("北京TriangleGrid.txt", FileMode.Create);
        public static StreamWriter sw0 = new StreamWriter(fs0);
        public static double getCos(double vx, double vy, double Vx, double Vy)
        {
            double cos = (vx * Vx + vy * Vy) / Math.Pow((vx * vx + vy * vy), 0.5) / Math.Pow((Vx * Vx + Vy * Vy), 0.5);
            return cos;
        }
        public static int  getBestVector(double Vx, double Vy, double vx, double vy, double L)
        {
            double[] v0 = new double[2] { L / 2, sqrt3 * L / 6 };
            double[] v1 = new double[2] { 0, sqrt3 * L / 3 };
            double[] v2 = new double[2] { -L / 2, sqrt3 * L / 6 };
            double[] v3 = new double[2] { -L / 2, -sqrt3 * L / 6 };
            double[] v4 = new double[2] { 0, -sqrt3 * L / 3 };
            double[] v5 = new double[2] { L / 2, -sqrt3 * L / 6 };
            double cosofv0 = getCos(v0[0], v0[1], Vx - vx, Vy - vy);
            double cosofv1 = getCos(v1[0], v1[1], Vx - vx, Vy - vy);
            double cosofv2 = getCos(v2[0], v2[1], Vx - vx, Vy - vy);
            double cosofv3 = getCos(v3[0], v3[1], Vx - vx, Vy - vy);
            double cosofv4 = getCos(v4[0], v4[1], Vx - vx, Vy - vy);
            double cosofv5 = getCos(v5[0], v5[1], Vx - vx, Vy - vy);
            ArrayList a = new ArrayList();
            a.Add(cosofv0);
            a.Add(cosofv1);
            a.Add(cosofv2);
            a.Add(cosofv3);
            a.Add(cosofv4);
            a.Add(cosofv5);
            a.Sort();
            int b;
            if ((Convert.ToDouble(a[4]) == cosofv0 && Convert.ToDouble(a[5]) == cosofv1) || (Convert.ToDouble(a[5]) == cosofv0 && Convert.ToDouble(a[4]) == cosofv1))
                b = 1;
            else if ((Convert.ToDouble(a[4]) == cosofv0 && Convert.ToDouble(a[5]) == cosofv5) || (Convert.ToDouble(a[5]) == cosofv0 && Convert.ToDouble(a[4]) == cosofv5))
                b = 2;
            else if ((Convert.ToDouble(a[4]) == cosofv1 && Convert.ToDouble(a[5]) == cosofv2) || (Convert.ToDouble(a[5]) == cosofv1 && Convert.ToDouble(a[4]) == cosofv2))
                b = 3;
            else if ((Convert.ToDouble(a[4]) == cosofv2 && Convert.ToDouble(a[5]) == cosofv3) || (Convert.ToDouble(a[5]) == cosofv2 && Convert.ToDouble(a[4]) == cosofv3))
                b = 4;
            else if ((Convert.ToDouble(a[4]) == cosofv3 && Convert.ToDouble(a[5]) == cosofv4) || (Convert.ToDouble(a[5]) == cosofv3 && Convert.ToDouble(a[4]) == cosofv4))
                b = 5;
            else if ((Convert.ToDouble(a[4]) == cosofv4 && Convert.ToDouble(a[5]) == cosofv5) || (Convert.ToDouble(a[5]) == cosofv5 && Convert.ToDouble(a[4]) == cosofv4))
                b = 6;
            else
                b = 7;
            return b;
        }
        public static void DrawLine(double vx, double vy, double Vx, double Vy, double L)
        {
            int number = getBestVector(Vx, Vy, vx, vy, L);
            double[] a; double[] b;
            switch (number)
            {
                case 1:
                    {
                        a = new double[2] { L / 2, sqrt3 * L / 6 };
                        b = new double[2] { 0, sqrt3 * L / 3 };
                        break;
                    }
              
                case 2:
                    {
                        a = new double[2] { L / 2, sqrt3 * L / 6 };
                        b = new double[2] { L / 2, -sqrt3 * L / 6 };
                        break;
                    }
                case 3:
                    {
                        a = new double[2] { 0, sqrt3 * L / 3 };
                        b = new double[2] { -L / 2, sqrt3 * L / 6 };
                        break;
                    }
              
                case 4:
                    {
                        a = new double[2] { -L / 2, sqrt3 * L / 6 };
                        b = new double[2] { -L / 2, -sqrt3 * L / 6 };
                        break;
                    }
              
                case 5:
                    {
                        a = new double[2] { -L / 2, -sqrt3 * L / 6 };
                        b = new double[2] { 0, -sqrt3 * L / 3 };
                        break;
                    }
                
                default:
                    {
                        a = new double[2] { 0, -sqrt3 * L / 3 };
                        b = new double[2] { L / 2, -sqrt3 * L / 6 };
                        break;
                    }
              
            }
            double sin1 = getCos(a[0], a[1], Vx - vx, Vy - vy);
            if (Math.Abs(sin1 - 1) <= 0.000001)
                sin1 = 1.0;
            if (Math.Abs(sin1 - 0) <= 0.000001)
                sin1 = 0.0;
            double sin2 = getCos(b[0], b[1], Vx - vx, Vy - vy);
            if (Math.Abs(sin2 - 1) <= 0.000001)
                sin2 = 1.0;
            if (Math.Abs(sin2 - 0) <= 0.000001)
                sin2 = 0.0;
            double cos1 = Math.Sqrt(1 - sin1 * sin1);
            double cos2 = Math.Sqrt(1 - sin2 * sin2);
            double v0 = -(Math.Sqrt(a[0] * a[0] + a[1] * a[1]) * cos1);
            double v1 = Math.Sqrt(b[0] * b[0] + b[1] * b[1]) * cos2;
            double sum = 0;
            double vc0 = vx;
            double vc1 = vy;
            ArrayList x = new ArrayList();
            ArrayList y = new ArrayList();
            int SX0;
            x.Add(vx);
            y.Add(vy);
            ArrayList X = new ArrayList();
            ArrayList Y = new ArrayList();
            ArrayList SXJ = new ArrayList();
            do
            {
                if (Math.Abs(sum + v0) <= Math.Abs(sum + v1))
                {
                    vx = vx + a[0];
                    vy = vy + a[1];
                    x.Add(vx);
                    y.Add(vy);
                    sum = sum + v0;
                }
                else
                {
                    vx = vx + b[0];
                    vy = vy + b[1];
                    x.Add(vx);
                    y.Add(vy);
                    sum = sum + v1;
                }
            } while (Math.Abs(sum) >= 0.0001);
            double Cur = 0;
            if (Math.Abs(vx - Vx) >= 0.0001 || Math.Abs(vy - Vy) >= 0.0001)
            {
                if (Math.Abs(Vx - vc0) < 0.0001 && Math.Abs(Vy - vc1) >= 0.0001)
                {
                    Cur = Math.Round(Math.Abs(Vy - vc1) / Math.Abs(vy - vc1));
                   
                }
                else if (Math.Abs(Vx - vc0) >= 0.0001 && Math.Abs(Vy - vc1) < 0.0001)
                {
                    Cur = Math.Round(Math.Abs(Vx - vc0) / Math.Abs(vx - vc0));
                  
                }
                else
                {
                    Cur = Math.Round(Math.Abs(Vx - vc0) / Math.Abs(vx - vc0));
                    
                }
            }
            for (int j = 0; j < Cur - 1; j++)
            {
                do
                {
                    if (Math.Abs(sum + v0) <= Math.Abs(sum + v1))
                    {
                        vx = vx + a[0];
                        vy = vy + a[1];
                        x.Add(vx);
                        y.Add(vy);
                        sum = sum + v0;
                    }
                    else
                    {
                        vx = vx + b[0];
                        vy = vy + b[1];
                        x.Add(vx);
                        y.Add(vy);
                        sum = sum + v1;
                    }
                } while (Math.Abs(sum) >= 0.0001);
            }
           
            for (int i = 0; i < x.Count -1; i++)
            {
                double j = 2 * Convert.ToDouble(y[i]) / (sqrt3 * L);
                if (Math.Abs((j -0.33333) % 1) <= 0.0001)
                {
                    SX0 = 1;
                  
                    sw0.WriteLine("{0} {1} {2}", x[i], y[i], SX0);
                    //X.Add(x[i]);
                    //Y.Add(y[i]);
                    //SXJ.Add(SX0);
                    //m++;
                }
                else if (Math.Abs((j - 0.66666) % 1) <= 0.0001)
                {
                    SX0 = 0;
                    sw0.WriteLine("{0} {1} {2}", x[i], y[i], SX0);
                    //X.Add(x[i]);
                    //Y.Add(y[i]);
                    //SXJ.Add(SX0);
                    //m++;
                }
                else
                {
                    
                    double lastTri = 2 * Convert.ToDouble(y[i - 1]) / (sqrt3 * L);
                    double nextTri = 2 * Convert.ToDouble(y[i + 1]) / (sqrt3 * L);
                    if (((Math.Abs((lastTri - 0.33333) % 1) <= 0.0001) && (Math.Abs((nextTri - 0.33333) % 1) <= 0.0001)) || ((Math.Abs((lastTri - 0.66666) % 1) <= 0.0001) && (Math.Abs((nextTri - 0.66666) % 1) <= 0.0001)))
                   {
                       double c = Convert.ToDouble(x[i]) - Convert.ToDouble(x[i - 1]);
                       double d = Convert.ToDouble(y[i]) - Convert.ToDouble(y[i - 1]);
                       double m;
                       double n;
                       if ((Math.Abs(c - a[0]) < 0.0001) && (Math.Abs(d - a[1]) < 0.0001))
                       {
                           m = b[0];
                           n = b[1];
                       }
                       else
                       {
                           m = a[0];
                           n = a[1];
                       }
                       double xnew = Convert.ToDouble(x[i - 1]) + m;
                       double ynew = Convert.ToDouble(y[i - 1]) + n;
                       double j0 = 2 * Convert.ToDouble(ynew) / (sqrt3 * L);
                       if (Math.Abs((j0 - 0.33333) % 1) <= 0.0001)
                       {
                           SX0 = 1;
                           sw0.WriteLine("{0} {1} {2}", xnew, ynew, SX0);
                           //X.Add(xnew);
                           //Y.Add(ynew);
                           //SXJ.Add(SX0);
                           //m++;
                       }
                       else 
                       {
                           SX0 = 0;
                           sw0.WriteLine("{0} {1} {2}", xnew, ynew, SX0);
                           //X.Add(xnew);
                           //Y.Add(ynew);
                           //SXJ.Add(SX0);
                           //m++;
                       }
                   }
                   else
                   {
                       double c = Convert.ToDouble(x[i]) - Convert.ToDouble(x[i - 1]);
                       double d = Convert.ToDouble(y[i]) - Convert.ToDouble(y[i - 1]);
                       double m;
                       double n;
                       if ((Math.Abs(c - a[0]) < 0.0001) && (Math.Abs(d - a[1]) < 0.0001))
                       {
                           m = b[0];
                           n = b[1];
                       }
                       else
                       {
                           m = a[0];
                           n = a[1];
                       }
                       double x1 = Convert.ToDouble(x[i - 1]) + m;
                       double y1 = Convert.ToDouble(y[i - 1]) + n;
                       double x2 = Convert.ToDouble(x[i]) + m;
                       double y2 = Convert.ToDouble(y[i]) + n;
                       double x3 = Convert.ToDouble(x[i]) - m;
                       double y3 = Convert.ToDouble(y[i]) - n;
                       double x4 = Convert.ToDouble(x[i + 1]) - m;
                       double y4 = Convert.ToDouble(y[i + 1]) - n;
                       double j1 = 2 * Convert.ToDouble(y1) / (sqrt3 * L);
                       double j3 = 2 * Convert.ToDouble(y3) / (sqrt3 * L);
                     
                       double SX1, SX2, SX3, SX4;
                       if (Math.Abs((j1 - 0.33333) % 1) <= 0.0001)
                       {
                           SX1 = 1;
                           SX2 = 0;

                       }
                       else
                       {
                           SX1 = 0;
                           SX2 = 1;
                       }
                      
                       if (Math.Abs((j3 - 0.33333) % 1) <= 0.0001)
                       {
                           SX3 = 1;
                           SX4 = 0; 
                       }
                       else
                       {
                           SX3 = 0;
                           SX4 = 1;
                       }
                       double Vv1 = Vy - vc1;
                       double Vv0 = Vx - vc0;
                       double vald = Convert.ToDouble(y[i]) - (Vv1) * (Convert.ToDouble(x[i]) - vc0) / (Vv0) - vc1;
                       double val1 = y1 - (Vv1) * (x1 - vc0) / (Vv0) - vc1;
                       double val2 = y2 - (Vv1) * (x2 - vc0) / (Vv0) - vc1;
                       if (!(((vald >= 0) && (val1 >= 0) && (val2 >= 0)) || ((vald <= 0) && (val1 <= 0) && (val2 <= 0))))//如果现有顶点和三角形1，2在直线的两侧，则记录为漏选三角形
                       {
                           sw0.WriteLine("{0} {1} {2}", x1, y1, SX1);
                           //X.Add(x1);
                           //Y.Add(y1);
                           //SXJ.Add(SX1);
                           //m++;
                           sw0.WriteLine("{0} {1} {2}", x2, y2, SX2);
                           //X.Add(x2);
                           //Y.Add(y2);
                           //SXJ.Add(SX2);
                           //m++;
                         
                       }
                       else
                       {
                           sw0.WriteLine("{0} {1} {2}", x3, y3, SX3);
                           //X.Add(x3);
                           //Y.Add(y3);
                           //SXJ.Add(SX3);
                           //m++;
                           sw0.WriteLine("{0} {1} {2}", x4, y4, SX4);
                           //X.Add(x4);
                           //Y.Add(y4);
                           //SXJ.Add(SX4);
                           //m++;
                          
                       }
                    
                   }
                }
            }
        }
        static void Main(string[] args)
        {
            int numberofpoint = 3661;
            double[] x0 = new double[numberofpoint];
            double[] y0 = new double[numberofpoint];
            int[] SX = new int[numberofpoint];
            StreamReader rd = File.OpenText("北京Triangle.txt");
                for (int i = 0; i < numberofpoint; i++)
                {
                    string line = rd.ReadLine();
                    string[] data = line.Split(' ');
                    x0[i] = double.Parse(data[0]);
                    y0[i] = double.Parse(data[1]);
                    SX[i] = int.Parse(data[2]);
                }
            //int q = 0;
                //Console.WriteLine("请输入三角形的边长：");
                //double L = double.Parse(Console.ReadLine());
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //for (int i = 0; i < 10; i++)
            //{
                for (int j = 0; j < numberofpoint - 1; j++)
                {
                    DrawLine(x0[j], y0[j], x0[j + 1], y0[j + 1], 100);
                    //q++;
                }
            //}
                sw.Stop();
                TimeSpan ts2 = sw.Elapsed;
                Console.WriteLine("Stopwatch总共花费{0}ms.", ts2.TotalMilliseconds);
                //Console.WriteLine("共有{0}个单元.", m);
            //Console.WriteLine("JIESHU 循环次数为：{0}", q);
            Console.ReadLine();
            sw0.Close();
            fs0.Close();
            rd.Close();
        }
    }
}
