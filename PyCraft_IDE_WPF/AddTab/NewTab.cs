using System;
using System.IO;
using System.Windows.Media;
using Dragablz;
using ICSharpCode.CodeCompletion;
using PyCraft_IDE_WPF.Utils;

namespace PyCraft_IDE_WPF
{
    public class NewTab
    {
        public static Func<HeaderedItemViewModel> Factory
        {
            get
            {
                return
                    () =>
                    {
                        var editor = NewEditor.CreateEditor("New");

                        return new HeaderedItemViewModel()
                        {
                            Header = "New File",
                            Content = editor
                        };
                    };
            }
        }

        public static void createNewTab(string path, string header)
        {
            
            var editor = NewEditor.CreateEditor(path);
            FileAttributes attr;
            if (path != "New")
            {
                attr = File.GetAttributes(path);
                if (header.Contains("Header:") && header.Contains(" Items.Count"))
                    header = StringManager.GetMiddleString(header, "Header:", " Items.Count");

                if ((attr & FileAttributes.Directory) == FileAttributes.Directory) { return; }
                else
                {
                    staticVar.mainModel.Items.Add(
                    new HeaderedItemViewModel
                    {
                        Header = header,
                        Content = editor
                    });
                }
            }
            else
            {
                staticVar.mainModel.Items.Add(
                    new HeaderedItemViewModel
                    {
                        Header = header,
                        Content = editor
                    });
            }
        }
    }
}
