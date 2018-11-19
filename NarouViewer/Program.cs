using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using YamlDotNet;
using Newtonsoft.Json;

using NarouViewer.API;

namespace NarouViewer
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            //  Form
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            //  Set Parameter
            NarouAPI.GetParameter parameter = new NarouAPI.GetParameter();
            parameter.limit = 20;
            parameter.useGZIP = true;
            parameter.outType = NarouAPI.GetParameter.OutType.json;

            NovelDataListView listView = new NovelDataListView(new List<NarouAPI.NovelData>());
            NarouSearchView searchView = new NarouSearchView(parameter, listView);
            searchView.Search();

            //  Form
            Form1 form = new Form1();
            form.Controls.Add(searchView);
            form.Controls.Add(listView);
            Application.Run(form);
        }
    }
}
