using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace OCR
{
    public partial class Form1 : Form
    {

        private string type = "screen";
        private string path = "";
        public Form1()
        {

            if (!File.Exists("config.ini"))
            {
                SettingValue.key = "";
                SettingValue.secret = "";
                SettingValue.id = "";
            }
            else
            {
                SettingValue.id = SettingValue.ReaderLinesFromFile("config.ini", 0, 1);
                SettingValue.key = SettingValue.ReaderLinesFromFile("config.ini", 1, 1);
                SettingValue.secret = SettingValue.ReaderLinesFromFile("config.ini", 2, 1);
            }
            InitializeComponent();
        }
        public delegate void SetTextCallback(string text);
        public delegate void TextBoxCallBack(string text);
        private void ThreadProcSafe (string type)
        {
            var baidu = new BaiduSDK();
            var result = baidu.GeneralBasicDemo(type, "");
        }
        public void Fun(object o)
        {
            //这里是你的操作代码,循环,根据条件退出while
            string result = "";
            while (true)
            {
                var baidu = new BaiduSDK();
                result = baidu.GeneralBasicDemo(this.type, this.path);
                break;
            }
            SetTextCallback cbd = o as SetTextCallback;
            //执行回调.
            cbd(result);
        }

        //回调方法
        private void CallBack(string message)
        {
            if (this.textBox1.InvokeRequired)
            {
                TextBoxCallBack d = new TextBoxCallBack(SetText);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                this.textBox1.Text = message;
            }
        }
        private void SetText(string message)
        {
            this.textBox1.Text = message;
        }
        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About ab = new About();
            ab.Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 网络图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Url sc = new Url();
            sc.Show(this);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (SettingValue.key == "" || SettingValue.secret == ""|| SettingValue.id == "")
            {
                MessageBox.Show("请先设置API Key及Secret Key！");
                return;
            }
            this.textBox1.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (SettingValue.key == "" || SettingValue.secret == "" || SettingValue.id == "")
            {
                MessageBox.Show("请先设置API Key及Secret Key！");
                return;
            }
            string text = this.textBox1.Text;
            Clipboard.SetDataObject(text, true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form fm = new Setting();
            fm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SettingValue.key == "" || SettingValue.secret == "" || SettingValue.id == "")
            {
                MessageBox.Show("请先设置API Key及Secret Key！");
                return;
            }
            Screen sc = new Screen();
            this.Hide();
            sc.Show(this);
            
        }
        public void setPosition(int[] potision)
        {
            Image CaptureScreen(int sourceX, int sourceY,Size regionSize)
            {
                Console.Write(regionSize.Height.ToString());

                Thread.Sleep(50);
                Bitmap bmp = new Bitmap(regionSize.Width, regionSize.Height);
                Graphics g = Graphics.FromImage(bmp);
                g.CopyFromScreen(new Point(sourceX, sourceY),new Point(0, 0), regionSize);
                return bmp;
            }
            int start = 0;
            int end = 0;
            
            if (potision[0] > potision[2])
            {
                if (potision[1] > potision[3])//右下左上
                {
                    start = potision[2];
                    end = potision[3];
                }
                else//右上左下
                {
                    start = potision[2];
                    end = potision[1];
                }

            }
            else
            {
                if (potision[1] > potision[3])//左下右上
                {
                    start = potision[0];
                    end = potision[3];

                }
                else//左上右下
                {
                    start = potision[0];
                    end = potision[1];

                }
            }
            Image image = CaptureScreen(start, end, new Size(Math.Abs(potision[2]- potision[0]), Math.Abs(potision[3]- potision[1])));
            image.Save("screen.jpg");
            this.Show();
            this.textBox1.Text = "文件上传中，请稍等...";
            this.type = "screen";
            this.path = "";
            SetTextCallback cbd = CallBack;
            Thread th = new Thread(Fun);
            th.Start(cbd);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (SettingValue.key == "" || SettingValue.secret == "" || SettingValue.id == "")
            {
                MessageBox.Show("请先设置API Key及Secret Key！");
                return;
            }
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.InitialDirectory = Application.StartupPath;
            openDlg.Filter = "图片文件(*.jpg,*.jpeg,*.png)|*.jpg;*.jpeg;*.png";
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = "文件上传中，请稍等...";
                this.type = "file";
                this.path = openDlg.FileName;
                SetTextCallback cbd = CallBack;
                Thread th = new Thread(Fun);
                th.Start(cbd);
            }
            else
            {
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (SettingValue.key == "" || SettingValue.secret == "" || SettingValue.id == "")
            {
                MessageBox.Show("请先设置API Key及Secret Key！");
                return;
            }
            Url sc = new Url();
            sc.Show(this);
        }
        public void setUrl(string url)
        {
            this.textBox1.Text = "文件解析中，请稍等...";
            this.type = "url";
            this.path = url;
            SetTextCallback cbd = CallBack;
            Thread th = new Thread(Fun);
            th.Start(cbd);
        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void contextMenuStrip1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {

            this.showOrHide();
        }

        private void Form1_MinimumSizeChanged(object sender, EventArgs e)
        {

        }

        private void 隐藏显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.showOrHide();
        }
        protected override void OnResize(EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.showOrHide();
            }
        }

        private void  showOrHide()
        {
            if (this.Visible == false)
            {
                this.Show();
                this.ShowInTaskbar = true;
                WindowState = FormWindowState.Normal;
            }
            else
            {
                this.Hide();
                this.ShowInTaskbar = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string moduleName = Process.GetCurrentProcess().MainModule.ModuleName;
            string processName = System.IO.Path.GetFileNameWithoutExtension(moduleName);
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length > 1)
            {
                MessageBox.Show("程序已经在运行啦，不要开那么多哟~", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }

        private void 本地图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            // 指定打开文本文件（后缀名为txt）
            openDlg.Filter = "图片文件(*.jpg,*.jpeg,*.png)|*.jpg;*.jpeg;*.png";
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = "文件上传中，请稍等...";
                var baidu = new BaiduSDK();
                this.textBox1.Text = baidu.GeneralBasicDemo("file", openDlg.FileName);
                this.Show();
            }
        }
    }
}
