using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using NarouViewer.API;

namespace NarouViewer
{
    public class BookmarkLabel : Label, INovelData
    {
        private NarouAPI.NovelData _model;
        public NarouAPI.NovelData model
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

        public BookmarkLabel(NarouAPI.NovelData model)
        {
            this.AutoSize = true;
            this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            this.ForeColor = Color.Black;
            this.Location = new Point(505, 236);
            this.Margin = new Padding(3, 0, 0, 3);
            this.Size = new Size(191, 18);
            this.Name = "BookMarkPoint";

            //  model
            this.model = model;
        }

        private void OnModelChanged()
        {
            if (model == null) return;
            this.Text = "ブックマーク： " + model.fav_novel_cnt + "件 ";
        }
    }
}
