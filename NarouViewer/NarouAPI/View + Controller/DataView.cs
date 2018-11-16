using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NarouViewer.API
{
    public class DataView : Panel
    {
        public NarouAPI.Data model;

        public DataView(NarouAPI.Data model)
        {
            this.model = model;
            this.DoubleBuffered = true;
        }
    }
}
