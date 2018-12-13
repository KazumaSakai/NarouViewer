using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using NarouViewer.API;

namespace NarouViewer
{
    public class UniqueUserLabel : Label, INovelData
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

        public UniqueUserLabel(NarouAPI.NovelData model)
        {
            this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            this.Name = "UniqueUser";
            this.Size = new Size(215, 18);

            //  model
            this.model = model;
        }

        private void OnChangeModel()
        {
            if (model == null) return;
            this.Text = "週別ユニークユーザ： " + model.weekly_unique + "人";
        }
    }

}
