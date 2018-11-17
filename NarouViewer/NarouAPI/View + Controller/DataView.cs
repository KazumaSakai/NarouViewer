using System.Windows.Forms;
using System.Drawing;

using NarouViewer.API;

namespace NarouViewer
{
    public class NovelDataVC : Panel
    {
        public NarouAPI.NovelData model;

        private LinkLabel writeLink;
        private Label writerLabel;
        private LinkLabel titleLink;
        private Label novelInfo;
        private Label genreLabel;
        private Label summaryLabel;
        private Label evaluatorLabel;
        private Label totalPointLabel;
        private Label uniqueuserLabel;
        private Label lastupLabel;
        private Panel tagsPanel;
        private Label keywordLabel;
        private LinkLabel genreLink;
        private Label readTimeLabel;
        private Label bookmarkLabel;
        private Label evaluationLabel;
        private Label pcUploadPanel;
        private Label phoneUploadLabel;
        private Label illustrationLabel;
        private Label reviewLabel;

        public NovelDataVC(NarouAPI.NovelData model)
        {
            //  Model
            this.model = model;

            this.BorderStyle = BorderStyle.FixedSingle;
            this.Location = new Point(12, 12);
            this.Name = "NovelDataVC";
            this.Size = new Size(700, 259);


            this.SuspendLayout();

            this.Controls.Add(this.titleLink = new TitleLink(this.model));
            this.Controls.Add(this.novelInfo = new NovelInfo(this.model));
            this.Controls.Add(this.writerLabel = new WriteLabel(this.model));
            this.Controls.Add(this.writeLink = new WriteLink(this.model));
            this.Controls.Add(this.summaryLabel = new SummryLabel(this.model));
            this.Controls.Add(this.genreLabel = new GenreLabel(this.model));
            this.Controls.Add(this.genreLink = new GenreLink(this.model));
            this.Controls.Add(this.keywordLabel = new KeyWordLabel(this.model));
            this.Controls.Add(this.tagsPanel = new TagsPanel(this.model));
            this.Controls.Add(this.lastupLabel = new LastupLabel(this.model));
            this.Controls.Add(this.readTimeLabel = new ReadTimeLabel(this.model));
            this.Controls.Add(this.uniqueuserLabel = new UniqueUserLabel(this.model));
            this.Controls.Add(this.reviewLabel = new ReviewLabel(this.model));
            this.Controls.Add(this.pcUploadPanel = new PCUploadLabel(this.model));
            this.Controls.Add(this.phoneUploadLabel = new PhoneUploadLabel(this.model));
            this.Controls.Add(this.illustrationLabel = new IllustrationLabel(this.model));
            this.Controls.Add(this.totalPointLabel = new TotalPointLabel(this.model));
            this.Controls.Add(this.evaluatorLabel = new EvaluatorLabel(this.model));
            this.Controls.Add(this.evaluationLabel = new EvaluationLabel(this.model));
            this.Controls.Add(this.bookmarkLabel = new BookmarkLabel(this.model));

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private class TitleLink : LinkLabel
        {
            private NarouAPI.NovelData model;

            public TitleLink(NarouAPI.NovelData model)
            {
                //  Model
                this.model = model;

                this.ActiveLinkColor = Color.FromArgb(255, 158, 30);
                this.AutoSize = true;
                this.Font = new Font("ＭＳ Ｐゴシック", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 128);
                this.LinkColor = Color.FromArgb(255, 128, 0);
                this.Location = new Point(3, 3);
                this.Margin = new Padding(3);
                this.Name = "TitleLink";
                this.Size = new Size(125, 21);
                this.TabStop = true;
                this.Text = "小説タイトル";
                this.VisitedLinkColor = Color.FromArgb(255, 128, 0);
            }
        }
        private class NovelInfo : Label
        {
            private NarouAPI.NovelData model;

            public NovelInfo(NarouAPI.NovelData model)
            {
                //  model
                this.model = model;

                this.AutoSize = true;
                this.BorderStyle = BorderStyle.FixedSingle;
                this.Location = new Point(3, 52);
                this.MaximumSize = new Size(115, 203);
                this.MinimumSize = new Size(115, 203);
                this.Name = "NovelPageNumber";
                this.Size = new Size(115, 203);
                this.Text = "連載中\r\n(全254部分)\r\n";
                this.TextAlign = ContentAlignment.MiddleCenter;
            }
        }
        private class WriteLabel : Label
        {
            private NarouAPI.NovelData model;

            public WriteLabel(NarouAPI.NovelData model)
            {
                //  model
                this.model = model;

                this.AutoSize = true;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F);
                this.Location = new Point(3, 30);
                this.Margin = new Padding(3);
                this.Name = "Writer";
                this.Size = new Size(48, 16);
                this.Text = "作者 :";
            }
        }
        private class WriteLink : LinkLabel
        {
            private NarouAPI.NovelData model;

            public WriteLink(NarouAPI.NovelData model)
            {
                //  model
                this.model = model;

                this.AutoSize = true;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F);
                this.Location = new Point(55, 30);
                this.Margin = new Padding(3);
                this.Name = "WriteLink";
                this.Size = new Size(56, 16);
                this.TabStop = true;
                this.Text = "作者名";
            }
        }
        private class SummryLabel : Label
        {
            private NarouAPI.NovelData model;

            public SummryLabel(NarouAPI.NovelData model)
            {
                //  model
                this.model = model;

                this.AutoSize = true;
                this.BorderStyle = BorderStyle.FixedSingle;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.ForeColor = Color.FromArgb(110, 64, 0);
                this.Location = new Point(121, 52);
                this.Margin = new Padding(3);
                this.MaximumSize = new Size(575, 50);
                this.MinimumSize = new Size(575, 50);
                this.Name = "Summary";
                this.Size = new Size(575, 50);
                this.Text = "あらすじ、あらすじ、あらすじ、あらすじ、あらすじ、あらすじ、あらすじ、あらすじ、あらすじ\r\n\r\n";
            }
        }
        private class GenreLabel : Label
        {
            private NarouAPI.NovelData model;

            public GenreLabel(NarouAPI.NovelData model)
            {
                //  model
                this.model = model;

                this.AutoSize = true;
                this.BorderStyle = BorderStyle.FixedSingle;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(121, 105);
                this.Margin = new Padding(0);
                this.Name = "Genre";
                this.Size = new Size(80, 18);
                this.Text = "ジャンル : ";
            }
        }
        private class GenreLink : LinkLabel
        {
            private NarouAPI.NovelData model;

            public GenreLink(NarouAPI.NovelData model)
            {
                //  model
                this.model = model;

                this.AutoSize = true;
                this.BorderStyle = BorderStyle.FixedSingle;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(204, 105);
                this.MaximumSize = new Size(492, 18);
                this.MinimumSize = new Size(492, 18);
                this.Name = "GenreLink";
                this.Size = new Size(492, 18);
                this.TabStop = true;
                this.Text = "ファンタジー";
            }
        }
        private class KeyWordLabel : Label
        {
            private NarouAPI.NovelData model;

            public KeyWordLabel(NarouAPI.NovelData model)
            {
                //  model
                this.model = model;

                this.AutoSize = true;
                this.BorderStyle = BorderStyle.FixedSingle;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(120, 126);
                this.Margin = new Padding(3, 3, 0, 3);
                this.Name = "Keyword";
                this.Size = new Size(94, 18);
                this.Text = "キーワード : ";
            }
        }
        private class TagsPanel : Panel
        {
            private NarouAPI.NovelData model;

            public TagsPanel(NarouAPI.NovelData model)
            {
                //  model
                this.model = model;

                this.Location = new Point(217, 126);
                this.Name = "panel2";
                this.Size = new Size(480, 44);
            }
        }
        private class LastupLabel : Label
        {
            private NarouAPI.NovelData model;

            public LastupLabel(NarouAPI.NovelData model)
            {
                //  model
                this.model = model;

                this.AutoSize = true;
                this.BorderStyle = BorderStyle.FixedSingle;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(120, 173);
                this.Margin = new Padding(3, 0, 0, 3);
                this.MaximumSize = new Size(287, 18);
                this.MinimumSize = new Size(287, 18);
                this.Name = "Lastup";
                this.Size = new Size(287, 18);
                this.Text = "最終更新日：2018/11/17 07:48";
            }
        }
        private class ReadTimeLabel : Label
        {
            private NarouAPI.NovelData model;

            public ReadTimeLabel(NarouAPI.NovelData model)
            {
                //  model
                this.model = model;

                this.AutoSize = true;
                this.BorderStyle = BorderStyle.FixedSingle;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(409, 173);
                this.Margin = new Padding(3, 0, 0, 3);
                this.MaximumSize = new Size(287, 18);
                this.MinimumSize = new Size(287, 18);
                this.Name = "ReadTime";
                this.Size = new Size(287, 18);
                this.Text = "読了時間：約282分（140,702文字）\r\n";
                this.TextAlign = ContentAlignment.MiddleRight;
            }
        }
        private class UniqueUserLabel : Label
        {
            private NarouAPI.NovelData model;

            public UniqueUserLabel(NarouAPI.NovelData model)
            {
                //  model
                this.model = model;

                this.AutoSize = true;
                this.BorderStyle = BorderStyle.FixedSingle;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(120, 194);
                this.Margin = new Padding(3, 0, 0, 3);
                this.MaximumSize = new Size(216, 18);
                this.MinimumSize = new Size(214, 18);
                this.Name = "UniqueUser";
                this.Size = new Size(215, 18);
                this.Text = "週別ユニークユーザ： 71000人";
            }
        }
        private class ReviewLabel : Label
        {
            private NarouAPI.NovelData model;

            public ReviewLabel(NarouAPI.NovelData model)
            {
                //  model
                this.model = model;

                this.AutoSize = true;
                this.BorderStyle = BorderStyle.FixedSingle;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.ImageAlign = ContentAlignment.MiddleLeft;
                this.Location = new Point(337, 194);
                this.Margin = new Padding(2, 0, 0, 3);
                this.MaximumSize = new Size(147, 18);
                this.MinimumSize = new Size(147, 18);
                this.Name = "Review";
                this.Size = new Size(147, 18);
                this.Text = "レビュー数： 1000件";
                this.TextAlign = ContentAlignment.MiddleLeft;
            }
        }
        private class PCUploadLabel : Label
        {
            private NarouAPI.NovelData model;

            public PCUploadLabel(NarouAPI.NovelData model)
            {
                //  model
                this.model = model;

                this.AutoSize = true;
                this.BorderStyle = BorderStyle.FixedSingle;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(486, 194);
                this.Margin = new Padding(2, 0, 0, 3);
                this.MaximumSize = new Size(63, 18);
                this.MinimumSize = new Size(63, 18);
                this.Name = "PCUpload";
                this.Size = new Size(63, 18);
                this.Text = "PC投稿";
                this.TextAlign = ContentAlignment.MiddleCenter;
            }
        }
        private class PhoneUploadLabel : Label
        {
            private NarouAPI.NovelData model;

            public PhoneUploadLabel(NarouAPI.NovelData model)
            {
                //  model
                this.model = model;

                this.AutoSize = true;
                this.BorderStyle = BorderStyle.FixedSingle;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(551, 194);
                this.Margin = new Padding(2, 0, 0, 3);
                this.MaximumSize = new Size(74, 18);
                this.MinimumSize = new Size(74, 18);
                this.Name = "PhoneUploadLabel";
                this.Size = new Size(74, 18);
                this.Text = "携帯投稿";
                this.TextAlign = ContentAlignment.MiddleCenter;
            }
        }
        private class IllustrationLabel : Label
        {
            private NarouAPI.NovelData model;

            public IllustrationLabel(NarouAPI.NovelData model)
            {
                //  model
                this.model = model;

                this.AutoSize = true;
                this.BorderStyle = BorderStyle.FixedSingle;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(627, 194);
                this.Margin = new Padding(2, 0, 0, 3);
                this.MaximumSize = new Size(69, 18);
                this.MinimumSize = new Size(69, 18);
                this.Name = "Illustration";
                this.Size = new Size(69, 18);
                this.Text = "挿絵あり\r\n";
                this.TextAlign = ContentAlignment.MiddleCenter;
            }
        }
        private class TotalPointLabel : Label
        {
            private NarouAPI.NovelData model;

            public TotalPointLabel(NarouAPI.NovelData model)
            {
                //  model
                this.model = model;

                this.AutoSize = true;
                this.BorderStyle = BorderStyle.FixedSingle;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.ForeColor = Color.Red;
                this.Location = new Point(120, 215);
                this.Margin = new Padding(3, 0, 0, 3);
                this.MaximumSize = new Size(576, 18);
                this.MinimumSize = new Size(576, 18);
                this.Name = "TotalPoint";
                this.Size = new Size(576, 18);
                this.Text = "総合評価ポイント： 274 pt\r\n";
            }
        }
        private class EvaluatorLabel : Label
        {
            private NarouAPI.NovelData model;

            public EvaluatorLabel(NarouAPI.NovelData model)
            {
                //  model
                this.model = model;

                this.AutoSize = true;
                this.BorderStyle = BorderStyle.FixedSingle;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.ForeColor = Color.Black;
                this.Location = new Point(120, 236);
                this.Margin = new Padding(3, 0, 0, 3);
                this.MaximumSize = new Size(189, 18);
                this.MinimumSize = new Size(189, 18);
                this.Name = "EvaluatorNumber";
                this.Size = new Size(189, 18);
                this.Text = "評価人数： 8,0000,0000人";
            }
        }
        private class EvaluationLabel : Label
        {
            private NarouAPI.NovelData model;

            public EvaluationLabel(NarouAPI.NovelData model)
            {
                //  model
                this.model = model;

                this.AutoSize = true;
                this.BorderStyle = BorderStyle.FixedSingle;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.ForeColor = Color.Black;
                this.Location = new Point(311, 236);
                this.Margin = new Padding(3, 0, 0, 3);
                this.MaximumSize = new Size(192, 18);
                this.MinimumSize = new Size(192, 18);
                this.Name = "EvaluationPoints";
                this.Size = new Size(192, 18);
                this.Text = "評価点： 6,0000,0000 pt";
            }
        }
        private class BookmarkLabel : Label
        {
            private NarouAPI.NovelData model;

            public BookmarkLabel(NarouAPI.NovelData model)
            {
                //  model
                this.model = model;

                this.AutoSize = true;
                this.BorderStyle = BorderStyle.FixedSingle;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.ForeColor = Color.Black;
                this.Location = new Point(505, 236);
                this.Margin = new Padding(3, 0, 0, 3);
                this.MaximumSize = new Size(191, 18);
                this.MinimumSize = new Size(191, 18);
                this.Name = "BookMarkPoint";
                this.Size = new Size(191, 18);
                this.Text = "ブックマーク： 1000,0000件 ";
            }
        }
    }
}
