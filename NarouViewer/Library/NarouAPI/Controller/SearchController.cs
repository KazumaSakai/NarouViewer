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
    public class SearchController
    {
        public SearchPrameterController searchPrameterController;
        public NovelDataController novelDataContoller;

        public SearchController()
        {
            this.searchPrameterController = new SearchPrameterController();
            this.novelDataContoller = new NovelDataController();
        }
    }
}
