using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using NarouViewer.API;

namespace NarouViewer
{
    public class LastUpdateLabel : Label, INovelData
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

        public LastUpdateLabel(NarouAPI.NovelData model)
        {
            this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            this.Name = "Lastup";
            this.Size = new Size(287, 18);

            //  model
            this.model = model;
        }
        private void OnModelChanged()
        {
            if (model == null) return;
            this.Text = "最終更新日：" + model.general_lastup.Replace('-', '/');
        }
    }
}
