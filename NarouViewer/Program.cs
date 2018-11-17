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

            NarouAPI.GetParameter p = new NarouAPI.GetParameter();
            p.limit = 2;
            p.useGZIP = true;
            p.outType = NarouAPI.GetParameter.OutType.yaml;
            list = NarouAPI.Get(p).Result;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            Form1 form = new Form1();
            form.Controls.Add(new NovelDataView(list[1]));

            Application.Run(form);
        }
    }
}
