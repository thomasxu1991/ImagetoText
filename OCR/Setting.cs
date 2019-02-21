using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace OCR
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
       
            if (File.Exists("config.ini"))
            {
                string id = SettingValue.ReaderLinesFromFile("config.ini", 0, 1);
                string key = SettingValue.ReaderLinesFromFile("config.ini", 1, 1);
                string secret = SettingValue.ReaderLinesFromFile("config.ini", 2, 1);
                this.textBox1.Text = key;
                this.textBox2.Text = secret;
                this.textBox3.Text = id;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Console.WriteLine(".");
            System.Diagnostics.Process.Start("https://cloud.baidu.com/product/ocr");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //保存配置
        private void button1_Click(object sender, EventArgs e)
        {
            string id = this.textBox3.Text.Trim();
            string key = this.textBox1.Text.Trim();
            string secret = this.textBox2.Text.Trim();
            FileStream fs = new FileStream("config.ini", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(id);
            sw.Write("\r\n");
            sw.Write(key);
            sw.Write("\r\n");
            sw.Write(secret);
            sw.Flush();
            sw.Close();
            fs.Close();
            SettingValue.id = id;
            SettingValue.key = key;
            SettingValue.secret = secret;
            this.Close();
        }
    }
}
