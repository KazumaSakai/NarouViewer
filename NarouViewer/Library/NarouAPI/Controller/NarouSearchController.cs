using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using NarouViewer.API;

namespace NarouViewer
{
    public class NarouSearchController
    {
        public delegate void NarouSearchEvent(NarouAPI.SearchParameter parameter);
        public NarouSearchEvent Search;
    }
}
