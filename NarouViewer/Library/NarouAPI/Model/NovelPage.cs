using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarouViewer
{
    public class NovelPage
    {
        public struct Rubi
        {
            public string rubi;
            public int start;
            public int length;

            public Rubi(string rubi, int start, int length)
            {
                this.rubi = rubi;
                this.start = start;
                this.length = length;
            }
        }

        public List<(string, List<Rubi>)> lines = new List<(string, List<Rubi>)>();

        public NovelPage(string html)
        {
            int start_index = html.IndexOf("<div id=\"novel_honbun\" class=\"novel_view\">");
            int end_index = html.IndexOf("</div>", start_index);

            string[] gyou = html.Substring(start_index, end_index - start_index).Split('\n');
            foreach(string item in gyou)
            {
                List<Rubi> rubis = new List<Rubi>();
                int item_start = item.IndexOf(">") + 1;
                string _item = item.Substring(item_start)
                    .Replace("</p>", "")
                    .Replace("<br />", "\n");

                while (true)
                {
                    int honbun_start = _item.IndexOf("<ruby><rb>");
                    if (honbun_start == -1) break;
                    honbun_start += 10;

                    int honbun_end = _item.IndexOf("</rb><rp>");
                    //(</rp><rt>
                    string honbun = _item.Substring(honbun_start, honbun_end - honbun_start);

                    int rubi_start = _item.IndexOf("</rp><rt>") + 9;
                    int rubi_end = _item.IndexOf("</rt><rp>");

                    string rubi = _item.Substring(rubi_start, rubi_end - rubi_start);

                    int start = honbun_start - 10;
                    int length = honbun_end - honbun_start;

                    rubis.Add(new Rubi(rubi, start, length));

                    _item = _item.Substring(0, honbun_start - 10) + honbun + _item.Substring((_item.IndexOf("</rp></ruby>") + 12));
                    _item.Insert(honbun_start, honbun);
                }

                lines.Add((_item, rubis));
            }
        }
    }
}
