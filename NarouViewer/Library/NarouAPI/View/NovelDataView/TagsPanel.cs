using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using NarouViewer.API;

namespace NarouViewer
{
    public class TagsPanel : Panel, INovelData
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
            
        public StringEventHandler onClickedTags;

        private List<TagLinkLabel> tagLinks = new List<TagLinkLabel>();

        public TagsPanel(NovelData model)
        {
            this.Name = "TagsPanel";

            //  model
            this.model = model;
        }

        private void OnModelChanged()
        {
            if (model == null || model.keyword == null) return;

            string[] keywords = model.keyword.Split(' ');

            //  必要数を数え、削除 or 追加
            int needCount = keywords.Length - tagLinks.Count;
            if (needCount > 0)
            {
                for (int i = 0; i < needCount; i++)
                {
                    TagLinkLabel tagLink = new TagLinkLabel("");

                    string tag_keyword = keywords[tagLinks.Count];
                    tagLink.LinkClicked += new LinkLabelLinkClickedEventHandler((object s, LinkLabelLinkClickedEventArgs e) => this.onClickedTags?.Invoke(tag_keyword));
                    this.tagLinks.Add(tagLink);
                    this.Controls.Add(tagLink);
                }
            }
            else if (needCount < 0)
            {
                for (int i = 0; i < -needCount; i++)
                {
                    TagLinkLabel tagLink = tagLinks.Last();

                    this.tagLinks.Remove(tagLink);
                    this.Controls.Remove(tagLink);
                }
            }

            //  文字の大きさ取得用
            using (Font stringFont = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128))
            {
                using (Graphics g = Graphics.FromHwnd(this.Handle))
                {
                    //  タグリンク作成
                    int nowLine = 0;
                    int nextX = 2;
                    for (int i = 0; i < keywords.Length; i++)
                    {
                        string keyword = keywords[i];
                        TagLinkLabel tagLink = tagLinks[i];

                        //  文字の大きさ取得
                        SizeF f = g.MeasureString(keyword, stringFont);
                        int xSize = (int)System.Math.Ceiling(f.Width) + 1;

                        //  改行
                        if (nextX != 2 && nextX + xSize > 480)
                        {
                            nowLine++;
                            nextX = 2;
                        }

                        tagLink.model = keyword;
                        tagLink.Location = new Point(nextX, 2 + (nowLine * 22));
                        tagLink.Size = new Size(xSize, 18);

                        nextX += xSize + 1;
                    }
                    this.Size = new Size(480, 22 * (nowLine + 1));
                }
            }
        }
    }
}
