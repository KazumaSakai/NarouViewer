using System;
using System.Windows.Forms;

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

            SearchController searchController = new SearchController();
            form.Controls.Add(new SearchView(searchController));

            Application.Run(form);
        }
    }
}
