using System;
using System.Windows.Forms;
using System.Drawing;
using NarouViewer.API;

namespace NarouViewer
{
    public class SearchTextBox : TextBox
    {
        private SearchParameter _model;
        public SearchParameter model
        {
            get
            {
                return _model;
            }
            set
            {
                OnModelChanged();
                _model = value;
            }
        }

        public SearchTextBox(SearchParameter model)
        {
            this.Cursor = Cursors.IBeam;
            this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            this.Name = "searchTextBox";
            this.Size = new Size(514, 25);
        }
        private void OnModelChanged()
        {
            if (this.model == null) return;

            this.model.word = this.Text;
        }
    }
}
