using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using NarouViewer.API;

namespace NarouViewer
{
    public class PCUploadLabel : Label, INovelData
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

        public PCUploadLabel(NovelData model)
        {
            this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            this.Size = new Size(63, 18);
            this.Name = "PCUpload";
            this.TextAlign = ContentAlignment.MiddleCenter;

            //  model
            this.model = model;
        }

        private void OnModelChanged()
        {
            if (model == null) return;
            this.Text = model.pc_or_k != 1 ? "PC投稿" : "";
        }
    }

}
