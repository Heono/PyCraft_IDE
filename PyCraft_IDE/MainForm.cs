using FarsiLibrary.Win;
using FastColoredTextBoxNS;
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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PyCraft_IDE
{
    public partial class MainForm : Form
    {

        // Styles
        TextStyle BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        TextStyle BoldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
        TextStyle GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        TextStyle MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        TextStyle GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        TextStyle BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        TextStyle MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
        Style sameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Gray)));

        string[] keywords = { "False", "None", "True", "and", "assert", "break", "class", "continue", "def", "del", "elif", "else", "except", "exec", "finally", "for", "from", "global", "if", "import", "in", "is", "lambda", "not", "or", "pass", "print", "raise", "return", "try", "while", "with", "yield" };
        //string[] methods = { "Equals()", "GetHashCode()", "GetType()", "ToString()" };
        string[] snippets = { "if(^):\n", "if(^):\n\nelse:\n", "for(^):\n", "while(^):\n" };
        string[] declarationSnippets = {
               "class name:\n",
               "def name(^):\n"
               };
        string currPath = Environment.CurrentDirectory;
        Style invisibleCharsStyle = new InvisibleCharsRenderer(Pens.Gray);
        Color currentLineColor = Color.FromArgb(100, 210, 210, 255);
        Color changedLineColor = Color.FromArgb(255, 230, 230, 255);

        HotkeysMapping keySettings = new HotkeysMapping();

        bool stop;

        DateTime lastNavigatedDateTime = DateTime.Now;

        public MainForm()
        {
            InitializeComponent();
            CreateTab(null);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            TreeViewInit.TrvInit(currPath, trvDirectory.Nodes, 0, 1);
            timer1.Start();
        }

        private void trvDirectory_AfterSelect(object sender, TreeViewEventArgs e)
        {
            DirectoryInfo dir_info = new DirectoryInfo(currPath + "\\" + trvDirectory.SelectedNode.FullPath);
            if (!(dir_info.Attributes.ToString() == "Directory"))
                CreateTab(currPath + "\\" + trvDirectory.SelectedNode.FullPath);
        }

        /*
        private void StartConsole()
        {
            OperatingSystem os = Environment.OSVersion;
            Version v = os.Version;

            switch (os.Platform)
            {
                case PlatformID.Win32NT:
                    if (os.Version.Major == 6)
                    {
                        CurrentTerminal.StartProcess("powershell", null);
                        UpdateUIState();
                    }
                    else
                    {
                        CurrentTerminal.StartProcess("cmd", null);
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
        */

        private void UpdateUIState()
        {
            //  Update the state.
            if (CurrentTerminal != null)
                if (CurrentTerminal.IsProcessRunning)
                    consoleState.Text = Path.GetFileName(CurrentTerminal.ProcessInterface.ProcessFileName) + "실행 중";
            else
                consoleState.Text = "실행 중 아님";
        }
        
        private void btn_openCMD_Click(object sender, EventArgs e)
        {
            CreateTerminal("cmd");
        }

        private void btn_openPowershell_Click(object sender, EventArgs e)
        {
            CreateTerminal("powershell");
        }

        private void CreateTab(string fileName)
        {
            try
            {
                var tb = new FastColoredTextBox();
                documentMap1.Target = tb;
                tb.Font = new Font("Consolas", 9.75f);
                tb.Dock = DockStyle.Fill;
                tb.BorderStyle = BorderStyle.Fixed3D;
                tb.VirtualSpace = true;
                tb.LeftPadding = 17;
                tb.Language = Language.Custom;
                tb.AddStyle(sameWordsStyle);// same words style
                var tab = new FATabStripItem(fileName != null ? Path.GetFileName(fileName) : "[새 파일]", tb);
                tab.Tag = fileName;
                if (fileName != null)
                    tb.OpenFile(fileName);
                tb.Tag = new TbInfo();
                tsFiles.AddTab(tab);
                tsFiles.SelectedItem = tab;
                tb.Focus();
                tb.DelayedTextChangedInterval = 1000;
                tb.DelayedEventsInterval = 500;
                tb.TextChangedDelayed += new EventHandler<TextChangedEventArgs>(tb_TextChangedDelayed);
                tb.SelectionChangedDelayed += new EventHandler(tb_SelectionChangedDelayed);
                tb.KeyDown += new KeyEventHandler(tb_KeyDown);
                tb.ChangedLineColor = changedLineColor;
                tb.CurrentLineColor = currentLineColor;
                tb.ShowFoldingLines = true;
                tb.HighlightingRangeType = HighlightingRangeType.VisibleRange;

                // create autocomplete popup menu
                AutocompleteMenu popupMenu = new AutocompleteMenu(tb);
                popupMenu.Items.ImageList = ilAutocomplete;
                popupMenu.Opening += new EventHandler<CancelEventArgs>(popupMenu_Opening);
                BuildAutocompleteMenu(popupMenu);
                (tb.Tag as TbInfo).popupMenu = popupMenu;
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    CreateTab(fileName);
            }
        }

        private void CreateTerminal(string args)
        {           
            var console = new ConsoleControl.ConsoleControl();
            console.Font = new Font("Consolas", 9.75f);
            console.Dock = DockStyle.Fill;
            FATabStripItem tab;

            if (args == "powershell")
                tab = new FATabStripItem("[Powershell]", console);
            else if (args == "cmd")
                tab = new FATabStripItem("[CMD]", console);
            else
                tab = new FATabStripItem(Path.GetFileName(args) + " [터미널]", console);

            console.Tag = CurrentTB.Name;
            console.Tag = new TbInfo();
            tsConsole.AddTab(tab);
            tsConsole.SelectedItem = tab;

            if (args == "powershell")
                CurrentTerminal.StartProcess(args, null);
            else if (args == "cmd")
                CurrentTerminal.StartProcess(args, null);
        }

        FastColoredTextBox CurrentTB
        {
            get
            {
                if (tsFiles.SelectedItem == null)
                    return null;
                return (tsFiles.SelectedItem.Controls[0] as FastColoredTextBox);
            }

            set
            {
                tsFiles.SelectedItem = (value.Parent as FATabStripItem);
                value.Focus();
            }
        }

        ConsoleControl.ConsoleControl CurrentTerminal
        {
            get
            {
                if (tsConsole.SelectedItem == null)
                    return null;
                return (tsConsole.SelectedItem.Controls[0] as ConsoleControl.ConsoleControl);
            }

            set
            {
                tsConsole.SelectedItem = (value.Parent as FATabStripItem);
                value.Focus();
            }
        }

        void popupMenu_Opening(object sender, CancelEventArgs e)
        {
            //---block autocomplete menu for comments
            //get index of green style (used for comments)
            var iGreenStyle = CurrentTB.GetStyleIndex(CurrentTB.SyntaxHighlighter.GreenStyle);
            if (iGreenStyle >= 0)
                if (CurrentTB.Selection.Start.iChar > 0)
                {
                    //current char (before caret)
                    var c = CurrentTB[CurrentTB.Selection.Start.iLine][CurrentTB.Selection.Start.iChar - 1];
                    //green Style
                    var greenStyleIndex = Range.ToStyleIndex(iGreenStyle);
                    //if char contains green style then block popup menu
                    if ((c.style & greenStyleIndex) != 0)
                        e.Cancel = true;
                }
        }

        private void BuildAutocompleteMenu(AutocompleteMenu popupMenu)
        {
            List<AutocompleteItem> items = new List<AutocompleteItem>();

            foreach (var item in snippets)
                items.Add(new SnippetAutocompleteItem(item) { ImageIndex = 1 });
            foreach (var item in declarationSnippets)
                items.Add(new DeclarationSnippet(item) { ImageIndex = 0 });
            //foreach (var item in methods)
            //    items.Add(new MethodAutocompleteItem(item) { ImageIndex = 2 });
            foreach (var item in keywords)
                items.Add(new AutocompleteItem(item));

            items.Add(new InsertSpaceSnippet());
            items.Add(new InsertSpaceSnippet(@"^(\w+)([=<>!:]+)(\w+)$"));
            items.Add(new InsertEnterSnippet());

            //set as autocomplete source
            popupMenu.Items.SetAutocompleteItems(items);
            popupMenu.SearchPattern = @"[\w\.:=!<>]";
        }
        private void PythonSyntaxHighlight(TextChangedEventArgs e)
        {
            CurrentTB.LeftBracket = '(';
            CurrentTB.RightBracket = ')';
            CurrentTB.LeftBracket2 = '\x0';
            CurrentTB.RightBracket2 = '\x0';
            CurrentTB.HighlightingRangeType = HighlightingRangeType.VisibleRange;
            //clear style of changed range
            e.ChangedRange.ClearStyle(BlueStyle, BoldStyle, GrayStyle, MagentaStyle, GreenStyle, BrownStyle);

            //string highlighting
            //e.ChangedRange.SetStyle(BrownStyle, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
            //comment highlighting
            //e.ChangedRange.SetStyle(GreenStyle, @"#.*$", RegexOptions.Multiline);
            //e.ChangedRange.SetStyle(GreenStyle, "(\"\\\"\".*?\\\"\"\")|(\"\"\".*)", RegexOptions.Singleline);
            e.ChangedRange.SetStyle(GreenStyle, "(\"\"\"[^\"]*\"\"\"|\'\'\'[^\']+\'\'\'|#[^\n]*)", RegexOptions.Singleline | RegexOptions.RightToLeft);
            //number highlighting
            e.ChangedRange.SetStyle(MagentaStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            //attribute highlighting
            e.ChangedRange.SetStyle(GrayStyle, @"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline);
            //class name highlighting
            e.ChangedRange.SetStyle(BoldStyle, @"\b(class)\s+(?<range>\w+?)\b");
            //keyword highlighting
            e.ChangedRange.SetStyle(BlueStyle, @"\b(False|None|True|and|assert|break|class|continue|def|del|elif|else|except|exec|finally|for|from|global|if|import|in|is|lambda|not|or|pass|print|raise|return|try|while|with|yield)\b");

            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();
        }

        void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.K | Keys.Control))
            {
                //forced show (MinFragmentLength will be ignored)
                (CurrentTB.Tag as TbInfo).popupMenu.Show(true);
                e.Handled = true;
            }
            if (e.KeyData == (Keys.Control | Keys.N))
                CreateTab(null);
            if (e.KeyData == (Keys.Control | Keys.S))
            {
                if (tsFiles.SelectedItem != null)
                    Save(tsFiles.SelectedItem);
            }
            if (e.KeyData == (Keys.Control | Keys.F))
            {
                FindForm ff = new FindForm(CurrentTB);
                ff.ShowDialog();
            }
        }

        void tb_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            FastColoredTextBox tb = (sender as FastColoredTextBox);
            PythonSyntaxHighlight(e);
            //show invisible chars
            HighlightInvisibleChars(e.ChangedRange);
            
            tb.Range.ClearFoldingMarkers();

            var currentIndent = 0;
            var lastNonEmptyLine = 0;

            for (int i = 0; i < tb.LinesCount; i++)
            {
                var line = tb[i];
                var spacesCount = line.StartSpacesCount;
                if (spacesCount == line.Count) //empty line
                    continue;

                if (currentIndent < spacesCount)
                    //append start folding marker
                    tb[lastNonEmptyLine].FoldingStartMarker = "m" + currentIndent;
                else
                if (currentIndent > spacesCount)
                    //append end folding marker
                    tb[lastNonEmptyLine].FoldingEndMarker = "m" + spacesCount;

                currentIndent = spacesCount;
                lastNonEmptyLine = i;
            }
        }

        private void HighlightInvisibleChars(Range range)
        {
            range.ClearStyle(invisibleCharsStyle);
            //if (btInvisibleChars.Checked)
            //    range.SetStyle(invisibleCharsStyle, @".$|.\r\n|\s");
        }

        void tb_SelectionChangedDelayed(object sender, EventArgs e)
        {
            var tb = sender as FastColoredTextBox;
            //remember last visit time
            if (tb.Selection.IsEmpty && tb.Selection.Start.iLine < tb.LinesCount)
            {
                if (lastNavigatedDateTime != tb[tb.Selection.Start.iLine].LastVisit)
                {
                    tb[tb.Selection.Start.iLine].LastVisit = DateTime.Now;
                    lastNavigatedDateTime = tb[tb.Selection.Start.iLine].LastVisit;
                }
            }

            //highlight same words
            tb.VisibleRange.ClearStyle(sameWordsStyle);
            if (!tb.Selection.IsEmpty)
                return;//user selected diapason
            //get fragment around caret
            var fragment = tb.Selection.GetFragment(@"\w");
            string text = fragment.Text;
            if (text.Length == 0)
                return;
            //highlight same words
            Range[] ranges = tb.VisibleRange.GetRanges("\\b" + text + "\\b").ToArray();

            if (ranges.Length > 1)
                foreach (var r in ranges)
                    r.SetStyle(sameWordsStyle);
        }

        private bool Save(FATabStripItem tab)
        {
            var tb = (tab.Controls[0] as FastColoredTextBox);
            if (tab.Tag == null)
            {
                if (sfdMain.ShowDialog() != DialogResult.OK)
                    return false;
                tab.Title = Path.GetFileName(sfdMain.FileName);
                tab.Tag = sfdMain.FileName;
            }

            try
            {
                File.WriteAllText(tab.Tag as string, tb.Text);
                tb.IsChanged = false;
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    return Save(tab);
                else
                    return false;
            }

            tb.Invalidate();

            return true;
        }

        private void tsFiles_TabStripItemClosing(TabStripItemClosingEventArgs e)
        {
            if (CurrentTB.IsChanged)
            {
                switch (MessageBox.Show(e.Item.Title + " 파일을 저장 하시겠습니까?", "저장", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                {
                    case DialogResult.Yes:
                        if (!Save(e.Item))
                            e.Cancel = true;
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void 새로만들기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTab(null);
        }

        private void 파일ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ofdMain.ShowDialog() == DialogResult.OK)
                CreateTab(ofdMain.FileName);
        }

        private void 폴더ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fbdMain.ShowDialog() == DialogResult.OK)
            {
                trvDirectory.Nodes.Clear();
                currPath = fbdMain.SelectedPath;
                TreeViewInit.TrvInit(currPath, trvDirectory.Nodes, 0, 1);
            }
        }

        private void 저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tsFiles.SelectedItem != null)
                Save(tsFiles.SelectedItem);
        }

        private void 다른이름으로저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tsFiles.SelectedItem != null)
            {
                string oldFile = tsFiles.SelectedItem.Tag as string;
                tsFiles.SelectedItem.Tag = null;
                if(!Save(tsFiles.SelectedItem)) 
                    if(oldFile != null)
                    {
                        tsFiles.SelectedItem.Tag = oldFile;
                        tsFiles.SelectedItem.Title = Path.GetFileName(oldFile);
                    }
            }
        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 실행취소ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB.UndoEnabled)
                CurrentTB.Undo();
        }

        private void 다시실행ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB.RedoEnabled)
                CurrentTB.Redo();
        }

        private void btn_undo_Click(object sender, EventArgs e)
        {
            if (CurrentTB.UndoEnabled)
                CurrentTB.Undo();
        }

        private void btn_redo_Click(object sender, EventArgs e)
        {
            if (CurrentTB.RedoEnabled)
                CurrentTB.Redo();
        }

        private void 찾기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindForm findForm = new FindForm(CurrentTB);
            findForm.ShowDialog();
        }

        private void 단축기설정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new HotkeysEditorForm(keySettings);
            if (form.ShowDialog() == DialogResult.OK)
            {
                keySettings = form.GetHotkeys();
                CurrentTB.HotkeysMapping = keySettings;
            }
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            //CreateConsole(currPath + "\\" + trvDirectory.SelectedNode.FullPath);
            CreateTerminal(currPath + "\\" + trvDirectory.SelectedNode.FullPath);
        }

        private void tsConsole_TabStripItemClosing(TabStripItemClosingEventArgs e)
        {
            try
            {
                CurrentTerminal.StopProcess();
            }
            catch (NullReferenceException) { /* 닫으려고 하는 창이 콘솔창일 경우*/ }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.HelpLink, "Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateUIState();
        }

        private void tsFiles_TabStripItemSelectionChanged(TabStripItemChangedEventArgs e)
        {
            if (CurrentTB != null)
                CurrentTB.HotkeysMapping = keySettings;
        }
    }
}
