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
        private List<NarouAPI.NovelData> _model;
        public List<NarouAPI.NovelData> model
        {
            set
            {
                _model = value;
                ChangeModel();
            }
            get
            {
                return _model;
            }
        }
        private List<NovelDataView> novelDataViews;

        private Control beforeParent;
        private EventHandler parentSizeChangedEvent;

        public NovelDataListView(List<NarouAPI.NovelData> model)
        {
            this.novelDataViews = new List<NovelDataView>();

            this.Size = new Size(706, 444);
            this.Location = new Point(3, 180);
            this.Scroll += new ScrollEventHandler(OnScrollCallBack);
            this.ParentChanged += new EventHandler(OnParentChanged);
            this.parentSizeChangedEvent = new EventHandler(OnParentSizeChanged);

            //  model
            this.model = model;
        }

        private void ChangeModel()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)ChangeModel);
                return;
            }

            if (model == null) return;

            model.RemoveAll((a) => a == null || a.title == null || a.keyword == null || a.userid == 0);
            this.Size = new Size(this.Width, 262 * model.Count + 6);

            int needModelCount = model.Count - novelDataViews.Count;

            if (needModelCount > 0)
            {
                for (int i = 0; i < needModelCount; i++)
                {
                    NovelDataView view = new NovelDataView(null);
                    view.Location = new Point(3, 3 + (novelDataViews.Count * 262));

                    this.Controls.Add(view);

                    this.novelDataViews.Add(view);
                }
            }
            else if (needModelCount < 0)
            {
                for (int i = 0; i < -needModelCount; i++)
                {
                    NovelDataView view = this.novelDataViews.Last();
                    this.novelDataViews.Remove(view);
                    this.Controls.Remove(view);
                    view.Dispose();
                }
            }

            for (int i = 0; i < model.Count; i++)
            {
                novelDataViews[i].model = model[i];
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
        }
        private void OnScrollCallBack(object sender, ScrollEventArgs e)
        {
            this.VerticalScroll.Value = e.NewValue;
            this.Update();
        }
    }
}
