using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kuibarov_Lab4
{
    public partial class MainForm : Form
    {
        string[] rows = new string[]
        {
            "Объём выпуска (yk) тыс. шт.",
            "Линейный тренд",
            "Квадратичный тренд",
            "Погрешность линейного тренда %",
            "Погрешность квадратичного тренда %",
        };

        public MainForm()
        {
            InitializeComponent();

            ResetTable();

            ResetChart();
        }

        private void ResetTable()
        {
            table.Rows.Clear();

            for (int i = 0; i < rows.Length; i++)
            {
                table.Rows.Add();
                table.Rows[i].Cells[0].Value = rows[i];
            }
        }

        private void ResetChart()
        {
            chart1.Series.Clear();
            chart1.Series.Add("Реальный объём выпуска");
            chart1.Series.Add("Линейный тренд");
            chart1.Series.Add("Квадратичный тренд");
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;

            chart1.Series[0].Color = Color.Green;
            chart1.Series[1].Color = Color.DarkBlue;
            chart1.Series[2].Color = Color.Red;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TimeSeries linearSeries = new TimeSeries(table, chart1, 2);

            string log = linearSeries.Solve();

            textBox.Text = log;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TimeSeries linearSeries = new TimeSeries(table, chart1, 3);

            string log = linearSeries.Solve();

            textBox.Text = log;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResetTable();
            ResetChart();
            textBox.Text = "";
        }
    }
}
