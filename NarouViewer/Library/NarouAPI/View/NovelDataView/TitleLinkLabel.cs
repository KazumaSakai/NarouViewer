﻿using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using NarouViewer.API;

namespace NarouViewer
{
    public class TitleLinkLabel : LinkLabel, INovelData
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

        public TitleLinkLabel(NovelData model)
        {
            this.ActiveLinkColor = Color.FromArgb(255, 158, 30);
            this.AutoSize = false;
            this.Font = new Font("ＭＳ Ｐゴシック", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 128);
            this.LinkColor = Color.FromArgb(255, 128, 0);
            this.Margin = new Padding(3);
            this.Name = "TitleLink";
            this.TabStop = true;
            this.VisitedLinkColor = Color.FromArgb(255, 128, 0);

            //  Model
            this.model = model;
        }

        private void OnModelChanged()
        {
            if (model == null) return;

            model.title = HTMLSafeString.Decode(model.title, HTMLSafeString.EncodeType.semicolon);

            this.Text = model.title;
            this.Size = GetLabelSize();
        }

        private Size GetLabelSize()
        {
            Size size = Size.Empty;

            //  文字の大きさ取得用
            using (Graphics g = Graphics.FromHwnd(this.Handle))
            {
                //  文字の大きさ取得
                SizeF f = g.MeasureString(this.model.title, this.Font, 700);
                size = new Size(692, (int)Math.Ceiling(f.Height));
            }

            return size;
        }
    }
}
