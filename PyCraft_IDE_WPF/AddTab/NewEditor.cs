using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.CodeCompletion;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Xml;

namespace PyCraft_IDE_WPF
{
    class NewEditor
    {
        public static CodeTextEditor CreateEditor(string fileName)
        {
            var editor = new CodeTextEditor();
            editor.FontFamily = new FontFamily("Consolas");
            editor.FontSize = 20;
            byte[] r = Encoding.Default.GetBytes("40");
            byte[] g = Encoding.Default.GetBytes("3C");
            byte[] b = Encoding.Default.GetBytes("3B");
            editor.Background = new SolidColorBrush(Color.FromRgb(r[0], g[0], b[0]));
            editor.ShowLineNumbers = true;
            try
            {
                if (fileName != "New")
                {
                    FileAttributes attr = File.GetAttributes(fileName);
                    if (((attr & FileAttributes.Directory) == FileAttributes.Directory)) { return null; }
                    else { editor.OpenFile(fileName); }
                }

                editor.SyntaxHighlighting = HighlightingLoader.Load(new XmlTextReader(Environment.CurrentDirectory + "\\Python-Mode.xshd"), HighlightingManager.Instance);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            return editor;
        }
    }
}
