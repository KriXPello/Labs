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
    public partial class FirstTask : Form
    {
        const int p = Program.p;
        const int q = Program.q;

        public FirstTask()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        
        private double volume1(int t)
        {
            return Math.Round(1.0 + p + (1 + q) * t + t * t, 4);
        }

        private double volume2(int t)
        {
            return Math.Round(Math.Pow(1.2 + p + q, t), 4);
        }

        private double profit1(int t, double volume)
        {
            switch (t)
            {
                case 1: return (p + q + 1) * volume;
                case 2: return 1.2*(p + q + 1) * volume;
                case 3: return 1.3*(p + q + 1) * volume;
                case 4: return 1.25*(p+q+1) * volume;
                default: return 1.2*(p + q + 1) * volume;
            }
        }

        private double profit2(int t, double volume)
        {
            switch (t)
            {
                case 1: return (p + q + 4) * volume;
                case 2: return 1.1 * (p + q + 2) * volume;
                case 3: return 1.2 * (p + q + 3) * volume;
                case 4: return 1.24 * (p + q + 1) * volume;
                default: return 1.2 * (p + q + 6) * volume;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder volumeStr = new StringBuilder();
            StringBuilder profitStr = new StringBuilder();

            volumeStr.AppendLine("Объём выпуска\n");
            volumeStr.AppendLine("t\tf1(t)\tf2(t)");

            profitStr.AppendLine("Прибыль\n");
            profitStr.AppendLine("t\tC1k\tC2k");

            chart1.Series.Clear();
            chart2.Series.Clear();

            chart1.Series.Add("Объём выпуска 1 предприятия");
            chart2.Series.Add("Объём выпуска 2 предприятия");

            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            chart1.Series[0].Color = Color.Red;
            chart2.Series[0].Color = Color.Blue;

            double totalProfit1 = 0;
            double totalProfit2 = 0;
            double v1, v2, p1, p2;

            for (int t = 1; t <= 5; t++)
            {
                v1 = volume1(t);
                v2 = volume2(t);

                chart1.Series[0].Points.AddXY(t, v1);
                chart2.Series[0].Points.AddXY(t, v2);

                volumeStr.AppendLine($"{t}\t{v1}\t{v2}");

                p1 = Math.Round(profit1(t, v1), 4);
                p2 = Math.Round(profit2(t, v2), 4);

                profitStr.AppendLine($"{t}\t{p1}\t{p2}");

                totalProfit1 += p1;
                totalProfit2 += p2;
            }

            profitStr.AppendLine($"Итого:\t{totalProfit1}\t{totalProfit2}");

            volumeTextbox.Text = volumeStr.ToString();
            commonProfitTextbox.Text = profitStr.ToString();
        }
    }
}
