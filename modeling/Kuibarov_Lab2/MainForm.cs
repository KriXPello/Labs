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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FirstTask form = new FirstTask();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SecondTask form = new SecondTask();
            form.ShowDialog();
        }
    }
}
