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
        static void Main(string[] arg)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form = new Form1();

            SearchController searchController = new SearchController();
            form.Controls.Add(new SearchView(searchController));

            Application.Run(form);
        }
    }
}
