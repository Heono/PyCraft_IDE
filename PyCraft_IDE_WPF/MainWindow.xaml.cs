using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;
using Dragablz;
using PyCraft_IDE_WPF.Utils;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;
using System.Security;

namespace PyCraft_IDE_WPF
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public static RoutedCommand MyCommand_CtrlS = new RoutedCommand();
        public static RoutedCommand MyCommand_CtrlO = new RoutedCommand();
        public static RoutedCommand MyCommand_CtrlN = new RoutedCommand();
        public static RoutedCommand MyCommand_CtrlZ = new RoutedCommand();
        public static RoutedCommand MyCommand_CtrlY = new RoutedCommand();

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        SnackbarMessageQueue msgQueue;

        private string workingDir = Environment.CurrentDirectory;
        private bool isIntergratedConsole = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foldersItem.Items.Clear();
            Populate(workingDir, foldersItem, null, false);
            timer.Start();
            timer.Interval = 500;
            timer.Tick += new System.EventHandler(this.timer_Tick);
            MyCommand_CtrlS.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            MyCommand_CtrlO.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
            MyCommand_CtrlN.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
            MyCommand_CtrlZ.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
            MyCommand_CtrlY.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(MyCommand_CtrlS, saveFile_Click));
            CommandBindings.Add(new CommandBinding(MyCommand_CtrlO, openFile_Click));
            CommandBindings.Add(new CommandBinding(MyCommand_CtrlN, newFile_Click));
            CommandBindings.Add(new CommandBinding(MyCommand_CtrlZ, Undo));
            CommandBindings.Add(new CommandBinding(MyCommand_CtrlY, Redo));
            msgQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(5000));
            snackBar.MessageQueue = msgQueue;
        }

        public string SelectedImagePath { get; set; }

        private void timer_Tick(object sender, EventArgs e)
        {
            //Add Timer Work
        }

        private void Populate(string path, TreeView _root, TreeViewItem _child, bool isfile)
        {
            //MessageBox.Show(_root.Items.CurrentItem.ToString());
            DirectoryInfo dir = new DirectoryInfo(path);
            TreeViewItem _driitem = new TreeViewItem();
            _driitem.Tag = path;
            _driitem.Header = dir.Name;
            //MessageBox.Show(_driitem.Header.ToString());
            _driitem.Expanded += new RoutedEventHandler(_driitem_Expanded);
            if (!isfile)
                _driitem.Items.Add(new TreeViewItem());

            if (_root != null)
            { _root.Items.Add(_driitem); }
            else { _child.Items.Add(_driitem); }
        }

        void _driitem_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem _item = (TreeViewItem)sender;
            if (_item.Items.Count == 1 && ((TreeViewItem)_item.Items[0]).Header == null)
            {
                _item.Items.Clear();
                foreach (string dir in Directory.GetDirectories(_item.Tag.ToString()))
                {
                    DirectoryInfo _dirinfo = new DirectoryInfo(dir);
                    Populate(_dirinfo.FullName, null, _item, false);
                }

                foreach (string dir in Directory.GetFiles(_item.Tag.ToString()))
                {
                    FileInfo _dirinfo = new FileInfo(dir);
                    Populate(_dirinfo.FullName, null, _item, true);
                }

            }
        }

        private void foldersItem_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView tree = (TreeView)sender;
            TreeViewItem temp = ((TreeViewItem)tree.SelectedItem);

            if (temp == null)
                return;
            SelectedImagePath = "";
            string temp1 = "";
            string temp2 = "";
            while (true)
            {
                temp1 = temp.Header.ToString();
                if (temp1.Contains(@"\"))
                {
                    temp2 = "";
                }
                SelectedImagePath = temp1 + temp2 + SelectedImagePath;
                if (temp.Parent.GetType().Equals(typeof(TreeView)))
                {
                    break;
                }
                temp = ((TreeViewItem)temp.Parent);
                temp2 = @"\";
            }
            //show user selected path
            SelectedImagePath = temp.Tag.ToString() + SelectedImagePath.Replace(temp.Header.ToString(), "");
            //staticVar.path = SelectedImagePath;
            //staticVar.fileName = temp.Header.ToString();
            NewTab.createNewTab(SelectedImagePath, foldersItem.SelectedItem.ToString());
            //MessageBox.Show(staticVar.mainModel.Items.Count.ToString());
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foldersItem.Height = this.ActualHeight - 185;
        }

        private void newFile_Click(object sender, RoutedEventArgs e)
        {
            NewTab.createNewTab("New", "New");
        }

        ICSharpCode.CodeCompletion.CodeTextEditor currEditor
        {
            get
            {
                try
                {
                    var editor = new ICSharpCode.CodeCompletion.CodeTextEditor();
                    editor = ((mainTab.SelectedItem as HeaderedItemViewModel).Content as ICSharpCode.CodeCompletion.CodeTextEditor);
                    return editor;
                }
                catch (NullReferenceException n) { return null; }
            }
        }

        private void openFile_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Title = "Open File";
            ofd.Filter = "Python Files (*.py) | *.py; | 모든 파일(*.*) | *.*";
            ofd.InitialDirectory = workingDir;
            System.Windows.Forms.DialogResult dr = ofd.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
                NewTab.createNewTab(ofd.FileName, ofd.SafeFileName);
        }

        private void openFolder_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = Environment.CurrentDirectory;
            dialog.IsFolderPicker = true;
            dialog.InitialDirectory = workingDir;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                workingDir = dialog.FileName;
                foldersItem.Items.Clear();
                Populate(dialog.FileName, foldersItem, null, false);
            }
        }
        private void saveFile_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            sfd.InitialDirectory = workingDir;
            sfd.Title = "다른이름으로 저장";
            sfd.Filter = "Python Files (*.py) | *.py; | 모든 파일(*.*) | *.*";
            sfd.DefaultExt = "py";
            sfd.AddExtension = true;

            var editor = currEditor;

            try
            {
                if (editor.FileName == null)
                {
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write);
                        StreamWriter sw = new StreamWriter(fs);
                        sw.WriteLine(editor.Text);
                        sw.Close();
                        fs.Close();
                        snackBar.MessageQueue.Enqueue(Path.GetFileName(sfd.FileName) + " 파일이 저장되었습니다.");
                    }
                }
                else
                {
                    FileStream fs = new FileStream(editor.FileName, FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(editor.Text);
                    sw.Close();
                    fs.Close();
                    snackBar.MessageQueue.Enqueue(Path.GetFileName(sfd.FileName) + " 파일이 저장되었습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void saveFileAsMenu_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            sfd.InitialDirectory = workingDir;
            sfd.Title = "다른이름으로 저장";
            sfd.Filter = "Python Files (*.py) | *.py; | 모든 파일(*.*) | *.*";
            sfd.DefaultExt = "py";
            sfd.AddExtension = true;

            var editor = currEditor;

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(editor.Text);
                sw.Close();
                fs.Close();
                snackBar.MessageQueue.Enqueue(Path.GetFileName(sfd.FileName) + " 파일이 저장되었습니다.");
            }
        }

        private void Undo(object sender, RoutedEventArgs e)
        {
            currEditor.Undo();
        }

        private void Redo(object sender, RoutedEventArgs e)
        {
            currEditor.Redo();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            /*
            MessageBoxResult result = MessageBox.Show("PyCraft IDE를 종료 하시겠습니까?", "종료", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (result == MessageBoxResult.Yes)
                Application.Current.Shutdown();
            else
                e.Cancel = true;
            */
        }

        private void runPy_Click(object sender, RoutedEventArgs e)
        {
            // 쓰레드 사용하고 비동기로 출력값 받아오기
            
            var s = currEditor.FileName;
            if (s != null)
            {
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;

                    if (isIntergratedConsole == true)
                    {
                        Thread.CurrentThread.IsBackground = true;

                        var cmd = new Process();
                        cmd.StartInfo.FileName = Environment.CurrentDirectory + @"\Python\python-3.7.0.amd64\python.exe"; //@"cmd.exe";
                        cmd.StartInfo.Arguments = s; // cmd 실행시  @"/C python " + s;  사용 /K 명령이 끝나도 cmd 유지 /C 명령이 끝나면 cmd 종료
                        cmd.StartInfo.WorkingDirectory = s.Replace("\\" + Path.GetFileName(s), "");
                        cmd.StartInfo.ErrorDialog = true;
                        cmd.Start();
                        cmd.WaitForExit();
                    }
                }).Start();
            }
        }
    }
}
