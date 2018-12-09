using System;
using System.Windows;
using Dragablz;
using MaterialDesignThemes.Wpf;
using PyCraft_IDE_WPF.Utils;

namespace PyCraft_IDE_WPF
{
    public class Boot
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var app = new App { ShutdownMode = ShutdownMode.OnLastWindowClose };
            app.InitializeComponent();

            var mainModel = new MainModel();

            staticVar.mainModel = mainModel;
            NewTab.createNewTab("New", "New Files");

            new MainWindow()
            {
                DataContext = mainModel
            }.Show(); 
            app.Run();
        }
    }
}
