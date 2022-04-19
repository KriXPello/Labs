using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Kuibarov_Lab4
{
    internal class TimeSeries
    {
        readonly double p = Program.p;
        readonly double q = Program.q;
        readonly int n = Program.n;
        readonly int matrixSize;
        readonly bool isLinear;
        Chart chart;
        readonly DataGridView table;
        readonly DataGridViewRow trendRow;
        readonly DataGridViewRow infelicityRow;
        double[] solutions;

        public TimeSeries(DataGridView dataTable, Chart chartObj, int matrSize)
        {
            this.matrixSize = matrSize;
            this.chart = chartObj;
            this.table = dataTable;
            this.isLinear = matrSize == 2;
            this.solutions = new double[matrixSize];
            this.trendRow = isLinear
               ? table.Rows[1]
               : table.Rows[2];
            this.infelicityRow = isLinear
               ? table.Rows[3]
               : table.Rows[4];
        }

        private string StringifyArray(double[] arr)
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < arr.Length; i++)
            {
                str.Append($"{Math.Round(arr[i], 3)}\t");
            }

            return str.ToString();
        }

        private double GetCellValue(DataGridViewCell cell)
        {
            if (cell.Value == null) return double.NaN;
            return double.Parse(cell.Value.ToString());
        }

        private (Matrix, double[]) GenerateCoefficients()
        {
            Matrix M = new Matrix(matrixSize, new double[matrixSize, matrixSize]);
            double[] right = new double[matrixSize];

            // Заполняет матрицу коэффициентов перед неизвестными
            // k = 1..5
            // a11= Sum(k^0) a12= Sum(k^1)
            // a21= Sum(k^1) a22= Sum(k^2)
            for (int i = 1; i <= matrixSize; i++)
            {
                for (int j = 1; j <= matrixSize; j++)
                {
                    // нахождение Sum
                    for (int k = 1; k <= n; k++)
                    {
                        // Нужно брать степень k = i+j (если они с 0)
                        M[i, j] += Math.Pow(k, (i - 1) + (j - 1));
                    }
                }
            }

            // Заполняет массив элементов правой части (b)
            for (int i = 0; i < matrixSize; i++)
            {
                for (int k = 1; k <= n; k++)
                {
                    right[i] += Math.Pow(k, i) * FuncYk(k);
                }
            }

            return (M, right);
        }

        private double[] SolveLinearEquation(Matrix M, double[] right)
        {
            double detM = M.GetDet();
            double[] coefficients = new double[matrixSize];

            for (int colInd = 1; colInd <= matrixSize; colInd++)
            {
                Matrix Mi = M.ReplaceColumn(colInd, right);
                double detMi = Mi.GetDet();

                coefficients[colInd - 1] = detMi / (detM == 0 ? 1 : detM);
            }

            return coefficients;
        }

        private double FuncYk(int k) // k - номер месяца
        {
            DataGridViewCell cell = table.Rows[0].Cells[k];

            double value = GetCellValue(cell);
            if (!double.IsNaN(value)) { return value; }

            double result = 10 + 0.05 * (p + 2 * q + 1) * k + 0.01 * (p + q + 1) * k * k + 0.02 * (2 * p + q + 1) * Math.Pow(-1, k);

            cell.Value = result;
            chart.Series[0].Points.AddXY(k, result);

            return result;
        }

        private double Trend(int t)
        {
            DataGridViewCell cell = trendRow.Cells[t];

            double value = GetCellValue(cell);
            if (!double.IsNaN(value)) { return value; }

            double c1 = solutions[0];
            double c2 = solutions[1];
            double c3 = isLinear ? 0 : solutions[2];

            double result = isLinear
                ? c1 + c2 * t
                : c1 + c2 * t + c3 * t * t;

            result = Math.Round(result, 4);

            cell.Value = result;
            chart.Series[isLinear ? 1 : 2].Points.AddXY(t, result);

            return result;
        }
        
        private double Infelicity(int k) // погрешность
        {
            DataGridViewCell cell = infelicityRow.Cells[k];

            double value = GetCellValue(cell);
            if (!double.IsNaN(value)) { return value; }

            double yk = FuncYk(k);

            double result = Math.Round(Math.Abs(Trend(k) - yk) / (yk == 0 ? 1 : yk) * 100, 4);

            cell.Value = result;

            return result;
        }

        public string Solve()
        {
            StringBuilder log = new StringBuilder();

            var (M, right) = GenerateCoefficients();
            solutions = SolveLinearEquation(M, right);

            log.AppendLine("Коэффициенты перед неизвестными системы:");
            log.AppendLine(M.ToString());
            log.AppendLine("Элементы правой части системы:");
            log.AppendLine(StringifyArray(right));
            log.AppendLine("Решение системы: ");
            log.AppendLine(StringifyArray(solutions));

            double[] infelicities = new double[n];

            for (int k = 1; k <= n; k++)
            {
                infelicities[k-1] = Infelicity(k);
            }

            Trend(n + 1);
            double max = infelicities.Max();
            infelicityRow.Cells[n + 1].Value = max;

            return log.ToString();
        }
        
    }
}
