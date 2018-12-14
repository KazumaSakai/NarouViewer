using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using NarouViewer.API;

namespace NarouViewer
{
    public class NarouSearchView : Panel
    {
        private NarouAPI.GetParameter _model;
        public NarouAPI.GetParameter model
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

        private NovelDataListView _listModel;
        public NovelDataListView listModel
        {
            set
            {
                _listModel = value;
            }
            get
            {
                return _listModel;
            }
        }

        private Label searchLabel;
        private Label eSearchLabel;

        private SearchTextBox searchTextBox;
        private SearchTextBox eSearchTextBox;

        private Button searchWordButton;
        private Button eSearchWordButton;

        private Label searchOptLabel;

        private Button genreButton;
        private Button detailOptButton;
        private Button searchButton;

        private SearchKeywordTabs keywordTabs;
        private SearchKeywordTabs eKeywordTabs;
        private GenrePanel genrePanel;

        public NarouSearchView(NarouAPI.GetParameter model)
        {
            this.DoubleBuffered = true;
            this.Location = new Point(3, 3);
            this.Name = "searchPanel";

            //  Search Line
            this.Controls.Add(this.searchLabel = new DefaultLabel("検索", "searchLabel", Point.Empty, false));
            this.Controls.Add(this.searchTextBox = new SearchTextBox(model));
            this.Controls.Add(this.searchWordButton = new Button() { Size = new Size(122, 25), Text = "+ 検索ワードを選択" });
            this.searchWordButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                genrePanel.Close();
                eKeywordTabs.Close();
                keywordTabs.AnimationOpen();
            });

            //  Exclusion Line
            this.Controls.Add(this.eSearchLabel = new DefaultLabel("除外", "exclusionLabel", Point.Empty, false));
            this.Controls.Add(this.eSearchTextBox = new SearchTextBox(model));
            this.Controls.Add(this.eSearchWordButton = new Button() { Size = new Size(122, 25), Text = "+ 除外ワードを選択" });
            this.eSearchWordButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                genrePanel.Close();
                keywordTabs.Close();
                eKeywordTabs.AnimationOpen();
            });

            //  SearchOption Line
            this.Controls.Add(this.searchOptLabel = new DefaultLabel("検索条件設定 ：", "searchOptionLabel", Point.Empty, false));
            this.Controls.Add(this.genreButton = new Button() { Size = new Size(130, 26), Text = "+ ジャンル選択" });
            this.genreButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                eKeywordTabs.Close();
                keywordTabs.Close();
                genrePanel.AnimationOpen();
            });
            this.Controls.Add(this.detailOptButton = new Button() { Size = new Size(130, 26), Text = "+ 詳細条件設定"});

            //  Keyword Line
            this.Controls.Add(this.keywordTabs = new SearchKeywordTabs(model));
            this.keywordTabs.SizeChanged += new EventHandler((object sender, EventArgs e) => UpdateSize());
            this.Controls.Add(this.eKeywordTabs = new SearchKeywordTabs(model));
            this.eKeywordTabs.SizeChanged += new EventHandler((object sender, EventArgs e) => UpdateSize());
            this.Controls.Add(this.genrePanel = new GenrePanel(model));
            this.genrePanel.SizeChanged += new EventHandler((object sender, EventArgs e) => UpdateSize());

            //  SearchButton Line
            this.Controls.Add(this.searchButton = new Button() { Size = new Size(500, 33) , Text = "検索", Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Bold, GraphicsUnit.Point, 128) });
            this.searchButton.Click += new EventHandler((object sender, EventArgs e) => Search());

            //  DataList View
            this.Controls.Add(this.listModel = new NovelDataListView(new List<NarouAPI.NovelData>()));

            this.ParentChanged += new EventHandler(ChangeParent);
            this.parentSizeChanged += new EventHandler(ParentSizeChanged);

            this.model = model;
            this.Search();
            this.UpdateSize();
        }

        private void OnModelChanged()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)OnModelChanged);
                return;
            }

            if (model == null) return;

            model.notWord = eSearchTextBox.Text;

            StringBuilder sb = new StringBuilder();
            foreach (string keyword in keywordTabs.searchKeywordList)
            {
                sb.Append(" ");
                sb.Append(keyword);
            }
            model.word += sb.ToString();
        }

        private EventHandler parentSizeChanged;
        private Control parent;
        private void ChangeParent(object sender, EventArgs e)
        {
            if (parent != null) parent.SizeChanged -= parentSizeChanged;
            this.Parent.SizeChanged += parentSizeChanged;

            this.parent = Parent;
        }
        private void ParentSizeChanged(object sender, EventArgs e)
        {
            int space_width = (this.Parent.Width - 35) - this.Width;

            this.SuspendLayout();
            this.Location = new Point(space_width / 2, 3);
            this.ResumeLayout();
        }

        public void Search()
        {
            if (listModel == null) return;
            this.OnModelChanged();

            Task.Run(async () =>
            {
                this.listModel.model = await NarouAPI.Get(model);

                Invoke((Action)(() =>
                {
                    this.UpdateSize();
                }));
            });
        }
        public void UpdateSize()
        {
            this.SuspendLayout();

            int nowY = 10;

            //  Search Line
            this.searchLabel.Location = new Point(3, nowY + 4);
            this.searchTextBox.Location = new Point(56, nowY);
            this.searchWordButton.Location = new Point(573, nowY - 1);
            nowY += this.searchTextBox.Height + 3;

            //  Exclusion Line
            this.eSearchLabel.Location = new Point(3, nowY + 4);
            this.eSearchTextBox.Location = new Point(56, nowY);
            this.eSearchWordButton.Location = new Point(573, nowY - 1);
            nowY += this.eSearchTextBox.Height + 10;

            //  SearchOption Line
            this.searchOptLabel.Location = new Point(311, nowY + 5);
            this.genreButton.Location = new Point(431, nowY);
            this.detailOptButton.Location = new Point(567, nowY);
            nowY += this.detailOptButton.Height + 3;

            //  Keyword Line
            this.keywordTabs.Location = new Point(12, nowY);
            this.eKeywordTabs.Location = new Point(12, nowY);
            this.genrePanel.Location = new Point(12, nowY);
            nowY += this.keywordTabs.Height + this.eKeywordTabs.Height + this.genrePanel.Height + 3;

            //  SearchButton Line
            this.searchButton.Location = new Point(100, nowY);
            nowY += this.searchButton.Height + 3;

            //  DataList Line
            this.listModel.Location = new Point(3, nowY);
            nowY += this.listModel.Height + 3;

            this.Size = new Size(706, nowY);
            this.ResumeLayout();
        }
    }
}