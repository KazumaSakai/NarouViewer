using NarouViewer.API;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NarouViewer
{
    public class WriterLinkLabel : LinkLabel, INovelData
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

        public WriterLinkLabel(NovelData model)
        {
            this.Font = new Font("ＭＳ Ｐゴシック", 12F);
            this.Name = "WriteLink";
            this.Size = new Size(56, 16);
            this.TabStop = true;

            //  model
            this.model = model;
        }

        private void OnModelChanged()
        {
            if (model == null) return;
            this.Text = model.writer;
        }
    }
}
