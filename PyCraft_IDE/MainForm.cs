using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PyCraft_IDE
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            TreeViewInit.TrvInit(Environment.CurrentDirectory, trvDirectory.Nodes, 0, 1);
            StartConsole();
        }

        private void trvDirectory_AfterSelect(object sender, TreeViewEventArgs e)
        {
            DirectoryInfo dir_info = new DirectoryInfo(Environment.CurrentDirectory + "\\" + trvDirectory.SelectedNode.FullPath);
            if (!(dir_info.Attributes.ToString() == "Directory"))
                MessageBox.Show(Environment.CurrentDirectory + "\\" + trvDirectory.SelectedNode.FullPath);

        }

        private void StartConsole()
        {
            OperatingSystem os = Environment.OSVersion;
            Version v = os.Version;

            switch (os.Platform)
            {
                case PlatformID.Win32NT:
                    if (os.Version.Major == 6)
                    {
                        consoleControl.StartProcess("powershell", null);
                        UpdateUIState();
                    }
                    else
                    {
                        consoleControl.StartProcess("cmd", null);
                        UpdateUIState();
                    }
                    break;

                default:
                    MessageBox.Show("PyCraft IDE가 실행 되고 있는 " +
                        "플랫폼의 정보를 읽어 드릴 수 없습니다.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }



        private void UpdateUIState()
        {
            //  Update the state.
            if (consoleControl.IsProcessRunning)
                consoleState.Text = "Running " + System.IO.Path.GetFileName(consoleControl.ProcessInterface.ProcessFileName);
            else
                consoleState.Text = "Not Running";

            //  Update toolbar buttons.
            btn_startCMD.Enabled = !consoleControl.IsProcessRunning;
            btn_stopCMD.Enabled = consoleControl.IsProcessRunning;
        }

        private void btn_startCMD_Click(object sender, EventArgs e)
        {
            StartConsole();
        }

        private void btn_stopCMD_Click(object sender, EventArgs e)
        {
            consoleControl.StopProcess();
            consoleControl.ClearOutput();
            UpdateUIState();
        }
    }
}
