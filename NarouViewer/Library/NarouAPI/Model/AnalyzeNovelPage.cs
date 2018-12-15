using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarouViewer
{
    class AnalyzeNovelPage
    {
        public static List<(string title, string updateDay, string reUpdateDay)> Analyaze(string html)
        {
            string[] episodes = html.Split(new string[] { "<dl class=\"novel_sublist2\">" }, StringSplitOptions.None);

            List<(string title, string updateDay, string reUpdateDay)> list = new List<(string title, string updateDay, string reUpdateDay)>(episodes.Length - 1);

            for (int i = 1; i < episodes.Length; i++)
            {
                string episode_title = "";
                string episode_updateDay = "";
                string episode_reUpdateDay = "";

                string item = episodes[i];
                {
                    item = item.Replace("<dd class=\"subtitle\">", "").Replace("\n", "") + 24;
                    int end = item.IndexOf("</dl>");
                    item = item.Substring(0, end);
                }

                //  Title
                {
                    int start = item.IndexOf(">") + 1;
                    int end = item.IndexOf("</a>");
                    episode_title = item.Substring(start, (end - start));
                }

                //  UpdateDay
                {
                    int start = item.IndexOf("<dt class=\"long_update\">") + 24;

                    int index_reupdate = item.IndexOf("<span title=\"");
                    if (index_reupdate != -1)
                    {
                        int end = item.IndexOf("<span");
                        episode_updateDay = item.Substring(start, end - start);

                        //  ReUpdateDay
                        start = index_reupdate + 13;
                        end = item.IndexOf(" 改稿");
                        episode_reUpdateDay = item.Substring(start, end - start);
                    }
                    else
                    {
                        int end = item.IndexOf("</dt>");
                        episode_updateDay = item.Substring(start, end - start);
                    }
                }
                list.Add((episode_title, episode_updateDay, episode_reUpdateDay));
            }

            return list;
        }
    }
}
