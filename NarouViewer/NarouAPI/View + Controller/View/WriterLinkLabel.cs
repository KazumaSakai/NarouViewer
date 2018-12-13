using NarouViewer.API;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NarouViewer
{
    public class WriterLinkLabel : LinkLabel, INovelData
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

        public WriterLinkLabel(NarouAPI.NovelData model)
        {
            this.Font = new Font("ＭＳ Ｐゴシック", 12F);
            this.Name = "WriteLink";
            this.Size = new Size(56, 16);
            this.TabStop = true;
            this.LinkClicked += new LinkLabelLinkClickedEventHandler(OnLinkClicked);

            //  model
            this.model = model;
        }
        private void OnLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StringBuilder url = new StringBuilder();
            url.AppendFormat("https://mypage.syosetu.com/{0}/", model.userid);

            System.Diagnostics.Process.Start(url.ToString());
        }
        private void OnModelChanged()
        {
            if (model == null) return;
            this.Text = model.writer;
        }
    }
}
