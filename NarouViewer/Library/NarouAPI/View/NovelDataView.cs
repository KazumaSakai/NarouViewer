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
        #region --- Model ---
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
        #endregion

        #region --- Controller ---
        private NovelDataController controller;
        #endregion

        #region --- Child Control ---
        private Label writerLabel;
        private Label genreLabel;
        private Label keywordLabel;

        private SummryLabel summaryLabel;
        private EvaluatorLabel evaluatorLabel;
        private NovelInfoLabel novelInfoLabel;
        private TotalPointLabel totalPointLabel;
        private UniqueUserLabel uniqueuserLabel;
        private LastUpdateLabel lastupLabel;
        private ReadTimeLabel readTimeLabel;
        private BookmarkLabel bookmarkLabel;
        private EvaluationLabel evaluationLabel;
        private PCUploadLabel pcUploadPanel;
        private PhoneUploadLabel phoneUploadLabel;
        private IllustrationLabel illustrationLabel;
        private ReviewLabel reviewLabel;

        public WriterLinkLabel writerLinkLabel;
        public TitleLinkLabel titleLinkLabel;
        public TagsPanel keywordsPanel;
        public GenreLinkLabel genreLinkLabel;

        private List<INovelData> iNovelDatas = new List<INovelData>();
        #endregion

        public NovelDataView(NovelData model, NovelDataController controller)
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Name = "NovelDataVC";

            this.SuspendLayout();

            this.Controls.Add(this.writerLabel = new DefaultLabel("作者 :", "Writer", new Point(3, 30)));
            this.Controls.Add(this.genreLabel = new DefaultLabel("ジャンル : ", "Genre", new Point(121, 105)));
            this.Controls.Add(this.keywordLabel = new DefaultLabel("キーワード :", "Keyword", new Point(120, 126)));

            this.Controls.Add(this.titleLinkLabel = new TitleLinkLabel(model));
            this.Controls.Add(this.novelInfoLabel = new NovelInfoLabel(model));
            this.Controls.Add(this.writerLinkLabel = new WriterLinkLabel(model));
            this.Controls.Add(this.summaryLabel = new SummryLabel(model));
            this.Controls.Add(this.genreLinkLabel = new GenreLinkLabel(model));
            this.Controls.Add(this.keywordsPanel = new TagsPanel(model));
            this.Controls.Add(this.lastupLabel = new LastUpdateLabel(model));
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

            this.titleLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler((object sender, LinkLabelLinkClickedEventArgs e) =>
            {
                this.controller?.TitleLinkClicked?.Invoke(this.model?.ncode??"");
            });
            this.writerLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler((object sender, LinkLabelLinkClickedEventArgs e) =>
            {
                this.controller?.WriterLinkClicked?.Invoke((this.model?.userid??0).ToString());
            });
            this.keywordsPanel.onClickedTags += new StringEventHandler((string str)=>
            {
                this.controller?.TagClicked?.Invoke(str);
            });
            this.genreLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler((object sender, LinkLabelLinkClickedEventArgs e) =>
            {
                this.controller?.GenreLinkClicked?.Invoke(this.model?.genre??0);
            });

            this.iNovelDatas.Add(titleLinkLabel);
            this.iNovelDatas.Add(novelInfoLabel);
            this.iNovelDatas.Add(writerLinkLabel);
            this.iNovelDatas.Add(summaryLabel);
            this.iNovelDatas.Add(genreLinkLabel);
            this.iNovelDatas.Add(keywordsPanel);
            this.iNovelDatas.Add(lastupLabel);
            this.iNovelDatas.Add(readTimeLabel);
            this.iNovelDatas.Add(uniqueuserLabel);
            this.iNovelDatas.Add(reviewLabel);
            this.iNovelDatas.Add(pcUploadPanel);
            this.iNovelDatas.Add(phoneUploadLabel);
            this.iNovelDatas.Add(illustrationLabel);
            this.iNovelDatas.Add(totalPointLabel);
            this.iNovelDatas.Add(evaluatorLabel);
            this.iNovelDatas.Add(evaluationLabel);
            this.iNovelDatas.Add(bookmarkLabel);

            this.ResumeLayout(false);
            this.PerformLayout();

            //  Model
            this.model = model;

            // Controller
            this.controller = controller;
        }

        private void OnModelChanged()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)OnModelChanged);
                return;
            }

            if (model == null) return;

            foreach (INovelData item in iNovelDatas)
            {
                item.model = this.model;
            }

            //  Title Line
            int nowY = 3;
            titleLinkLabel.Location = new Point(3, nowY);
            nowY += titleLinkLabel.Size.Height + 3;

            //  Writer Line
            writerLabel.Location = new Point(3, nowY);
            writerLinkLabel.Location = new Point(55, nowY);
            nowY += writerLinkLabel.Size.Height + 3;

            //  NovelInfo Line
            novelInfoLabel.Location = new Point(3, nowY);

            //  Summary Line
            summaryLabel.Location = new Point(121, nowY);
            nowY += summaryLabel.Size.Height + 3;

            //  Genre Line
            genreLabel.Location = new Point(121, nowY);
            genreLinkLabel.Location = new Point(204, nowY);
            nowY += genreLinkLabel.Size.Height + 3;

            //  Keyword Line
            keywordLabel.Location = new Point(121, nowY);
            keywordsPanel.Location = new Point(217, nowY);
            nowY += keywordsPanel.Size.Height + 3;

            //  Lastup & ReadTime Line
            lastupLabel.Location = new Point(121, nowY);
            readTimeLabel.Location = new Point(409, nowY);
            nowY += lastupLabel.Size.Height + 3;

            //  UniqueUser Line
            uniqueuserLabel.Location = new Point(121, nowY);
            reviewLabel.Location = new Point(337, nowY);
            pcUploadPanel.Location = new Point(486, nowY);
            phoneUploadLabel.Location = new Point(551, nowY);
            illustrationLabel.Location = new Point(627, nowY);
            nowY += uniqueuserLabel.Size.Height + 3;

            //  TotalPointLabel Line
            totalPointLabel.Location = new Point(121, nowY);
            nowY += totalPointLabel.Size.Height + 3;

            //  Evaluator Line
            evaluatorLabel.Location = new Point(121, nowY);
            evaluationLabel.Location = new Point(311, nowY);
            bookmarkLabel.Location = new Point(505, nowY);
            nowY += evaluatorLabel.Size.Height + 3;

            this.Size = new Size(700, nowY + 3);
        }
    }
}
