using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

using NarouViewer.API;

namespace NarouViewer
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            List<NarouAPI.NovelData> list = null;

            //  Set Parameter
            NarouAPI.GetParameter parameter = new NarouAPI.GetParameter();
            parameter.limit = 20;
            parameter.useGZIP = true;
            parameter.outType = NarouAPI.GetParameter.OutType.yaml;

            //  Get
            list = NarouAPI.Get(parameter).Result;

            //  Form
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 form = new Form1();
            form.Controls.Add(new NovelDataListView(list));

            Application.Run(form);
        }
    }
}
