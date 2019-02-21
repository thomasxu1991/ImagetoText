using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OCR
{
    public partial class Url : Form
    {
        public Url()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 parent = (Form1)Owner;
            parent.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 parent = (Form1)Owner;
            parent.setUrl(this.textBox1.Text);
            parent.Show();
            this.Close();
        }
    }
}
