using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using NarouViewer.API;

namespace NarouViewer
{
    public class NovelInfoLabel : Label, INovelData
    {
        private NarouAPI.NovelData _model;
        public NarouAPI.NovelData model
        {
            set
            {
                _model = value;
                ChangeModel();
            }
            get
            {
                return _model;
            }
        }

        public NovelInfoLabel(NarouAPI.NovelData model)
        {
            this.Name = "NovelPageNumber";
            this.Size = new Size(115, 203);

            //  model
            this.model = model;
        }

        private void ChangeModel()
        {
            if (model == null) return;

            string novelend = model.end == 0 ? (model.novel_type == 1 ? "完結済小説" : "短編小説") : "連載中";
            this.Text = novelend + (model.novel_type == 1 ? "\r\n(全" + model.general_all_no + "部分)" : "");
            this.TextAlign = ContentAlignment.MiddleCenter;
        }
    }
}
