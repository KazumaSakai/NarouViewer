using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using NarouViewer.API;

namespace NarouViewer
{
    public class IllustrationLabel : Label, INovelData
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

        public IllustrationLabel(NarouAPI.NovelData model)
        {
            this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            this.Name = "Illustration";
            this.Size = new Size(69, 18);
            this.TextAlign = ContentAlignment.MiddleCenter;

            //  model
            this.model = model;
        }

        private void OnModelChanged()
        {
            if (model == null) return;
            this.Text = model.sasie_cnt > 0 ? "挿絵あり" : "";
        }
    }

}
