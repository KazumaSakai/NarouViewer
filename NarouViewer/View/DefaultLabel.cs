using System.Windows.Forms;
using System.Drawing;

namespace NarouViewer
{
    public class DefaultLabel : Label
    {
        public DefaultLabel(string text, string name, Point location, bool bold = false)
        {
            this.AutoSize = true;
            this.Font = new Font("ＭＳ Ｐゴシック", 12F, bold ? FontStyle.Bold : FontStyle.Regular, GraphicsUnit.Point, 128);
            this.Location =location;
            this.Name = name;
            this.Text = text;
        }
    }
}
