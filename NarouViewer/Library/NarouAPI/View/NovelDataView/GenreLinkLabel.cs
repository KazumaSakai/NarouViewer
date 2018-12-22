using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using NarouViewer.API;

namespace NarouViewer
{
    public class GenreLinkLabel : LinkLabel, INovelData
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

        public GenreLinkLabel(NovelData model)
        {
            this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            this.Name = "GenreLink";
            this.Size = new Size(492, 18);
            this.TabStop = true;

            //  model
            this.model = model;
        }

        private void OnModelChanged()
        {
            if (model == null) return;
            this.Text = SearchParameter.genreint2String[model.genre];
        }
    }
}
