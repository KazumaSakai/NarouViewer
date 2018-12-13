using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using NarouViewer.API;

namespace NarouViewer
{
    public class TagLinkLabel : LinkLabel
    {
        private string _model;
        public string model
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

        public TagLinkLabel(string linkName)
        {
            this.model = linkName;

            this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            this.TabStop = true;
        }
        private void OnModelChanged()
        {
            this.Name = model;
            this.Text = model;
        }
    }
}
