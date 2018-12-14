using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using NarouViewer.API;

namespace NarouViewer
{
    /// <summary>
    /// 検索パラメータを設定するパネル
    /// </summary>
    public class SearchParameterView : Panel, IUpdateView
    {
        #region --- Model ---
        private NarouAPI.SearchParameter _model;
        public NarouAPI.SearchParameter model
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
        public NarouSearchController controller;
        #endregion

        #region --- 子コントロール ---
        //  Search Line
        private Label searchLabel;
        private SearchTextBox searchTextBox;
        private Button searchWordButton;

        //  Exclusion Search Line
        private Label eSearchLabel;
        private SearchTextBox eSearchTextBox;
        private Button eSearchWordButton;

        //  SearchOption Line
        private Label searchOptLabel;
        private Button genreButton;
        private Button detailOptButton;

        //  Keyword Line
        private SearchKeywordTabControl keywordTabs;
        private SearchKeywordTabControl eKeywordTabs;
        private CheckBoxsTablesPanel genrePanel;

        //  SearchButton Line
        private Button searchButton;
        #endregion

        #region --- Data ---
        private readonly (string[][] word, string title, int line)[] genreData = new (string[][], string, int)[]
        {
            (new string[][]
            {
                new string[]
                {
                    "恋愛", "異世界", "現実世界"
                },
                new string[]
                {
                    "ファンタジー", "ハイファンタジー", "ローファンタジー"
                },
                new string[]
                {
                    "文芸", "純文学", "ヒューマンドラマ", "歴史",
                    "推理", "ホラー", "アクション", "コメディー"
                },
                new string[]
                {
                    "SF", "VRゲーム", "宇宙", "空想科学", "パニック"
                },
                new string[]
                {
                    "その他", "童話", "詩", "エッセイ", "リプレイ", "その他"
                },
                new string[]
                {
                    "ノンジャンル", "ノンジャンル"
                }
            }, "ジャンル選択", 3)
        };
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="model">モデル</param>
        /// <param name="controller">コントローラー</param>
        public SearchParameterView(NarouAPI.SearchParameter model, NarouSearchController controller = null)
        {
            //  Search Line
            this.Controls.Add(this.searchLabel = new DefaultLabel("検索", "searchLabel", Point.Empty, false));
            this.Controls.Add(this.searchTextBox = new SearchTextBox(model));
            this.Controls.Add(this.searchWordButton = new Button() { Size = new Size(122, 25), Text = "+ 検索ワードを選択" });
            this.searchWordButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                OpenSearchOption(0);
            });

            //  Exclusion Search Line
            this.Controls.Add(this.eSearchLabel = new DefaultLabel("除外", "exclusionLabel", Point.Empty, false));
            this.Controls.Add(this.eSearchTextBox = new SearchTextBox(model));
            this.Controls.Add(this.eSearchWordButton = new Button() { Size = new Size(122, 25), Text = "+ 除外ワードを選択" });
            this.eSearchWordButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                OpenSearchOption(1);
            });

            //  SearchOption Line
            this.Controls.Add(this.searchOptLabel = new DefaultLabel("検索条件設定 ：", "searchOptionLabel", Point.Empty, false));
            this.Controls.Add(this.genreButton = new Button() { Size = new Size(130, 26), Text = "+ ジャンル選択" });
            this.genreButton.Click += new EventHandler((object sender, EventArgs e) =>
            {
                OpenSearchOption(2);
            });
            this.Controls.Add(this.detailOptButton = new Button() { Size = new Size(130, 26), Text = "+ 詳細条件設定"});

            //  Keyword Line
            this.Controls.Add(this.keywordTabs = new SearchKeywordTabControl(model, new StringEventHandler(SearchKeyword)));
            this.Controls.Add(this.eKeywordTabs = new SearchKeywordTabControl(model, new StringEventHandler(ESearchKeyword)));
            this.Controls.Add(this.genrePanel = new CheckBoxsTablesPanel(genreData, new StringEventHandler(GenreKeyword)));
            this.searchOptionControlList.Add(this.keywordTabs);
            this.searchOptionControlList.Add(this.eKeywordTabs);
            this.searchOptionControlList.Add(this.genrePanel);

            //  SearchButton Line
            this.Controls.Add(this.searchButton = new Button() { Size = new Size(500, 33) , Text = "検索", Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Bold, GraphicsUnit.Point, 128) });
            this.searchButton.Click += new EventHandler(OnSearchButtonClicked);

            //  Parent
            this.ParentChanged += new EventHandler(OnParentChanged);
            this._OnParentSizeChanged += new EventHandler(OnParentSizeChanged);

            //  Model
            this.model = model;

            //  Controller
            this.controller = controller ?? new NarouSearchController();

            //  Init
            this.DoubleBuffered = true;
            this.Name = "searchPanel";
            this.UpdateView();
        }

        /// <summary>
        /// モデルが変更された時のメソッド
        /// </summary>
        private void OnModelChanged()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)OnModelChanged);
                return;
            }
            if (model == null) return;

            model.notWord = eSearchTextBox.Text;
        }

        #region --- Parent用フィールド ---
        private EventHandler _OnParentSizeChanged;
        private Control parent;
        #endregion
        /// <summary>
        /// 親を変更した時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnParentChanged(object sender, EventArgs e)
        {
            if (parent != null) parent.SizeChanged -= _OnParentSizeChanged;
            this.Parent.SizeChanged += _OnParentSizeChanged;

            this.parent = Parent;
        }
        /// <summary>
        /// 親のサイズが変更された時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnParentSizeChanged(object sender, EventArgs e)
        {
            int space_width = (this.Parent.Width - 35) - this.Width;

            this.SuspendLayout();
            this.Location = new Point(space_width / 2, 3);
            this.ResumeLayout();
        }

        /// <summary>
        /// 検索ボタンをクリックしたイベント
        /// </summary>
        public void OnSearchButtonClicked(object sender, EventArgs e)
        {
            this.OnModelChanged();
            controller.Search?.Invoke(this.model);
        }

        /// <summary>
        /// Viewを更新する
        /// </summary>
        public void UpdateView()
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

            this.Size = new Size(706, nowY);
            this.ResumeLayout();
        }

        #region --- SearchOption Drop ---
        private int searchOption_Index;
        private List<IAnimationOpen> searchOptionControlList = new List<IAnimationOpen>();
        #endregion
        private void OpenSearchOption(int index)
        {
            if(searchOption_Index == index)
            {
                searchOption_Index = -1;
                searchOptionControlList[index].AnimationOpen(true, false);
            }
            if(searchOption_Index != -1)
            {
                searchOptionControlList[searchOption_Index].AnimationOpen(false, false);
            }

            searchOptionControlList[index].AnimationOpen(true, true);
            searchOption_Index = index;
        }

        private void SearchKeyword(string word)
        {
            if (model.searchKeywordList.Contains(word))
            {
                model.searchKeywordList.Remove(word);
            }
            else
            {
                model.searchKeywordList.Add(word);
            }
        }
        private void ESearchKeyword(string word)
        {
            if (model.eSearchKeywordList.Contains(word))
            {
                model.eSearchKeywordList.Remove(word);
            }
            else
            {
                model.eSearchKeywordList.Add(word);
            }
        }
        private void GenreKeyword(string word)
        {
            NarouAPI.SearchParameter.Genre genre = NarouAPI.SearchParameter.genreString2Enum[word];
            if(model.genre.HasFlag(genre))
            {
                model.genre &= ~genre;
            }
            else
            {
                model.genre |= genre;
            }
        }
    }
}