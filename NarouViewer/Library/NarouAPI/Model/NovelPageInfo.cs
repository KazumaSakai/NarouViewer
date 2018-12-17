using System;
using System.Collections.Generic;

namespace NarouViewer
{
    public class NovelPageInfo
    {
        public string title;
        public string updateDay;
        public string reUpdateDay;

        public NovelPageInfo(string title, string updateDay, string reUpdateDay)
        {
            this.title = title;
            this.updateDay = updateDay;
            this.reUpdateDay = reUpdateDay;
        }

        public static List<NovelPageInfo> Hmtl_NovelPageList(string html)
        {
            string[] episodes = html.Split(new string[] { "<dl class=\"novel_sublist2\">" }, StringSplitOptions.None);

            List<NovelPageInfo> list = new List<NovelPageInfo>(episodes.Length - 1);

            for (int i = 1; i < episodes.Length; i++)
            {
                string title = "";
                string updateDay = "";
                string reUpdateDay = "";

                string item = episodes[i];
                item = item.Replace("<dd class=\"subtitle\">", "").Replace("\n", "") + 24;
                int item_end = item.IndexOf("</dl>");
                item = item.Substring(0, item_end);

                //  Title
                int title_start = item.IndexOf(">") + 1;
                int title_end = item.IndexOf("</a>");
                title = item.Substring(title_start, (title_end - title_start));

                //  UpdateDay
                int update_start = item.IndexOf("<dt class=\"long_update\">") + 24;
                int index_reupdate = item.IndexOf("<span title=\"");
                if (index_reupdate != -1)
                {
                    int update_end = item.IndexOf("<span");
                    updateDay = item.Substring(update_start, update_end - update_start);

                    //  ReUpdateDay
                    update_start = index_reupdate + 13;
                    update_end = item.IndexOf(" 改稿");
                    reUpdateDay = item.Substring(update_start, update_end - update_start);
                }
                else
                {
                    int update_end = item.IndexOf("</dt>");
                    updateDay = item.Substring(update_start, update_end - update_start);
                }

                list.Add(new NovelPageInfo(title, updateDay, reUpdateDay));
            }

            return list;
        }
    }
}
