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
    public class NovelDataListView : Panel, IUpdateView
    {
        #region --- Model ---
        private List<NovelData> _model;
        public List<NovelData> model
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
        #endregion

        #region --- Contoller ---
        private NovelDataController controller;
        #endregion

        #region --- Child Control ---
        private List<NovelDataView> novelDataViews;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="model"></param>
        public NovelDataListView(List<NovelData> model, NovelDataController controller)
        {
            this.novelDataViews = new List<NovelDataView>();

            this.Size = new Size(706, 444);
            this.Location = new Point(3, 180);
            this.Scroll += new ScrollEventHandler(OnScrollCallBack);

            //  model
            this.model = model;

            //  Controller
            this.controller = controller;
        }

        /// <summary>
        /// モデル変更
        /// </summary>
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
                    NovelDataView view = new NovelDataView(null, controller);

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

            UpdateView();
        }

        public void UpdateView()
        {
            int nowY = 3;
            for (int i = 0; i < model.Count; i++)
            {
                NovelDataView view = novelDataViews[i];
                view.model = model[i];
                view.Location = new Point(3, nowY);

                nowY += view.Size.Height + 3;
            }

            this.Size = new Size(this.Width, nowY + 6);

            (this.Parent as IUpdateView)?.UpdateView();
        }

        private void OnScrollCallBack(object sender, ScrollEventArgs e)
        {
            this.VerticalScroll.Value = e.NewValue;
            this.Update();
        }
    }
}
