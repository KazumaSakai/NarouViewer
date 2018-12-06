using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarouViewer
{
    public class Animator
    {
        private static System.Windows.Forms.Timer timer;
        private static List<(EventHandler, int, uint)> animes = new List<(EventHandler, int, uint)>();
        private static uint nextIndex;

        public static uint Animate(int frequency, Func<int, int, bool> callback)
        {
            if (timer == null)
            {
                timer = new System.Windows.Forms.Timer();
                timer.Interval = 10;
                timer.Start();
            }

            (EventHandler, int, uint) thisAnime = (null, 0, nextIndex);

            thisAnime.Item1 = (sender, e) =>
            {
                if (callback(thisAnime.Item2, frequency) == false || thisAnime.Item2 >= frequency)
                {
                    EndAnime(thisAnime.Item3);
                }
                thisAnime.Item2++;
            };
            timer.Tick += thisAnime.Item1;

            animes.Add(thisAnime);
            nextIndex++;

            return thisAnime.Item3;
        }
        public static void EndAnime(uint index)
        {
            (EventHandler, int, uint) anime = animes.Find((a) => a.Item3 == index);
            timer.Tick -= anime.Item1;
            animes.Remove(anime);
        }
    }
}
