using System;
using System.Windows.Forms;
using System.Drawing;
using NarouViewer.API;

namespace NarouViewer
{
    public class SearchTextBox : TextBox
    {
        private NarouAPI.GetParameter _model;
        public NarouAPI.GetParameter model
        {
            get
            {
                OnModelChanged();
                return _model;
            }
            set
            {
                _model = value;
            }
        }

        public SearchTextBox(NarouAPI.GetParameter model)
        {
            this.Cursor = Cursors.IBeam;
            this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            this.Name = "searchTextBox";
            this.Size = new Size(514, 25);
            this.TextChanged += new EventHandler(OnTextChanged);
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            this.model.word = this.Text;
        }
        private void OnModelChanged()
        {
            if (this.model == null) return;

            this.model.word = this.Text;
        }
    }
}
