using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kuibarov_Lab5
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        readonly double p = 1;
        readonly double q = 9;
        readonly double nearZero = 1e-15;

        string ConvertMatrixToString(double[,] matrix, double[] b)
        {
            StringBuilder str = new StringBuilder();

            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    str.Append(String.Format("{0:F2}\t", matrix[i, j]));
                }

                str.AppendLine(String.Format("| {0:F2}", b[i]));
            }

            return str.ToString();
        }

        void SwapColumns(double[,] matrix, int c1, int c2)
        {
            int m = matrix.GetLength(0);

            double temp;

            for (int i = 0; i < m; i++)
            {
                temp = matrix[i, c1];
                matrix[i, c1] = matrix[i, c2];
                matrix[i, c2] = temp;
            }
        }

        /**
         * Ищет столбец, в котором ведущий элемент для 
         * переданного номера строки наибольший по модулю.
         */
        int FindMaxAbs(double[,] matrix, int row)
        {
            int col = -1;
            double max = 0;

            int n = matrix.GetLength(1);

            for (int j = row; j < n; j++)
            {
                if (Math.Abs(matrix[row, j]) > max)
                {
                    max = Math.Abs(matrix[row, j]);
                    col = j;
                }
            }

            return col;
        }

        bool SolveByGauss(double[,] a, double[] b, double[] x)
        {
            double det = 1; // определитель
            double e; // значение ведущего элемента

            int n = a.GetLength(1);

            double[] temp = new double[n];

            int[] numbers = new int[n]; // номера переменных
            for (int i = 0; i < n; i++)
            {
                numbers[i] = i;
            }

            int col;
            int t;
            for (int j = 0; j < n; j++)
            {
                col = FindMaxAbs(a, j);

                if (col == -1 || Math.Abs(a[j, col]) < nearZero)
                {
                    return false;
                }

                if (col != j)
                {
                    SwapColumns(a, j, col);
                    t = numbers[j];
                    numbers[j] = numbers[col];
                    numbers[col] = t;
                }

                e = a[j, j];
                a[j, j] = 1.0;

                det *= e;

                // Делим каждый элемент строки на ведущий элемент
                for (int k = j + 1; k < n; k++)
                {
                    a[j, k] /= e;
                }
                b[j] /= e;

                // Исключаем все элементы ниже ведущего в j столбце
                for (int i = j + 1; i < n; i++)
                {
                    e = a[i, j];
                    a[i, j] = 0;

                    for (int k = j + 1; k < n; k++)
                    {
                        a[i, k] -= a[j, k] * e;
                    }
                    b[i] = b[i] - b[j] * e;
                }
            }

            if (Math.Abs(det) < nearZero)
            {
                return false;
            }

            double sum;

            temp[n - 1] = b[n - 1];

            for (int i = n - 2; i >= 0; i--)
            {
                sum = 0;
                for (int j = i + 1; j < n; j++)
                {
                    sum += a[i, j] * temp[j];
                }
                temp[i] = b[i] - sum;
            }

            // окончательное решение
            for (int i = 0; i < n; i++)
            {
                x[numbers[i]] = temp[i];
            }

            return true;
        }

        double r(int k)
        {
            return p + q + k;
        }

        double eds(int k)
        {
            switch (k)
            {
                case 1: return 2 * (p + q) + 20;
                case 2: return 2 * (p + q) + 3;
                case 3: return 3 * (p + q) + 8;
                case 4: return 8 * (p + q) + 39;
                default: return 0;
            }
        }

        double edsLow(int k)
        {
            return eds(k) * 0.8;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // решение первой задачи

            // система для нахождения распределения токов
            double[,] matrix = new double[,]
            {
            //  { i[1], i[2], i[3], i[4], i[5], i[6], i[7], i[8] }
                { 1, 0, 0, 1, 1, 0, 0, 0 },
                { -1, -1, 0, 0, 0, 1, 0, 0 },
                { 0, 1, -1, 0, 0, 0, 1, 0 },
                { 0, 0, 1, -1, 0, 0, 0, 1 },
                { r(1), 0, 0, 0, -r(5), r(6), 0, 0 },
                { 0, r(2), 0, 0, 0, r(6), -r(7), 0 },
                { 0, 0, r(3), 0, 0, 0, r(7), -r(8) },
                { 0, 0, 0, r(4), -r(5), 0, 0, r(8) }
            };

            double[] b = new double[] { 0, 0, 0, 0, eds(1), eds(2), eds(3), eds(4) };

            // распределения токов 
            double[] i = new double[matrix.GetLength(1)];

            StringBuilder str = new StringBuilder();

            str.AppendLine("Матрица для нахождения распределения токов:");
            str.AppendLine(ConvertMatrixToString(matrix, b));

            bool res = SolveByGauss(matrix, b, i);

            if (res)
            {
                str.AppendLine("Решение");

                for (int k = 0; k < matrix.GetLength(1); k++)
                {
                    str.AppendLine($"i{k + 1} = {i[k].ToString("R4")}");
                }
            } else
            {
                str.AppendLine("Решения нет");
            }


            // решение 2 задачи

            double i1Low = i[0] * 0.8;

            double[,] matrix2 = new double[,]
            {
             // { e4, i2, i3, i4, i5, i6, i7, i8 },
                { 0, 0, 0, 1, 1, 0, 0, 0 },
                { 0, -1, 0, 0, 0, 1, 0, 0 },
                { 0, 1, -1, 0, 0, 0, 1, 0 },
                { 0, 0, 1, -1, 0, 0, 0, 1 },
                { 0, 0, 0, 0, -r(5), r(6), 0, 0 },
                { 0, r(2), 0, 0, 0, r(6), -r(7), 0 },
                { 0, 0, r(3), 0, 0, 0, r(7), -r(8) },
                { 0, 0, 0, r(4), -r(5), 0, 0, r(8) }
            };

            double[] b2 = new double[] { 
                -i1Low,
                i1Low,
                0,
                0,
                edsLow(1) - i1Low * r(1),
                edsLow(2),
                edsLow(3),
                0,
            };

            double[] x = new double[8];

            str.AppendLine("\n\nМатрица для нахождения e4:");
            str.AppendLine(ConvertMatrixToString(matrix2, b2));

            bool res2 = SolveByGauss(matrix2, b2, x);

            if (res2)
            {
                str.AppendLine("Решение");
                str.AppendLine($"e4 = {i[0].ToString("R4")}");
            }
            else
            {
                str.AppendLine("e4 найти нельзя, нет решения системы");
            }

            richTextBox1.Text = str.ToString();
        }
    }

}
