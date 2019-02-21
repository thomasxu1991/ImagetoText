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
    public partial class Screen : Form
    {
        private int count = 0;
        private int []position = new int[4];
        private Rectangle RcDraw;
        public Screen()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void Screen_MouseDown(object sender, MouseEventArgs e)
        {
            //MessageBox.Show(p.ToString(), X + Y);
            Point p = e.Location;
            if (count==0)
            {
                position[0] = p.X;
                position[1] = p.Y;
                RcDraw.X = e.X;
                RcDraw.Y = e.Y;
            }
            if (count == 1)
            {
                position[2] = p.X;
                position[3] = p.Y;

                this.Hide();
                Form1 parent = (Form1)Owner;
                parent.setPosition(position);
                this.Close();
            }
            count++;
        }

        private void Screen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Form1 parent = (Form1)Owner;
                parent.Show();
                this.Close();
            }
        }
        

        private void Screen_MouseMove(object sender, MouseEventArgs e)
        {
            if (position[0]==0&& position[1]==0&& position[2]==0&& position[3]==0)
            {
                return;
            }
            if (e.X < position[0])
            {
                RcDraw.Width = position[0] - e.X;
                RcDraw.X = e.X;
            }
            else
            {
                RcDraw.Width = e.X - RcDraw.X;
            }

            if (e.Y < position[1])
            {
                RcDraw.Height = position[1] - e.Y;
                RcDraw.Y = e.Y;
            }
            else
            {
                RcDraw.Height = e.Y - RcDraw.Y;
            }
            this.Invalidate();
        }

        private void Screen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Blue, 2), RcDraw);
        }
    }
}
