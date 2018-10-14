using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PyCraft_IDE
{
    class TreeViewInit
    {
        public static void TrvInit(string dir, TreeNodeCollection nodes, int folder_img, int file_img)
        {
            DirectoryInfo directory = new DirectoryInfo(dir);
            foreach (DirectoryInfo d in directory.GetDirectories())
            {
                TreeNode t = new TreeNode(d.Name);
                nodes.Add(t);
                t.ImageIndex = folder_img;
                TrvInit(d.FullName, t.Nodes, folder_img, file_img);
            }
            foreach (FileInfo f in directory.GetFiles())
            {
                TreeNode t = new TreeNode(f.Name);
                nodes.Add(t);
                t.ImageIndex = file_img;
            }
        }
    }
}
