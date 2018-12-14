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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form = new Form1();

            form.Controls.Add(new NarouSearchView(new NarouAPI.GetParameter()));

            Application.Run(form);
        }
    }
}
