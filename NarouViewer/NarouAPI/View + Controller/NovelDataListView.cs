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
                OnModelChanged();
            }
            get
            {
                return _model;
            }
        }

        private List<NovelDataView> novelDataViews;

        public NovelDataListView(List<NarouAPI.NovelData> model)
        {
            this.novelDataViews = new List<NovelDataView>();

            this.Size = new Size(706, 444);
            this.Location = new Point(3, 180);
            this.Scroll += new ScrollEventHandler(OnScrollCallBack);

            //  model
            this.model = model;
        }

        private void OnModelChanged()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)OnModelChanged);
                return;
            }

            if (model == null) return;

            model.RemoveAll((a) => a == null || a.title == null || a.keyword == null || a.userid == 0);

            int needModelCount = model.Count - novelDataViews.Count;

            if (needModelCount > 0)
            {
                for (int i = 0; i < needModelCount; i++)
                {
                    NovelDataView view = new NovelDataView(null);

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

            int nowY = 3;
            for (int i = 0; i < model.Count; i++)
            {
                NovelDataView view = novelDataViews[i];
                view.model = model[i];
                view.Location = new Point(3, nowY);

                nowY += view.Size.Height + 3;
            }

            this.Size = new Size(this.Width, nowY + 6);
        }
        private void OnScrollCallBack(object sender, ScrollEventArgs e)
        {
            this.VerticalScroll.Value = e.NewValue;
            this.Update();
        }
    }
}
