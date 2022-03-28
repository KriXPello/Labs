using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kuibarov_Lab2
{
    public partial class SecondTask : Form
    {
        const int p = Program.p;
        const int q = Program.q;

        public SecondTask()
        {
            InitializeComponent();
        }

        private double fy1(double x)
        {
            return Math.Round((6 * p + 4 * q + 1) / (x + 1), 4);
        }

        private double fy2(double x)
        {
            return Math.Round((p + q + 1) * Math.Sqrt(x) + p, 4);
        }

        private void Calculate2Task_Click(object sender, EventArgs e)
        {
            int a = 0; // Левая граница
            int b = p + q + 1; // Правая граница

            double h = 0.2; // шаг

            double x = a;
            double y1, y2;
            double allowedDiff = 0.5; // допустимая разница между значениями y1, y2 чтобы считать точку x точкой пересечения
            double intersectionPoint = double.NaN;

            StringBuilder str = new StringBuilder();
            str.AppendLine("x\ty1(x)\ty2(x)");

            chart1.Series.Clear();

            chart1.Series.Add("График функции y1(x)");
            chart1.Series.Add("График функции y2(x)");

            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series[1].ChartType =System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            chart1.Series[0].Color = Color.Red;
            chart1.Series[1].Color = Color.Blue;

            while (x <= b)
            {
                y1 = fy1(x);
                y2 = fy2(x);

                if (Math.Abs(y1 - y2) < allowedDiff)
                {
                    intersectionPoint = x;
                }

                chart1.Series[0].Points.AddXY(x, y1);
                chart1.Series[1].Points.AddXY(x, y2);

                str.AppendLine($"{x}\t{y1}\t{y2}");

                x = Math.Round(x + h, 4);
            }

            if (!double.IsNaN(intersectionPoint))
            {
                str.AppendLine($"Точка пересечения графиков: x = {intersectionPoint}");
            }

            richTextBox1.Text = str.ToString();
        }
    }
}
