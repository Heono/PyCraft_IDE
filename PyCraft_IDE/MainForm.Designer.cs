namespace PyCraft_IDE
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.trvDirectory = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_start = new System.Windows.Forms.ToolStripButton();
            this.btn_stop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_startCMD = new System.Windows.Forms.ToolStripButton();
            this.btn_stopCMD = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.consoleControl = new ConsoleControl.ConsoleControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.consoleState = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.파일ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.열기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.폴더열기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.저장ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.다른이름으로저잘ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trvDirectory
            // 
            this.trvDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvDirectory.ImageIndex = 0;
            this.trvDirectory.ImageList = this.imageList1;
            this.trvDirectory.Location = new System.Drawing.Point(0, 0);
            this.trvDirectory.Margin = new System.Windows.Forms.Padding(4);
            this.trvDirectory.Name = "trvDirectory";
            this.trvDirectory.SelectedImageIndex = 2;
            this.trvDirectory.Size = new System.Drawing.Size(281, 818);
            this.trvDirectory.TabIndex = 0;
            this.trvDirectory.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvDirectory_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icons8-open-32.png");
            this.imageList1.Images.SetKeyName(1, "icons8-document-32.png");
            this.imageList1.Images.SetKeyName(2, "icons8-ok-32.png");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 33);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.trvDirectory);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1264, 818);
            this.splitContainer1.SplitterDistance = 281;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.textBox1);
            this.splitContainer2.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer2.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer2.Size = new System.Drawing.Size(977, 818);
            this.splitContainer2.SplitterDistance = 501;
            this.splitContainer2.SplitterWidth = 6;
            this.splitContainer2.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 31);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(977, 470);
            this.textBox1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_start,
            this.btn_stop,
            this.toolStripSeparator2,
            this.btn_startCMD,
            this.btn_stopCMD});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(977, 31);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_start
            // 
            this.btn_start.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_start.Image = global::PyCraft_IDE.Properties.Resources.icons8_play_32;
            this.btn_start.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(28, 28);
            this.btn_start.Text = "toolStripButton1";
            // 
            // btn_stop
            // 
            this.btn_stop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_stop.Image = global::PyCraft_IDE.Properties.Resources.icons8_stop_32;
            this.btn_stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(28, 28);
            this.btn_stop.Text = "toolStripButton2";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // btn_startCMD
            // 
            this.btn_startCMD.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_startCMD.Image = global::PyCraft_IDE.Properties.Resources.icons8_command_line_32;
            this.btn_startCMD.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_startCMD.Name = "btn_startCMD";
            this.btn_startCMD.Size = new System.Drawing.Size(28, 28);
            this.btn_startCMD.Text = "toolStripSplitButton1";
            this.btn_startCMD.Click += new System.EventHandler(this.btn_startCMD_Click);
            // 
            // btn_stopCMD
            // 
            this.btn_stopCMD.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_stopCMD.Image = global::PyCraft_IDE.Properties.Resources.icons8_multiply_32;
            this.btn_stopCMD.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_stopCMD.Name = "btn_stopCMD";
            this.btn_stopCMD.Size = new System.Drawing.Size(28, 28);
            this.btn_stopCMD.Text = "toolStripButton3";
            this.btn_stopCMD.Click += new System.EventHandler(this.btn_stopCMD_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.consoleControl);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(977, 281);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Console";
            // 
            // consoleControl
            // 
            this.consoleControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.consoleControl.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consoleControl.IsInputEnabled = true;
            this.consoleControl.Location = new System.Drawing.Point(3, 25);
            this.consoleControl.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.consoleControl.Name = "consoleControl";
            this.consoleControl.SendKeyboardCommandsToProcess = false;
            this.consoleControl.ShowDiagnostics = false;
            this.consoleControl.Size = new System.Drawing.Size(971, 253);
            this.consoleControl.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.consoleState});
            this.statusStrip1.Location = new System.Drawing.Point(0, 281);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(977, 30);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(132, 25);
            this.toolStripStatusLabel1.Text = "Terminal State:";
            // 
            // consoleState
            // 
            this.consoleState.Name = "consoleState";
            this.consoleState.Size = new System.Drawing.Size(115, 25);
            this.consoleState.Text = "Not Running";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.파일ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1264, 33);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 파일ToolStripMenuItem
            // 
            this.파일ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.열기ToolStripMenuItem,
            this.폴더열기ToolStripMenuItem,
            this.toolStripSeparator1,
            this.저장ToolStripMenuItem,
            this.다른이름으로저잘ToolStripMenuItem});
            this.파일ToolStripMenuItem.Name = "파일ToolStripMenuItem";
            this.파일ToolStripMenuItem.Size = new System.Drawing.Size(60, 29);
            this.파일ToolStripMenuItem.Text = "파일";
            // 
            // 열기ToolStripMenuItem
            // 
            this.열기ToolStripMenuItem.Name = "열기ToolStripMenuItem";
            this.열기ToolStripMenuItem.Size = new System.Drawing.Size(252, 30);
            this.열기ToolStripMenuItem.Text = "열기";
            // 
            // 폴더열기ToolStripMenuItem
            // 
            this.폴더열기ToolStripMenuItem.Name = "폴더열기ToolStripMenuItem";
            this.폴더열기ToolStripMenuItem.Size = new System.Drawing.Size(252, 30);
            this.폴더열기ToolStripMenuItem.Text = "폴더 열기";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(249, 6);
            // 
            // 저장ToolStripMenuItem
            // 
            this.저장ToolStripMenuItem.Name = "저장ToolStripMenuItem";
            this.저장ToolStripMenuItem.Size = new System.Drawing.Size(252, 30);
            this.저장ToolStripMenuItem.Text = "저장";
            // 
            // 다른이름으로저잘ToolStripMenuItem
            // 
            this.다른이름으로저잘ToolStripMenuItem.Name = "다른이름으로저잘ToolStripMenuItem";
            this.다른이름으로저잘ToolStripMenuItem.Size = new System.Drawing.Size(252, 30);
            this.다른이름으로저잘ToolStripMenuItem.Text = "다른 이름으로 저장";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 851);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trvDirectory;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ConsoleControl.ConsoleControl consoleControl;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel consoleState;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 파일ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 열기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 폴더열기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 저장ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 다른이름으로저잘ToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_start;
        private System.Windows.Forms.ToolStripButton btn_stop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btn_startCMD;
        private System.Windows.Forms.ToolStripButton btn_stopCMD;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}