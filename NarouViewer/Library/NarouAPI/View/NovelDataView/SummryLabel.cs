using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using NarouViewer.API;

namespace NarouViewer
{
    public class SummryLabel : Label, INovelData
    {
        #region --- model ---
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
        #endregion

        public SummryLabel(NovelData model)
        {
            this.AutoSize = true;
            this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            this.ForeColor = Color.FromArgb(110, 64, 0);
            this.MaximumSize = new Size(575, 1000);
            this.MinimumSize = new Size(575, 0);
            this.Name = "Summary";

            //  model
            this.model = model;
        }
        private void OnModelChanged()
        {
            if (model == null) return;

            model.story = HTMLSafeString.Decode(model.story, HTMLSafeString.EncodeType.semicolon);
            this.Text = model.story;
        }
    }
}
