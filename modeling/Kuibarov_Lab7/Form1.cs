using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kuibarov_Lab7
{
    public partial class Form1 : Form
    {
        static double p = 1;
        static double q = 9;
        static double k1 = 0.2 + 0.1 * p;
        static double k2 = 0.04 + 0.01 * p;
        static double H = 5 * (20 + 2 * p + q) * (20 + 2 * p + q);
        static double m = 200 + 20 * p + 10 * q;
        static double vc = (600 + 2 * p + 4 * q) * 1000 / 3600;
        static double vk = (60 + p + q) * 1000 / 3600;
        static double tau = 0.2;
        static double g = 9.81;

        public Form1()
        {
            InitializeComponent();
        }

        // линейная интерполяция
        double CalculateTime(double t1, double h1, double t2, double h2)
        {
            double tp = t1 - (t2 - t1) * h1 / (h2 - h1);
            return tp;
        }

        string conv(double val)
        {
            return val.ToString("F2");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear(); // график скорости
            chart2.Series.Clear(); // график траектории

            chart1.Series.Add("Скорость");
            chart2.Series.Add("Траектория");

            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;

            chart1.Series[0].Color = Color.DarkBlue;
            chart2.Series[0].Color = Color.DarkBlue;

            // траектория
            // на каком расстоянии сбросить снаряд (l)
            double x = 0; // координата снаряда            
            double v = 0; // скорость снаряда
            double y = 0; // координата самолета            
            double h = H; // высота снаряда            
            double t = 0; // время движения снаряда            
            double tp = 0; // время падения снаряда

            chart1.Series[0].Points.AddXY(t, v);
            chart2.Series[0].Points.AddXY(t, h);

            StringBuilder str = new StringBuilder();

            str.AppendLine("t\tv\tx(t)\ty(t)\th");
            str.AppendLine($"{conv(t)}\t{conv(v)}\t{conv(x)}\t{conv(y)}\t{conv(h)}");
            
            while (h > 0)
            {
                v = v + tau * (g - (k1 * v + k2 * v * v) / m); // расчет скорости
                x = x + tau * v; // расчет координаты снаряда

                if ((H - x) <= 0)
                {
                    // если высота получилось отрицательной, то
                    // выполняем расчет момента времени падения снаряда
                    tp = CalculateTime(t, h, t + tau, H - x);
                }

                h = H - x; // расчет высоты
                t += tau; // расчет времени
                y = vc * t; // расчет координаты самолета

                chart1.Series[0].Points.AddXY(t, v);
                chart2.Series[0].Points.AddXY(t, h);

                str.AppendLine($"{conv(t)}\t{conv(v)}\t{conv(x)}\t{conv(y)}\t{conv(h)}");
            }

            double l = (vc - vk) * tp;

            richTextBox1.Text = 
                $"Время падения снаряда tп = {conv(tp)} сек.\n" +
                $"Нужно сбросить снаряд на расстоянии l = {l} м\n" + 
                str.ToString();
        }
    }


}
