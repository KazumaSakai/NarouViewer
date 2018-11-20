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
                ChangeModel();

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

        private SearchLabel searchLabel;
        private SearchTextBox searchTextBox;
        private ChoiceSearchWordButton choiceSearchWordButton;
        private ExclusionLabel exclusionLabel;
        private ExclusionTextBox exclusionTextBox;
        private ChoiceExclusionWordButton choiceExclusionWordButton;
        private SearchOptionLabel searchOptionLabel;
        private ChoiceGenreButton choiceGenreButton;
        private ChoiceDetailOption choiceDetailOption;
        private SearchButton searchButton;
        private SearchKeywordTabs searchKeywordTabs;

        public NarouSearchView(NarouAPI.GetParameter model)
        {
            this.DoubleBuffered = true;
            this.Location = new Point(3, 3);
            this.Name = "searchPanel";

            this.Controls.Add(this.searchLabel = new SearchLabel());
            this.Controls.Add(this.searchTextBox = new SearchTextBox());
            this.Controls.Add(this.choiceSearchWordButton = new ChoiceSearchWordButton());
            this.Controls.Add(this.exclusionLabel = new ExclusionLabel());
            this.Controls.Add(this.exclusionTextBox = new ExclusionTextBox());
            this.Controls.Add(this.choiceExclusionWordButton = new ChoiceExclusionWordButton());
            this.Controls.Add(this.searchOptionLabel = new SearchOptionLabel());
            this.Controls.Add(this.choiceGenreButton = new ChoiceGenreButton());
            this.Controls.Add(this.choiceDetailOption = new ChoiceDetailOption());
            this.Controls.Add(this.searchButton = new SearchButton());
            this.Controls.Add(this.listModel = new NovelDataListView(new List<NarouAPI.NovelData>()));
            this.Controls.Add(this.searchKeywordTabs = new SearchKeywordTabs(model));
            this.Size = new Size(706, 185 + listModel.Size.Height);

            this.searchButton.Click += new EventHandler((object sender, EventArgs e) => Search());
            this.choiceSearchWordButton.Click += new EventHandler((object sender, EventArgs e) => SearchKeywordButton_Push());

            this.model = model;
            this.Search();
        }

        private void ChangeModel()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)ChangeModel);
                return;
            }

            if (model == null) return;

            model.word = searchTextBox.Text;
            model.notWord = exclusionTextBox.Text;
        }

        public void Search()
        {
            if (listModel == null) return;
            this.ChangeModel();

            Task.Run(async () =>
            {
                this.listModel.model = await NarouAPI.Get(model);

                Invoke((Action)(() =>
                {
                    this.Size = new Size(706, 185 + listModel.Size.Height);
                }));
            });
        }

        private Timer animationTimer;
        private bool openSeachKeywordTabs = false;
        public void SearchKeywordButton_Push()
        {
            if (animationTimer != null)
            {
                animationTimer.Stop();
            }
            openSeachKeywordTabs = !openSeachKeywordTabs;

            animationTimer = Animator.Animate(10, 20, (frame, frequency) =>
            {
                if (!Visible || IsDisposed) return false;


                double value = (double)frame / (double)frequency;

                Size size = searchKeywordTabs.defaultSize;
                int height = (int)(size.Height * ((openSeachKeywordTabs ? value : 1.0d - value)));

                searchKeywordTabs.Size = new Size(size.Width, height);
                searchButton.Location = new Point(100, 122 + height);
                listModel.Location = new Point(3, 180 + height);

                return true;
            });
        }

        private class SearchLabel : Label
        {
            public SearchLabel()
            {
                this.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(3, 19);
                this.Name = "searchLabel";
                this.Size = new Size(50, 25);
                this.Text = "検索";
                this.TextAlign = ContentAlignment.MiddleCenter;

            }
        }
        private class SearchTextBox : TextBox
        {
            public SearchTextBox()
            {
                this.Cursor = Cursors.IBeam;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(56, 19);
                this.Name = "searchTextBox";
                this.Size = new Size(514, 25);

            }
        }
        private class ChoiceSearchWordButton : Button
        {
            public ChoiceSearchWordButton()
            {
                this.Location = new Point(573, 19);
                this.Name = "ChoiceSearchWordButton";
                this.Size = new Size(122, 25);
                this.Text = "+ 検索ワードを選択";
                this.UseVisualStyleBackColor = true;
            }
        }
        private class ExclusionLabel : Label
        {
            public ExclusionLabel()
            {
                this.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(3, 56);
                this.Name = "exclusionLabel";
                this.Size = new Size(50, 25);
                this.Text = "除外";
                this.TextAlign = ContentAlignment.MiddleCenter;
            }
        }
        private class ExclusionTextBox : TextBox
        {
            public ExclusionTextBox()
            {
                this.Cursor = Cursors.IBeam;
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(56, 56);
                this.Name = "ExclusionTextBox";
                this.Size = new Size(514, 25);
            }
        }
        private class ChoiceExclusionWordButton : Button
        {
            public ChoiceExclusionWordButton()
            {
                this.Location = new Point(573, 56);
                this.Name = "ChoiceExclusionWordButton";
                this.Size = new Size(122, 25);
                this.Text = "+ 除外ワードを選択";
                this.UseVisualStyleBackColor = true;
            }
        }
        private class SearchOptionLabel : Label
        {
            public SearchOptionLabel()
            {
                this.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(311, 89);
                this.Name = "searchOptionLabel";
                this.Size = new Size(117, 23);
                this.Text = "検索条件設定 ：";
                this.TextAlign = ContentAlignment.MiddleCenter;
            }
        }
        private class ChoiceGenreButton : Button
        {
            public ChoiceGenreButton()
            {
                this.Font = new Font("ＭＳ Ｐゴシック", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(431, 87);
                this.Name = "choiceGenreButton";
                this.Size = new Size(130, 26);
                this.Text = "+ ジャンル選択";
                this.UseVisualStyleBackColor = true;
            }
        }
        private class ChoiceDetailOption : Button
        {
            public ChoiceDetailOption()
            {
                this.Font = new Font("ＭＳ Ｐゴシック", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(567, 87);
                this.Name = "ChoiceDetailOption";
                this.Size = new Size(130, 26);
                this.Text = "+ 詳細条件設定";
                this.UseVisualStyleBackColor = true;
            }
        }
        private class SearchButton : Button
        {
            public SearchButton()
            {
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Bold, GraphicsUnit.Point, 128);
                this.Location = new Point(100, 122);
                this.Name = "searchButton";
                this.Size = new Size(500, 33);
                this.Text = "検索";
                this.UseVisualStyleBackColor = true;
            }
        }

        private class SearchKeywordTabs : TabControl
        {
            public Size defaultSize;

            private OfficialKeywordTabPage officialKeywordTabPage;
            private RecommendKeywordTabPage recommendKeywordTabPage;
            private ReplayKeywordTabPage replayKeywordTabPage;

            private NarouAPI.GetParameter _model;
            public NarouAPI.GetParameter model
            {
                set
                {
                    _model = value;
                }
                get
                {
                    return _model;
                }
            }

            public SearchKeywordTabs(NarouAPI.GetParameter model)
            {
                this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
                this.Location = new Point(12, 120);
                this.Name = "searchKeywordTabs";
                this.SelectedIndex = 0;
                this.Size = new Size(690, 0);
                this.defaultSize = new Size(690, 524);

                this.Controls.Add(this.officialKeywordTabPage = new OfficialKeywordTabPage());
                this.Controls.Add(this.recommendKeywordTabPage = new RecommendKeywordTabPage());
                this.Controls.Add(this.replayKeywordTabPage = new ReplayKeywordTabPage());

                //  Model
                this.model = model;
            }

            private class OfficialKeywordTabPage : TabPage
            {
                private Label title;
                private Label description;
                private KeywordsTable keywordsTable;

                private static string[][] officialKeywords = new string[][]
                {
                    new string[]
                    {
                        "作品傾向","ギャグ","シリアス","ほのぼの","ダーク"
                    },
                    new string[]
                    {
                        "登場キャラクター","男主人公", "女主人公", "人外",
                        "魔王", "勇者"
                    },
                    new string[]
                    {
                        "舞台","和風","西洋","中華","学園"
                    },
                    new string[]
                    {
                        "時代設定","戦国","幕末","明治/大正","昭和","平成",
                        "古代","中世","近世","近代","現代","未来"
                    },
                    new string[]
                    {
                        "要素","ロボット", "アンドロイド","職業もの","ハーレム",
                        "逆ハーレム", "偶像劇", "チート", "内政", "魔法", "冒険",
                        "ミリタリー", "日常", "ハッピーエンド", "バッドエンド", "グルメ",
                        "青春", "ゲーム", "超能力", "タイムトラベル", "ダンジョン",
                        "パラレルワールド","タイムリープ"
                    }
                };

                public OfficialKeywordTabPage()
                {
                    this.DoubleBuffered = true;
                    this.Location = new Point(4, 26);
                    this.Name = "officialKeywordTabPage";
                    this.Size = new Size(682, 494);
                    this.TabIndex = 0;
                    this.Text = "公式キーワード";
                    this.UseVisualStyleBackColor = true;

                    this.Controls.Add(this.title = new DefaultLabel("公式キーワード", "title", new Point(11, 6)));
                    this.Controls.Add(this.description = new DefaultLabel("ワードをチェックすると、検索ボックスに自動で入力されます。検索ワードは直接入力も可能です。", "description", new Point(11, 6)));
                    this.Controls.Add(this.keywordsTable = new KeywordsTable(officialKeywords));             
                }
            }
            private class RecommendKeywordTabPage : TabPage
            {
                public RecommendKeywordTabPage()
                {
                    this.DoubleBuffered = true;
                    this.Location = new Point(4, 26);
                    this.Name = "recommendKeywordTabPage";
                    this.Size = new Size(682, 494);
                    this.Text = "おすすめキーワード";
                    this.UseVisualStyleBackColor = true;
                }
            }
            private class ReplayKeywordTabPage : TabPage
            {
                public ReplayKeywordTabPage()
                {
                    this.DoubleBuffered = true;
                    this.Location = new Point(4, 26);
                    this.Name = "replayKeywordTab";
                    this.Size = new Size(682, 494);
                    this.Text = "リプレイ用キーワード";
                    this.UseVisualStyleBackColor = true;
                }
            }

            private class KeywordsTable : TableLayoutPanel
            {
                public KeywordsTable(string[][] words)
                {
                    this.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                    this.ColumnCount = 2;
                    this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.02985F));
                    this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75.97015F));

                    this.DoubleBuffered = true;
                    this.Location = new Point(8, 40);
                    this.Name = "tableLayoutPanel";
                    this.RowCount = words.Length;
                    for (int i = 0; i < words.Length; i++)
                    {
                        string[] lineWords = words[i];

                        this.RowStyles.Add(new RowStyle(SizeType.Absolute, 6 + (22 * lineWords.Length - 1)));
                        this.Controls.Add(new DefaultLabel(lineWords[0], lineWords[0], new Point(3, 3)), 0, i);
                        this.Controls.Add(new WordCheckBoxsPanel(lineWords), 1, i);
                    }
                    this.Size = new Size(676, 439);
                }

                private class WordCheckBoxsPanel : Panel
                {
                    WordCheckBox[] wordCheckBox;
                    public WordCheckBoxsPanel(string[] words)
                    {
                        wordCheckBox = new WordCheckBox[words.Length];
                        for (int i = 1; i < words.Length; i++)
                        {
                            this.Controls.Add(this.wordCheckBox[i] = new WordCheckBox(words[i], i));
                        }

                        this.Dock = DockStyle.Fill;
                        this.Name = "CheckBoxsPanel";
                        this.Size = new Size(507, 6 + (22 * words.Length - 1));
                    }

                    private class WordCheckBox : CheckBox
                    {
                        public WordCheckBox(string word, int index)
                        {
                            int x = index % 3;
                            int y = (index - x) / 3;

                            this.Location = new Point(3 + (168 * x), 3 + (22 * y));
                            this.Size = new Size(165, 22);
                            this.Text = word;
                            this.Name = "WordCheckBox " + word;
                            this.UseVisualStyleBackColor = true;
                        }
                    }
                }
            }
        }
    }
}
