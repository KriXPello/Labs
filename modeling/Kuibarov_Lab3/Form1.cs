using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kuibarov_Lab3
{
    public partial class Form1 : Form
    {
        const int p = 1;
        const int q = 9;
        const int i = p + q + 1;
        const double z0 = 100 + 4 * p + 3 * q + 1;

        double multiplier = 1.0 + (i / 100.0); // во сколько раз увеличивается кол-во денег на вкладе каждый год
        double additional = 0.1 * z0; // на сколько ежегодно пополняется вклад во втором случае

        public Form1()
        {
            InitializeComponent();
        }

        private int getT()
        {
            int t;

            bool isParsed = int.TryParse(tInput.Text, out t);

            if (!isParsed || (isParsed && t <= 0))
            {
                MessageBox.Show("Некорректное значение t. Число должно быть целым и >0");

                return 0;
            }

            return t;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int t = getT();

            if (t == 0) return;

            double total1 = z0;
            double total2 = z0;

            chart1.Series.Clear();
            chart1.Series.Add("Случай 1");
            chart1.Series.Add("Случай 2");
            chart1.Series.Add("Увеличение в 2 раза в первом случае");
            chart1.Series.Add("Увеличение в 5 раз во втором случае");
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series[3].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            chart1.Series[0].Color = Color.Red;
            chart1.Series[1].Color = Color.DarkBlue;
            chart1.Series[2].Color = Color.Yellow;
            chart1.Series[3].Color = Color.PaleGreen;

            chart1.Series[0].BorderWidth = 4;
            chart1.Series[1].BorderWidth = 4;
            chart1.Series[2].BorderWidth = 2;
            chart1.Series[3].BorderWidth = 2;
            chart1.Series[2].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chart1.Series[3].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;

            chart1.Series[0].Points.AddXY(0, z0);
            chart1.Series[1].Points.AddXY(0, z0);

            StringBuilder str = new StringBuilder();

            str.AppendLine("t\t1 случай\t2 случай");
            str.AppendLine($"0\t{z0}\t{z0}");

            for (int j = 1; j <= t; j++)
            {
                total1 = Math.Round(total1 * multiplier, 2);
                total2 = Math.Round(total2 * multiplier + additional, 2);

                chart1.Series[0].Points.AddXY(j, total1);
                chart1.Series[1].Points.AddXY(j, total2);

                str.AppendLine($"{j}\t{total1}\t{total2}");
            }

            double x2firstWay = z0 * 2;
            double x5secondWay = z0 * 5;
            total1 = z0;
            total2 = z0;

            int tx2First = 0;
            int tx5Second = 0;

            chart1.Series[2].Points.AddXY(0, z0);
            chart1.Series[3].Points.AddXY(0, z0);

            while (total1 < x2firstWay || total2 < x5secondWay)
            {
                if (total1 < x2firstWay)
                {
                    tx2First++;

                    total1 = Math.Round(total1 * multiplier, 2);

                    chart1.Series[2].Points.AddXY(tx2First, total1);
                }

                if (total2 < x5secondWay)
                {
                    tx5Second++;
                    total2 = Math.Round(total2 * multiplier + additional, 2);

                    chart1.Series[3].Points.AddXY(tx5Second, total2);
                }
            }

            str.AppendLine($"\nДля увеличения в 2 раза в первом случае нужно {tx2First} лет");
            str.AppendLine($"\nДля увеличения в 5 раз во втором случае нужно {tx5Second} лет");

            richTextBox1.Text = str.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double requiredSum = 10 * (100 + 4 * p + 3 * q + 1);
            int t = 5;

            double value = 0;

            for (int j = 0; j < t; j++)
            {
                value += Math.Pow(multiplier, j);
            }

            value = value * 0.1 + Math.Pow(multiplier, t);

            double S = Math.Round(requiredSum / value, 3);

            chart1.Series.Clear();

            StringBuilder str = new StringBuilder();

            str.Append($"Чтобы через {t} лет иметь сумму больше чем {requiredSum}\n");
            str.Append($"первоначальный взнос должен быть: {S} тыс. руб.");

            richTextBox1.Text = str.ToString();
        }
    }
}
