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

            this.searchKeywordTabs.SizeChanged += new EventHandler((object sender, EventArgs e) => AnimationChangeSize());
            this.searchButton.Click += new EventHandler((object sender, EventArgs e) => Search());
            this.choiceSearchWordButton.Click += new EventHandler((object sender, EventArgs e) => searchKeywordTabs.Open());

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

        public void AnimationChangeSize()
        {
            Size size = searchKeywordTabs.Size;

            searchKeywordTabs.Size = new Size(size.Width, size.Height);
            searchButton.Location = new Point(100, 122 + size.Height);
            listModel.Location = new Point(3, 180 + size.Height);
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
            private Size defaultSize;

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

                this.Controls.Add(this.officialKeywordTabPage = new OfficialKeywordTabPage());
                this.Controls.Add(this.recommendKeywordTabPage = new RecommendKeywordTabPage());
                this.Controls.Add(this.replayKeywordTabPage = new ReplayKeywordTabPage());

                Size size = officialKeywordTabPage.defaultSize;
                defaultSize = new Size(690, 30 + size.Height);
                this.SelectedIndexChanged += new EventHandler((object sender, EventArgs e) =>
                {
                    Size s = Size.Empty;
                    switch (SelectedIndex)
                    {
                        case 0:
                            s = officialKeywordTabPage.defaultSize;
                            break;

                        case 1:
                            s = recommendKeywordTabPage.defaultSize;
                            break;

                        case 2:
                            break;
                    }
                    AnimationChangeSize(new Size(690, 30 + s.Height));
                });

                //  Model
                this.model = model;
            }

            private Timer animationTimer;
            private bool isOpen = false;
            private int frame = 20;
            public void Open()
            {
                isOpen = !isOpen;
                if (animationTimer != null)
                {
                    animationTimer.Stop();
                }

                int startFrame = 20 - this.frame;
                animationTimer = Animator.Animate(10, this.frame, (frame, frequency) =>
                {
                    if (!Visible || IsDisposed) return false;
                    this.frame = startFrame + frame;

                    double value = (double)this.frame / (double)frequency;

                    int height = (int)(defaultSize.Height * ((isOpen ? value : 1.0d - value)));
                    this.Size = new Size(defaultSize.Width, height);

                    return true;
                });
            }

            private Timer timer2;
            public void AnimationChangeSize(Size newSize)
            {
                if (timer2 != null)
                {
                    timer2.Stop();
                }

                Size oldSize = defaultSize;
                int needFrame = Math.Abs(newSize.Height - oldSize.Height) / 30;
                timer2 = Animator.Animate(10, Math.Max(1, needFrame), (frame, frequency) =>
                {
                    if (!Visible || IsDisposed) return false;

                    double value = (double)frame / (double)frequency;
                    
                    this.defaultSize = new Size(oldSize.Width + (int)((newSize.Width - oldSize.Width) * value), oldSize.Height + (int)((newSize.Height - oldSize.Height) * value));
                    this.Size = this.defaultSize;
                    return true;
                });
            }

            private class OfficialKeywordTabPage : TabPage
            {
                private Label title;
                private KeywordsTable keywordsTable;
                public Size defaultSize;

                private static readonly string[][] officialKeywords = new string[][]
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
                    this.TabIndex = 0;
                    this.Text = "公式キーワード";
                    this.UseVisualStyleBackColor = true;

                    this.Controls.Add(this.title = new DefaultLabel("公式キーワード", "title", new Point(11, 15)));
                    this.Controls.Add(this.keywordsTable = new KeywordsTable(officialKeywords) { Location = new Point(8, title.Location.Y + title.Height + 3)});

                    this.defaultSize = new Size(682, keywordsTable.Location.Y + keywordsTable.Height + 8);
                }
            }
            private class RecommendKeywordTabPage : TabPage
            {
                private Label loveTitle;
                private Label fantasyTitle;
                private Label literatureTitle;
                private Label sfTitle;
                private KeywordsTable loveTable;
                private KeywordsTable fantasyTable;
                private KeywordsTable literatureTable;
                private KeywordsTable sfTable;
                public Size defaultSize;

                private static readonly string[][] loveKeywords = new string[][]
                {
                    new string[]
                    {
                        "恋愛〔大ジャンル〕", "異類婚姻譚", "身分差", "年の差", "悲恋"
                    },
                    new string[]
                    {
                        "異世界", "ヒストリカル", "乙女ゲーム", "悪役令嬢"
                    },
                    new string[]
                    {
                        "現実世界", "オフィスラブ", "スクールラブ", "古典恋愛"
                    }
                };
                private static readonly string[][] fantasyKeywords = new string[][]
                {
                    new string[]
                    {
                        "ハイファンタジー", "オリジナル戦記"
                    },
                    new string[]
                    {
                        "ローファンタジー", "伝奇"
                    }
                };
                private static readonly string[][] literatureKeywords = new string[][]
                {
                    new string[]
                    {
                        "ヒューマンドラマ", "日常", "青春", "ハードボイルド", "私小説",
                        "ホームドラマ"
                    },
                    new string[]
                    {
                        "歴史", "IF戦記", "史実", "時代小説", "逆光転生"
                    },
                    new string[]
                    {
                        "推理", "ミステリー", "サスペンス", "探偵小説"
                    },
                    new string[]
                    {
                        "ホラー", "スプラッタ", "怪談", "サイコホラー"
                    },
                    new string[]
                    {
                        "アクション", "異能力バトル", "ヒーロー", "スパイ", "冒険"
                    },
                    new string[]
                    {
                        "コメディー", "ラブコメ"
                    }
                };
                private static readonly string[][] sfKeywords = new string[][]
                {
                    new string[]
                    {
                        "SF〔大ジャンル〕", "近未来", "人工知能", "電脳世界"
                    },
                    new string[]
                    {
                        "VRゲーム", "VRMMO"
                    },
                    new string[]
                    {
                        "宇宙", "スペースオペラ", "エイリアン"
                    },
                    new string[]
                    {
                        "空想科学", "サイバーパンク", "スチームパンク", "ディストピア",
                        "タイムマシン"
                    },
                    new string[]
                    {
                        "パニック", "怪獣", "天災", "バイオハザード", "パンデミック"
                    }
                };

                public RecommendKeywordTabPage()
                {
                    this.DoubleBuffered = true;
                    this.Location = new Point(4, 26);
                    this.Name = "recommendKeywordTabPage";
                    this.Text = "おすすめキーワード";
                    this.UseVisualStyleBackColor = true;

                    this.Controls.Add(this.loveTitle = new DefaultLabel("恋愛", "love", new Point(11, 15)));
                    this.Controls.Add(this.loveTable = new KeywordsTable(loveKeywords) { Location = new Point(8, loveTitle.Location.Y + loveTitle.Height + 3) });

                    this.Controls.Add(this.fantasyTitle = new DefaultLabel("ファンタジー", "fantasy", new Point(11, 150)) { Location = new Point(8, loveTable.Location.Y + loveTable.Height + 15) });
                    this.Controls.Add(this.fantasyTable = new KeywordsTable(fantasyKeywords) { Location = new Point(8, fantasyTitle.Location.Y + fantasyTitle.Height + 3) });

                    this.Controls.Add(this.literatureTitle = new DefaultLabel("文芸", "literature", new Point(11, 240)) { Location = new Point(8, fantasyTable.Location.Y + fantasyTable.Height + 15) });
                    this.Controls.Add(this.literatureTable = new KeywordsTable(literatureKeywords) { Location = new Point(8, literatureTitle.Location.Y + literatureTitle.Height + 3) });

                    this.Controls.Add(this.sfTitle = new DefaultLabel("SF", "sf", new Point(11, 520)) { Location = new Point(8, literatureTable.Location.Y + literatureTable.Height + 15) });
                    this.Controls.Add(this.sfTable = new KeywordsTable(sfKeywords) { Location = new Point(8, sfTitle.Location.Y + sfTitle.Height + 3) });

                    this.defaultSize = new Size(682, sfTable.Location.Y + sfTable.Height + 8);
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
                    this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));
                    this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 76F));

                    this.DoubleBuffered = true;
                    this.Location = new Point(8, 26);
                    this.Name = "tableLayoutPanel";
                    this.RowCount = words.Length;

                    int totalHeight = 0;
                    for (int i = 0; i < words.Length; i++)
                    {
                        string[] lineWords = words[i];

                        int height = 8 + (22 * (int)Math.Ceiling((lineWords.Length - 1) / 3d));
                        totalHeight += (height + 1);

                        this.RowStyles.Add(new RowStyle(SizeType.Absolute, height));
                        Label label = new DefaultLabel(lineWords[0], lineWords[0], new Point(3, 3));
                        label.Padding = new Padding(3);
                        this.Controls.Add(label, 0, i);
                        this.Controls.Add(new WordCheckBoxsPanel(lineWords), 1, i);
                    }
                    this.Size = new Size(663, 1 + totalHeight);
                }

                private class WordCheckBoxsPanel : Panel
                {
                    WordCheckBox[] wordCheckBox;
                    public WordCheckBoxsPanel(string[] words)
                    {
                        wordCheckBox = new WordCheckBox[words.Length];
                        for (int i = 1; i < words.Length; i++)
                        {
                            this.Controls.Add(this.wordCheckBox[i] = new WordCheckBox(words[i], i - 1));
                        }

                        this.Dock = DockStyle.Fill;
                        this.Name = "CheckBoxsPanel";
                        this.Size = new Size(480, 6 + (22 * words.Length - 1));
                    }

                    private class WordCheckBox : CheckBox
                    {
                        public WordCheckBox(string word, int index)
                        {
                            int x = index % 3;
                            int y = (index - x) / 3;

                            this.Location = new Point(3 + (168 * x), 2 + (22 * y));
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