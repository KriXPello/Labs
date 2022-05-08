using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kuibarov_Lab6
{
    public delegate double Integrand(double x); // подынтегральная функция
    public delegate double IntegralCalculator(double a, double b, int n, Integrand func);

    public partial class Form1 : Form
    {
        readonly static double p = 1;
        readonly static double q = 9;

        readonly static double Ro = 7856; // кг/м^3
        readonly static double l = 10 + p + q;
        readonly static double Alpha = 1 + 0.1 * (p + q + 2);
        readonly static double A = (0.2 * (p + q + 1)) / 100; // в метрах
        readonly static double B = (5 + p + q) / 100; // в метрах
        readonly static double realV =  Math.Round(Math.PI * (A * A * (Math.Pow(l, 2 * Alpha + 1) / (2 * Alpha + 1)) + 2 * A * B * (Math.Pow(l, Alpha + 1) / Alpha + 1) + B * B * l), 2);

        public Form1()
        {
            InitializeComponent();
        }

        double IntegralRectangle(double a, double b, int n, Integrand func)
        {
            double sum = 0;
            double h = (b - a) / n;

            for (int i = 1; i <= n; i++)
            {
                sum += func(a + i * h);
            }

            sum *= h;

            return sum;
        }

        double IntegralTrapeze(double a, double b, int n, Integrand func)
        {
            double sum = 0;
            double h = (b - a) / n;

            for (int i = 1; i < n; i++)
            {
                sum += func(a + i * h);
            }

            sum += 0.5 * (func(a) + func(b));
            sum *= h;

            return sum;
        }

        double f(double x)
        {
            return A * Math.Pow(x, Alpha) + B;
        }

        double F(double x)
        {
            double y = f(x);

            return y * y;
        }

        void Solve(string integralFuncName, IntegralCalculator calculateIntegral)
        {
            double R = f(l);
            double r = f(0);

            double V0 = Math.Round(Math.PI * R * R * l, 2); // объём заготовки цилиндра
            double Vcon = Math.Round(Math.PI * l * (R * R + R * r + r * r) / 3, 2); // объём заготовки усеч. конуса
            double V = Math.Round(Math.PI * calculateIntegral(0, l, 20, F), 2); // объём детали

            double mot1 = Math.Round(Ro * (V0 - V), 2); // масса отходов если деталь цилиндр
            double mot2 = Math.Round(Ro * (Vcon - V), 2); // масса отходов если деталь усеч. конус

            double profit = Math.Round(mot1 - mot2, 2);
            double profitPercent = Math.Round(profit / mot1 * 100, 2);

            double relativeError =  Math.Round(Math.Abs(realV - V) / V * 100, 2); // погрешность в %

            StringBuilder str = new StringBuilder();

            str.AppendLine($"Объём цилиндра: {V0}");
            str.AppendLine($"Настоящий объём детали: {realV}");
            str.AppendLine($"Объём детали при нахождении с помощью формулы {integralFuncName}: {V}\n");
            str.AppendLine($"Относительная погрешность (в %) при использовании формулы {integralFuncName}: {relativeError}");

            str.AppendLine($"Масса отходов если заготовка - цилиндр: {mot1}");
            str.AppendLine($"Масса отходов если заготовка - усечённый конус: {mot2}\n");

            str.AppendLine($"При использовании заготовки в виде усечённого конуса вместо цилиндра:");
            str.AppendLine($"Разница в массе отходов: {profit}");
            str.AppendLine($"Процент уменьшения отходов: {profitPercent}");

            richTextBox1.Text = str.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Solve("прямоугольников", IntegralRectangle);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Solve("трапеций", IntegralTrapeze);
        }
    }
}
