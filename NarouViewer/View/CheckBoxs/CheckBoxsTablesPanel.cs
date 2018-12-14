using NarouViewer.API;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NarouViewer
{
    public class CheckBoxsTablesPanel : Panel, IRequestSize, IAnimationOpen
    {
        #region --- Controller ---
        private StringEventHandler controller;
        #endregion

        #region --- IRequestSize ---
        private Size _RequestSize;
        public Size RequestSize
        {
            get
            {
                return new Size(_RequestSize.Width, isOpen ? _RequestSize.Height : 0);
            }
            set
            {
                _RequestSize = value;
            }
        }
        #endregion

        #region --- 子コントロール ---
        private Label[] titles;
        private CheckBoxsTable[] tables;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="model"></param>
        public CheckBoxsTablesPanel((string[][] word, string title, int line)[] data, StringEventHandler controller)
        {
            //  Init
            this.DoubleBuffered = true;

            //  Table Line
            this.titles = new Label[data.Length];
            this.tables = new CheckBoxsTable[data.Length];
            int nowY = 15;
            for (int i = 0; i < data.Length; i++)
            {
                Label title = new DefaultLabel(data[i].title, "title", new Point(11, nowY), true);
                nowY += title.Height + 3;
                CheckBoxsTable table = new CheckBoxsTable(data[i].word, controller, data[i].line) { Location = new Point(8, nowY) };
                nowY += table.Height + 15;

                this.titles[i] = title;
                this.tables[i] = table;

                this.Controls.Add(title);
                this.Controls.Add(table);
            }

            //  Size
            this.RequestSize = new Size(682, nowY);
            this.Size = RequestSize;
            this.SizeChanged += new EventHandler(OnSizeChanged);

            //  Controller
            this.controller = controller;
        }

        /// <summary>
        /// サイズが変更された時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSizeChanged(object sender, EventArgs e)
        {
            IUpdateView updateView = this.Parent as IUpdateView;
            if (updateView == null) return;

            updateView.UpdateView();
        }

        #region --- アニメーション ---
        private bool isOpen = false;
        #endregion
        public bool AnimationOpen(bool use_Animation, bool open)
        {
            isOpen = open;
            SizeUpdate(use_Animation);
            return isOpen;
        }
        public bool AnimationOpen(bool use_Animation)
        {
            return AnimationOpen(!isOpen);
        }
        private void SizeUpdate(bool use_Animation = true)
        {
            if (use_Animation)
            {
                Size oldSize = this.Size;
                Size deltaSize = RequestSize - oldSize;
                int needFrame = Math.Abs(deltaSize.Height) / 70;

                Animator.Animate(Math.Max(1, needFrame), (frame, frequency) =>
                {
                    if (!Visible || IsDisposed) return false;

                    double value = (double)frame / (double)frequency;

                    this.SuspendLayout();
                    this.Size = new Size(oldSize.Width + (int)(deltaSize.Width * value), oldSize.Height + (int)(deltaSize.Height * value));
                    this.ResumeLayout();
                    return true;
                });
            }
            else
            {
                this.Size = RequestSize;
            }
        }
    }
}
