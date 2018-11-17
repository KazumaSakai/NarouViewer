using System;
using System.Windows.Forms;

using NarouViewer.API;

namespace NarouViewer
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*
            Task.Run(async () =>
            {
                NarouAPI.GetParameter p = new NarouAPI.GetParameter();
                p.useGZIP = true;
                p.outType = NarouAPI.GetParameter.OutType.yaml;
                List<NarouAPI.Data> s = await NarouAPI.Get(p);

                foreach (NarouAPI.Data data in s)
                {
                    Console.WriteLine("Title : {0}", data.title);
                    Console.WriteLine("Writer : {0}", data.writer);
                    Console.WriteLine("Story : {0}", data.story);
                    Console.WriteLine("\n\n");
                }
            });

            Console.ReadKey();
            */

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            Form1 form = new Form1();
            form.Controls.Add(new NovelDataVC(null));

            Application.Run(form);
        }
    }
}
