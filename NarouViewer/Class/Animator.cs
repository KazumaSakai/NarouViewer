using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarouViewer
{
    public class Animator
    {
        public static System.Windows.Forms.Timer Animate(int interval, int frequency, Func<int, int, bool> callback)
        {
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = interval;
            int frame = 0;
            timer.Tick += (sender, e) =>
            {
                if (callback(frame, frequency) == false || frame >= frequency)
                {
                    timer.Stop();
                }
                frame++;
            };
            timer.Start();
            return timer;
        }
        public static System.Windows.Forms.Timer Animate(int duration, Func<int, int, bool> callback)
        {
            const int interval = 25;
            if (duration < interval) duration = interval;
            return Animate(25, duration / interval, callback);
        }
    }
}
