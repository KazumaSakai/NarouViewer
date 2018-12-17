using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarouViewer
{
    public class Novel
    {
        public List<NovelPageInfo> pages;

        public Novel(string html)
        {
            this.pages = NovelPageInfo.Hmtl_NovelPageList(html);
        }
    }
}
