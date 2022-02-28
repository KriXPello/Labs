using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kuibarov_Lab1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private (bool, double) ParseInputValue(TextBox textBox)
        {   
            double value;

            bool isParsed = double.TryParse(textBox.Text, out value);

            return (isParsed, value);
        }

        private bool ValidateInput(TextBox textBox)
        {
            var (isParsed, _) = this.ParseInputValue(textBox);

            bool isValid = isParsed;

            textBox.BackColor = isValid ? Color.White : Color.Red;
            textBox.ForeColor = isValid ? Color.Black : Color.White;

            return isValid;
        }

        private bool ValidateInputs()
        {
            bool isPValid = this.ValidateInput(pInput);
            bool isQValid = this.ValidateInput(qInput);
            bool isInputsValid = isPValid && isQValid;

            return isInputsValid;
        }

        private void PInput_TextChanged(object sender, EventArgs e)
        {
            this.ValidateInput(pInput);
        }

        private void QInput_TextChanged(object sender, EventArgs e)
        {
            this.ValidateInput(qInput);
        }

        private void Schema1Button_Click(object sender, EventArgs e)
        {
            if (!this.ValidateInputs())
            {
                return;
            }

            var (_, p) = this.ParseInputValue(pInput);
            var (_, q) = this.ParseInputValue(qInput);

            double sum = 0; // Результат

            // Арифметическая прогрессия
            double uk; // переменная для сохранения u(k)
            double u1 = 2*p - 3*q + 4; // 1 член арифм. прогрессии
            double d = 1 + p*q; // разность арифм. прогрессии

            // Геом. прогрессия
            double vk; // переменная для сохранения v(k)
            double v1 = p + q/2 - 1; // 1 член геом. прогрессии
            double s = p + q + 2; // знаменатель геом. прогрессии

            for (double k = 1; k <= 10; k++)
            {
                uk = u1 + d*(k - 1); // u(k)
                vk = v1 * Math.Pow(s, k - 1); // v(k)

                sum += (p + 2*k)*uk + (3*q + 1)*vk;
            }

            // Вывод результата
            resultBox.Text = sum.ToString();
        }

        private void schema2Button_Click(object sender, EventArgs e)
        {
            if (!this.ValidateInputs())
            {
                return;
            }

            var (_, p) = this.ParseInputValue(pInput);
            var (_, q) = this.ParseInputValue(qInput);

            double a = -(2 * p + q + 2);
            double b = p + q - 1;

            double result = Math.Abs((b - a) / 0.2) + 1;

            var str = new StringBuilder();

            str.AppendLine($"[{a},{b}]");
            str.AppendLine($"Result: {result}");

            resultBox.Text = str.ToString();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
