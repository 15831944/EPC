using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;
using Common.Logic;

//[assembly: ExtensionApplication(typeof(CoDesign.Cad.PlugIn.Command))] //启动时加载工具栏，注意typeof括号里的类库名
namespace Plugin.CadJob
{
    public class Command : IExtensionApplication
    {
        public void Initialize()
        {
            Common.Logic.AppConfig.InitContext(true);
            Log4NetConfig.ConfigureFile();
            //初始化默认打开
            PlotRun();
        }

        public void Terminate()
        {
            foreach (Form formInstance in System.Windows.Forms.Application.OpenForms)
            {
                formInstance.Close();
                formInstance.Dispose();
            }
        }

        [CommandMethod("PlotRun")]
        public static void PlotRun()
        {
            var count = System.Windows.Forms.Application.OpenForms.Count;
            if (count==0)
            {
                MainDlg createTempltDlg = new MainDlg();
                AcadApp.ShowModelessDialog(createTempltDlg);
            }
        }

    }
}
