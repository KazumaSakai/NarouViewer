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

        public SummryLabel(NarouAPI.NovelData model)
        {
            this.AutoSize = true;
            this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            this.ForeColor = Color.FromArgb(110, 64, 0);
            this.Location = new Point(121, 52);
            this.Margin = new Padding(3);
            this.MaximumSize = new Size(575, 1000);
            this.MinimumSize = new Size(575, 0);
            this.Name = "Summary";

            //  model
            this.model = model;
        }
        private void OnModelChanged()
        {
            if (model == null) return;
            this.Text = model.story;
        }
    }
}
