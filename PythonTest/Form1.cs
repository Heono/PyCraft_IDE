using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PythonTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Process pro = new Process();
            pro.StartInfo.FileName = Environment.CurrentDirectory + @"\Python\python-3.7.0.amd64\python.exe";
            pro.StartInfo.RedirectStandardOutput = true;
            pro.StartInfo.RedirectStandardError = true;
            pro.EnableRaisingEvents = true;
            pro.StartInfo.UseShellExecute = false;
            //pro.StartInfo.CreateNoWindow = true;
            pro.OutputDataReceived += pro_OutputDataReceived;
            pro.Start();
            pro.BeginOutputReadLine();
            pro.BeginErrorReadLine();
            pro.WaitForExit();

        }

        private void pro_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
                BeginInvoke(new Action(() => textBox1.Text += (Environment.NewLine + e.Data)));
        }
    }
}
