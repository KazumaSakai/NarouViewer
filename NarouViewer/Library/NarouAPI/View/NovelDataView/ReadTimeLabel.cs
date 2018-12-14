using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using NarouViewer.API;

namespace NarouViewer
{
    public class ReadTimeLabel : Label, INovelData
    {
        private NarouAPI.NovelData _model;
        public NarouAPI.NovelData model
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

        public ReadTimeLabel(NarouAPI.NovelData model)
        {
            this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            this.Size = new Size(287, 18);
            this.Name = "ReadTime";
            this.TextAlign = ContentAlignment.MiddleRight;

            //  model
            this.model = model;
        }

        private void OnChangeModel()
        {
            if (model == null) return;
            this.Text = "読了時間：約" + model.time + "分 （" + model.length + "文字）";
        }
    }

}
