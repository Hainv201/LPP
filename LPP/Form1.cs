using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LPP
{
    public partial class Form1 : Form
    {
        Formula infix;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            infix = new Formula(textBox1.Text);
            textBox2.Text = infix.RootProposition.ToString();
        }
    }
}
