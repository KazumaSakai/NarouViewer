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
    public class NovelDataListView : Panel
    {
        private List<NarouAPI.NovelData> model;
        private List<NovelDataView> novelDataViews;

        private Control beforeParent;
        private EventHandler parentSizeChangedEvent;

        public NovelDataListView(List<NarouAPI.NovelData> model)
        {
            this.model = model;
            this.novelDataViews = new List<NovelDataView>();

            this.AutoScroll = true;
            this.Size = new Size(720, 444);
            this.Scroll += new ScrollEventHandler(OnScrollCallBack);
            this.ParentChanged += new EventHandler(OnParentChanged);
            this.parentSizeChangedEvent = new EventHandler(OnParentSizeChanged);

            int line = 0;
            foreach (NarouAPI.NovelData data in model)
            {
                if (data.title == null) continue;

                NovelDataView view = new NovelDataView(data);
                view.Location = new Point(3, 3 + (line * 262));
                line++;

                this.Controls.Add(view);
                this.novelDataViews.Add(view);
            }
        }

        private void OnParentChanged(object sender, EventArgs e)
        {
            if(beforeParent != null)
            {
                beforeParent.SizeChanged -= parentSizeChangedEvent;
            }

            this.OnParentSizeChanged(sender, e);
            this.Parent.SizeChanged += parentSizeChangedEvent;
            this.beforeParent = this.Parent;
        }
        private void OnParentSizeChanged(object sender, EventArgs e)
        {
            this.Size = new Size(this.Parent.Size.Width - 18, this.Parent.Size.Height - 42);
        }
        private void OnScrollCallBack(object sender, ScrollEventArgs e)
        {
            this.VerticalScroll.Value = e.NewValue;
            this.Update();
        }
    }
}
