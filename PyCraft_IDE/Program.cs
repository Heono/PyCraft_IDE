using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PyCraft_IDE
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            restart:
            try
            {
                Application.Run(new MainForm());
            }
            catch (ArgumentOutOfRangeException arge)
            {
                goto restart;
            }
        }
    }
}
