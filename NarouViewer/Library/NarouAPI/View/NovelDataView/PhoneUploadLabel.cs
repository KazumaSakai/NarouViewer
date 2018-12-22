using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using NarouViewer.API;

namespace NarouViewer
{
    public class PhoneUploadLabel : Label, INovelData
    {
        private NovelData _model;
        public NovelData model
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

        public PhoneUploadLabel(NovelData model)
        {
            this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            this.Size = new Size(74, 18);
            this.Name = "PhoneUploadLabel";
            this.TextAlign = ContentAlignment.MiddleCenter;

            //  model
            this.model = model;
        }
        private void OnModelChanged()
        {
            if (model == null) return;
            this.Text = model.pc_or_k != 2 ? "携帯投稿" : "";
        }
    }

}
