using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using NarouViewer.API;

namespace NarouViewer
{
    public class ReviewLabel : Label, INovelData
    {
        private NovelData _model;
        public NovelData model
        {
            set
            {
                _model = value;
                OnChangeModel();
            }
            get
            {
                return _model;
            }
        }

        public ReviewLabel(NovelData model)
        {
            this.AutoSize = true;
            this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            this.ImageAlign = ContentAlignment.MiddleLeft;
            this.TextAlign = ContentAlignment.MiddleLeft;
            this.Name = "Review";
            this.Size = new Size(147, 18);

            //  model
            this.model = model;
        }

        private void OnChangeModel()
        {
            if (model == null) return;
            this.Text = "レビュー数： " + model.review_cnt + "件";
        }
    }

}
