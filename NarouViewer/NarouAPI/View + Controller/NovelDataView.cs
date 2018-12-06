using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using NarouViewer.API;

namespace NarouViewer
{
    public class NovelDataView : Panel
    {
        private interface IChangeModel
        {
            NarouAPI.NovelData model
            {
                get;
                set;
            }
        }

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

        private WriteLink writeLink;
        private WriteLabel writerLabel;
        private TitleLink titleLink;
        private NovelInfo novelInfo;
        private GenreLabel genreLabel;
        private SummryLabel summaryLabel;
        private EvaluatorLabel evaluatorLabel;
        private TotalPointLabel totalPointLabel;
        private UniqueUserLabel uniqueuserLabel;
        private LastupLabel lastupLabel;
        private TagsPanel tagsPanel;
        private KeyWordLabel keywordLabel;
        private GenreLink genreLink;
        private ReadTimeLabel readTimeLabel;
        private BookmarkLabel bookmarkLabel;
        private EvaluationLabel evaluationLabel;
        private PCUploadLabel pcUploadPanel;
        private PhoneUploadLabel phoneUploadLabel;
        private IllustrationLabel illustrationLabel;
        private ReviewLabel reviewLabel;

        private List<IChangeModel> iChangeModels;

        public NovelDataView(NarouAPI.NovelData model)
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Location = new Point(12, 12);
            this.Name = "NovelDataVC";
            this.Size = new Size(700, 259);
            this.iChangeModels = new List<IChangeModel>();

            this.SuspendLayout();

            this.Controls.Add(this.titleLink = new TitleLink(model));
            this.Controls.Add(this.novelInfo = new NovelInfo(model));
            this.Controls.Add(this.writerLabel = new WriteLabel(model));
            this.Controls.Add(this.writeLink = new WriteLink(model));
            this.Controls.Add(this.summaryLabel = new SummryLabel(model));
            this.Controls.Add(this.genreLabel = new GenreLabel(model));
            this.Controls.Add(this.genreLink = new GenreLink(model));
            this.Controls.Add(this.keywordLabel = new KeyWordLabel(model));
            this.Controls.Add(this.tagsPanel = new TagsPanel(model));
            this.Controls.Add(this.lastupLabel = new LastupLabel(model));
            this.Controls.Add(this.readTimeLabel = new ReadTimeLabel(model));
            this.Controls.Add(this.uniqueuserLabel = new UniqueUserLabel(model));
            this.Controls.Add(this.reviewLabel = new ReviewLabel(model));
            this.Controls.Add(this.pcUploadPanel = new PCUploadLabel(model));
            this.Controls.Add(this.phoneUploadLabel = new PhoneUploadLabel(model));
            this.Controls.Add(this.illustrationLabel = new IllustrationLabel(model));
            this.Controls.Add(this.totalPointLabel = new TotalPointLabel(model));
            this.Controls.Add(this.evaluatorLabel = new EvaluatorLabel(model));
            this.Controls.Add(this.evaluationLabel = new EvaluationLabel(model));
            this.Controls.Add(this.bookmarkLabel = new BookmarkLabel(model));

            this.iChangeModels.Add(titleLink);
            this.iChangeModels.Add(novelInfo);
            this.iChangeModels.Add(writeLink);
            this.iChangeModels.Add(writerLabel);
            this.iChangeModels.Add(summaryLabel);
            this.iChangeModels.Add(genreLabel);
            this.iChangeModels.Add(genreLink);
            this.iChangeModels.Add(keywordLabel);
            this.iChangeModels.Add(tagsPanel);
            this.iChangeModels.Add(lastupLabel);
            this.iChangeModels.Add(readTimeLabel);
            this.iChangeModels.Add(uniqueuserLabel);
            this.iChangeModels.Add(reviewLabel);
            this.iChangeModels.Add(pcUploadPanel);
            this.iChangeModels.Add(phoneUploadLabel);
            this.iChangeModels.Add(illustrationLabel);
            this.iChangeModels.Add(evaluatorLabel);
            this.iChangeModels.Add(evaluationLabel);
            this.iChangeModels.Add(bookmarkLabel);

            this.ResumeLayout(false);
            this.PerformLayout();

            //  Model
            this.model = model;
        }

        private void ChangeModel()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)ChangeModel);
                return;
            }

            if (model == null) return;

            foreach (IChangeModel item in iChangeModels)
            {
                item.model = this.model;
            }
        }

        private class TitleLink : LinkLabel, IChangeModel
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

            public TitleLink(NarouAPI.NovelData model)
            {
                this.ActiveLinkColor = Color.FromArgb(255, 158, 30);
                this.AutoSize = true;
                this.Font = new Font("ＭＳ Ｐゴシック", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 128);
                this.LinkColor = Color.FromArgb(255, 128, 0);
                this.Location = new Point(3, 3);
                this.Margin = new Padding(3);
                this.Name = "TitleLink";
                this.Size = new Size(125, 21);
                this.TabStop = true;
                this.VisitedLinkColor = Color.FromArgb(255, 128, 0);
                this.LinkClicked += new LinkLabelLinkClickedEventHandler(LinkClick);

                //  Model
                this.model = model;
            }

            private void LinkClick(object sender, LinkLabelLinkClickedEventArgs e)
            {
                System.Diagnostics.Process.Start("https://ncode.syosetu.com/" + model.ncode + "/");
            }
            private void ChangeModel()
            {
                if (model == null) return;
                this.Text = model.title;
            }
        }
        private class NovelInfo : Label, IChangeModel
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

            public NovelInfo(NarouAPI.NovelData model)
            {
                this.AutoSize = true;
                this.Location = new Point(3, 52);
                this.MaximumSize = new Size(115, 203);
                this.MinimumSize = new Size(115, 203);
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
        private class WriteLabel : Label, IChangeModel
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

            public WriteLabel(NarouAPI.NovelData model)
            {
                this.AutoSize = true;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F);
                this.Location = new Point(3, 30);
                this.Margin = new Padding(3);
                this.Name = "Writer";
                this.Size = new Size(48, 16);
                this.Text = "作者 :";

                //  model
                this.model = model;
            }
            private void ChangeModel()
            {
                if (model == null) return;
            }
        }
        private class WriteLink : LinkLabel, IChangeModel
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

            public WriteLink(NarouAPI.NovelData model)
            {
                this.AutoSize = true;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F);
                this.Location = new Point(55, 30);
                this.Margin = new Padding(3);
                this.Name = "WriteLink";
                this.Size = new Size(56, 16);
                this.TabStop = true;
                this.LinkClicked += new LinkLabelLinkClickedEventHandler(LinkClick);

                //  model
                this.model = model;
            }
            private void LinkClick(object sender, LinkLabelLinkClickedEventArgs e)
            {
                System.Diagnostics.Process.Start("https://mypage.syosetu.com/" + model.userid + "/");
            }
            private void ChangeModel()
            {
                if (model == null) return;
                this.Text = model.writer;
            }
        }
        private class SummryLabel : Label, IChangeModel
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

            public SummryLabel(NarouAPI.NovelData model)
            {
                this.AutoSize = true;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.ForeColor = Color.FromArgb(110, 64, 0);
                this.Location = new Point(121, 52);
                this.Margin = new Padding(3);
                this.MaximumSize = new Size(575, 50);
                this.MinimumSize = new Size(575, 50);
                this.Name = "Summary";
                this.Size = new Size(575, 50);

                //  model
                this.model = model;
            }
            private void ChangeModel()
            {
                if (model == null) return;
                this.Text = model.story;
            }
        }
        private class GenreLabel : Label, IChangeModel
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

            public GenreLabel(NarouAPI.NovelData model)
            {
                this.AutoSize = true;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(121, 105);
                this.Margin = new Padding(0);
                this.Name = "Genre";
                this.Size = new Size(80, 18);
                this.Text = "ジャンル : ";

                //  model
                this.model = model;
            }

            private void ChangeModel()
            {
                if (model == null) return;
            }
        }
        private class GenreLink : LinkLabel, IChangeModel
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

            public GenreLink(NarouAPI.NovelData model)
            {
                this.AutoSize = true;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(204, 105);
                this.MaximumSize = new Size(492, 18);
                this.MinimumSize = new Size(492, 18);
                this.Name = "GenreLink";
                this.Size = new Size(492, 18);
                this.TabStop = true;
                this.LinkClicked += new LinkLabelLinkClickedEventHandler(LinkClick);

                //  model
                this.model = model;
            }

            private void LinkClick(object sender, LinkLabelLinkClickedEventArgs e)
            {
            }

            private void ChangeModel()
            {
                if (model == null) return;
                this.Text = model.genre_name;
            }
        }
        private class KeyWordLabel : Label, IChangeModel
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

            public KeyWordLabel(NarouAPI.NovelData model)
            {
                this.AutoSize = true;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(120, 126);
                this.Margin = new Padding(3, 3, 0, 3);
                this.Name = "Keyword";
                this.Size = new Size(94, 18);
                this.Text = "キーワード : ";

                //  model
                this.model = model;
            }

            private void ChangeModel()
            {
                if (model == null) return;
            }
        }
        private class TagsPanel : Panel, IChangeModel
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
            private List<TagLink> tagLinks;

            public TagsPanel(NarouAPI.NovelData model)
            {
                this.Location = new Point(217, 126);
                this.Name = "TagsPanel";
                this.Size = new Size(480, 44);

                //  model
                this.model = model;
            }

            private void ChangeModel()
            {
                if (model == null) return;
                if (model.keyword == null) return;
                if (tagLinks == null) tagLinks = new List<TagLink>();

                string[] keywords = model.keyword.Split(' ');
                int needCount = keywords.Length - tagLinks.Count;

                if (needCount > 0)
                {
                    for (int i = 0; i < needCount; i++)
                    {
                        TagLink tagLink = new TagLink("");
                        this.tagLinks.Add(tagLink);
                        this.Controls.Add(tagLink);
                    }
                }
                else if(needCount < 0)
                {
                    for (int i = 0; i < -needCount; i++)
                    {
                        TagLink tagLink = tagLinks.Last();

                        this.tagLinks.Remove(tagLink);
                        this.Controls.Remove(tagLink);
                    }
                }

                //  文字の大きさ取得用
                Font stringFont = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                Graphics g = Graphics.FromHwnd(this.Handle);

                //  タグリンク作成
                int nowLine = 0;
                int nextX = 2;
                for (int i = 0; i < keywords.Length; i++)
                {
                    string keyword = keywords[i];
                    TagLink tagLink = tagLinks[i];

                    //  文字の大きさ取得
                    SizeF f = g.MeasureString(keyword, stringFont);
                    int xSize = (int)System.Math.Ceiling(f.Width) + 1;

                    //  改行
                    if (nextX != 2 && nextX + xSize > this.Size.Width)
                    {
                        nowLine++;
                        nextX = 2;
                    }

                    tagLink.model = keyword;
                    tagLink.Location = new Point(nextX, 2 + (nowLine * 22));
                    tagLink.Size = new Size(xSize, 18);

                    nextX += xSize + 1;
                }

                stringFont.Dispose();
                g.Dispose();
            }

            private class TagLink : LinkLabel
            {
                private string _model;
                public string model
                {
                    set
                    {
                        _model = value;
                        ModelChanged();
                    }
                    get
                    {
                        return _model;
                    }
                }

                public TagLink(string linkName)
                {
                    this.model = linkName;

                    this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                    this.TabStop = true;
                }

                private void ModelChanged()
                {
                    this.Name = model;
                    this.Text = model;
                }
            }
        }
        private class LastupLabel : Label, IChangeModel
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

            public LastupLabel(NarouAPI.NovelData model)
            {
                this.AutoSize = true;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(120, 173);
                this.Margin = new Padding(3, 0, 0, 3);
                this.MaximumSize = new Size(287, 18);
                this.MinimumSize = new Size(287, 18);
                this.Name = "Lastup";
                this.Size = new Size(287, 18);

                //  model
                this.model = model;
            }

            private void ChangeModel()
            {
                if (model == null) return;
                this.Text = "最終更新日：" + model.general_lastup.Replace('-', '/');
            }
        }
        private class ReadTimeLabel : Label, IChangeModel
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

            public ReadTimeLabel(NarouAPI.NovelData model)
            {
                this.AutoSize = true;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(409, 173);
                this.Margin = new Padding(3, 0, 0, 3);
                this.MaximumSize = new Size(287, 18);
                this.MinimumSize = new Size(287, 18);
                this.Name = "ReadTime";
                this.Size = new Size(287, 18);
                this.TextAlign = ContentAlignment.MiddleRight;

                //  model
                this.model = model;
            }

            private void ChangeModel()
            {
                if (model == null) return;
                this.Text = "読了時間：約" + model.time + "分 （" + model.length + "文字）";
            }
        }
        private class UniqueUserLabel : Label, IChangeModel
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

            public UniqueUserLabel(NarouAPI.NovelData model)
            {
                this.AutoSize = true;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(120, 194);
                this.Margin = new Padding(3, 0, 0, 3);
                this.MaximumSize = new Size(216, 18);
                this.MinimumSize = new Size(214, 18);
                this.Name = "UniqueUser";
                this.Size = new Size(215, 18);

                //  model
                this.model = model;
            }

            private void ChangeModel()
            {
                if (model == null) return;
                this.Text = "週別ユニークユーザ： " + model.weekly_unique + "人";
            }
        }
        private class ReviewLabel : Label, IChangeModel
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

            public ReviewLabel(NarouAPI.NovelData model)
            {
                this.AutoSize = true;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.ImageAlign = ContentAlignment.MiddleLeft;
                this.Location = new Point(337, 194);
                this.Margin = new Padding(2, 0, 0, 3);
                this.MaximumSize = new Size(147, 18);
                this.MinimumSize = new Size(147, 18);
                this.Name = "Review";
                this.Size = new Size(147, 18);
                this.TextAlign = ContentAlignment.MiddleLeft;

                //  model
                this.model = model;
            }

            private void ChangeModel()
            {
                if (model == null) return;
                this.Text = "レビュー数： " + model.review_cnt + "件";
            }
        }
        private class PCUploadLabel : Label, IChangeModel
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

            public PCUploadLabel(NarouAPI.NovelData model)
            {
                this.AutoSize = true;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(486, 194);
                this.Margin = new Padding(2, 0, 0, 3);
                this.MaximumSize = new Size(63, 18);
                this.MinimumSize = new Size(63, 18);
                this.Name = "PCUpload";
                this.Size = new Size(63, 18);
                this.TextAlign = ContentAlignment.MiddleCenter;

                //  model
                this.model = model;
            }

            private void ChangeModel()
            {
                if (model == null) return;
                this.Text = model.pc_or_k != 1 ? "PC投稿" : "";
            }
        }
        private class PhoneUploadLabel : Label, IChangeModel
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

            public PhoneUploadLabel(NarouAPI.NovelData model)
            {
                this.AutoSize = true;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(551, 194);
                this.Margin = new Padding(2, 0, 0, 3);
                this.MaximumSize = new Size(74, 18);
                this.MinimumSize = new Size(74, 18);
                this.Name = "PhoneUploadLabel";
                this.Size = new Size(74, 18);
                this.TextAlign = ContentAlignment.MiddleCenter;

                //  model
                this.model = model;
            }

            private void ChangeModel()
            {
                if (model == null) return;
                this.Text = model.pc_or_k != 2 ? "携帯投稿" : "";
            }
        }
        private class IllustrationLabel : Label, IChangeModel
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

            public IllustrationLabel(NarouAPI.NovelData model)
            {
                this.AutoSize = true;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(627, 194);
                this.Margin = new Padding(2, 0, 0, 3);
                this.MaximumSize = new Size(69, 18);
                this.MinimumSize = new Size(69, 18);
                this.Name = "Illustration";
                this.Size = new Size(69, 18);
                this.TextAlign = ContentAlignment.MiddleCenter;

                //  model
                this.model = model;
            }

            private void ChangeModel()
            {
                if (model == null) return;
                this.Text = model.sasie_cnt > 0 ? "挿絵あり" : "";
            }
        }
        private class TotalPointLabel : Label, IChangeModel
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

            public TotalPointLabel(NarouAPI.NovelData model)
            {
                this.AutoSize = true;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.ForeColor = Color.Red;
                this.Location = new Point(120, 215);
                this.Margin = new Padding(3, 0, 0, 3);
                this.MaximumSize = new Size(576, 18);
                this.MinimumSize = new Size(576, 18);
                this.Name = "TotalPoint";
                this.Size = new Size(576, 18);

                //  model
                this.model = model;
            }

            private void ChangeModel()
            {
                if (model == null) return;
                this.Text = "総合評価ポイント： " + model.global_point + " pt";
            }
        }
        private class EvaluatorLabel : Label, IChangeModel
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

            public EvaluatorLabel(NarouAPI.NovelData model)
            {
                this.AutoSize = true;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.ForeColor = Color.Black;
                this.Location = new Point(120, 236);
                this.Margin = new Padding(3, 0, 0, 3);
                this.MaximumSize = new Size(189, 18);
                this.MinimumSize = new Size(189, 18);
                this.Name = "EvaluatorNumber";
                this.Size = new Size(189, 18);

                //  model
                this.model = model;
            }

            private void ChangeModel()
            {
                if (model == null) return;
                this.Text = "評価人数： " + model.all_hyoka_cnt + "人";
            }
        }
        private class EvaluationLabel : Label, IChangeModel
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

            public EvaluationLabel(NarouAPI.NovelData model)
            {
                this.AutoSize = true;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.ForeColor = Color.Black;
                this.Location = new Point(311, 236);
                this.Margin = new Padding(3, 0, 0, 3);
                this.MaximumSize = new Size(192, 18);
                this.MinimumSize = new Size(192, 18);
                this.Name = "EvaluationPoints";
                this.Size = new Size(192, 18);

                //  model
                this.model = model;
            }

            private void ChangeModel()
            {
                if (model == null) return;
                this.Text = "評価点： " + model.all_point + " pt";
            }
        }
        private class BookmarkLabel : Label, IChangeModel
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

            public BookmarkLabel(NarouAPI.NovelData model)
            {
                this.AutoSize = true;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.ForeColor = Color.Black;
                this.Location = new Point(505, 236);
                this.Margin = new Padding(3, 0, 0, 3);
                this.MaximumSize = new Size(191, 18);
                this.MinimumSize = new Size(191, 18);
                this.Name = "BookMarkPoint";
                this.Size = new Size(191, 18);

                //  model
                this.model = model;
            }

            private void ChangeModel()
            {
                if (model == null) return;
                this.Text = "ブックマーク： " + model.fav_novel_cnt + "件 ";
            }
        }
    }
}
