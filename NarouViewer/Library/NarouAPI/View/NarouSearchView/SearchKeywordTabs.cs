﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using NarouViewer.API;

namespace NarouViewer
{
    public class SearchKeywordTabs : TabControl
    {
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

        public List<string> searchKeywordList;

        public SearchKeywordTabs(NarouAPI.GetParameter model)
        {
            this.searchKeywordList = new List<string>();

            this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            this.Location = new Point(12, 120);
            this.Name = "searchKeywordTabs";
            this.SelectedIndex = 0;
            this.Size = new Size(690, 0);

            this.Controls.Add(this.officialKeywordTabPage = new OfficialKeywordTabPage(searchKeywordList));
            this.Controls.Add(this.recommendKeywordTabPage = new RecommendKeywordTabPage(searchKeywordList));
            this.Controls.Add(this.replayKeywordTabPage = new ReplayKeywordTabPage(searchKeywordList));

            this.SelectedIndexChanged += new EventHandler((object sender, EventArgs e) =>
            {
                AnimationSizeUpdate();
            });

            //  Model
            this.model = model;
        }

        private bool isOpen = false;
        private Size requestSize
        {
            get
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
                        s = replayKeywordTabPage.defaultSize;
                        break;
                }
                return new Size(690, 30 + s.Height);
            }
        }
        public bool AnimationOpen()
        {
            isOpen = !isOpen;
            AnimationSizeUpdate();
            return isOpen;
        }
        public void AnimationOpen(bool open)
        {
            if (isOpen == open) return;
            isOpen = open;
            AnimationSizeUpdate();
        }
        public void Close()
        {
            isOpen = false;
            Size = new Size(requestSize.Width, 0);
        }
        private void AnimationSizeUpdate()
        {
            AnimationChangeSize(isOpen ? requestSize : new Size(requestSize.Width, 0));
        }
        private void AnimationChangeSize(Size newSize)
        {
            Size oldSize = this.Size;
            int needFrame = Math.Abs(newSize.Height - oldSize.Height) / 70;
            Animator.Animate(Math.Max(1, needFrame), (frame, frequency) =>
            {
                if (!Visible || IsDisposed) return false;

                double value = (double)frame / (double)frequency;

                this.SuspendLayout();
                this.Size = new Size(oldSize.Width + (int)((newSize.Width - oldSize.Width) * value), oldSize.Height + (int)((newSize.Height - oldSize.Height) * value));
                this.ResumeLayout();
                return true;
            });
        }

        private class OfficialKeywordTabPage : TabPage
        {
            private List<string> _model;
            public List<string> model
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

            public Size defaultSize;
            private Label title;
            private KeywordsTable keywordsTable;
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

            public OfficialKeywordTabPage(List<string> model)
            {
                this.DoubleBuffered = true;
                this.Location = new Point(4, 26);
                this.Name = "officialKeywordTabPage";
                this.TabIndex = 0;
                this.Text = "公式キーワード";
                this.UseVisualStyleBackColor = true;

                this.Controls.Add(this.title = new DefaultLabel("公式キーワード", "title", new Point(11, 15), true));
                this.Controls.Add(this.keywordsTable = new KeywordsTable(officialKeywords, model) { Location = new Point(8, title.Location.Y + title.Height + 3) });

                this.defaultSize = new Size(682, keywordsTable.Location.Y + keywordsTable.Height + 8);

                //  Model
                this.model = model;
            }
        }
        private class RecommendKeywordTabPage : TabPage
        {
            private List<string> _model;
            public List<string> model
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

            public Size defaultSize;
            private Label loveTitle;
            private Label fantasyTitle;
            private Label literatureTitle;
            private Label sfTitle;
            private KeywordsTable loveTable;
            private KeywordsTable fantasyTable;
            private KeywordsTable literatureTable;
            private KeywordsTable sfTable;
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

            public RecommendKeywordTabPage(List<string> model)
            {
                this.DoubleBuffered = true;
                this.Location = new Point(4, 26);
                this.Name = "recommendKeywordTabPage";
                this.Text = "おすすめキーワード";
                this.UseVisualStyleBackColor = true;

                this.Controls.Add(this.loveTitle = new DefaultLabel("恋愛", "love", new Point(11, 15), true));
                this.Controls.Add(this.loveTable = new KeywordsTable(loveKeywords, model) { Location = new Point(8, loveTitle.Location.Y + loveTitle.Height + 3) });

                this.Controls.Add(this.fantasyTitle = new DefaultLabel("ファンタジー", "fantasy", new Point(11, 150), true) { Location = new Point(8, loveTable.Location.Y + loveTable.Height + 15) });
                this.Controls.Add(this.fantasyTable = new KeywordsTable(fantasyKeywords, model) { Location = new Point(8, fantasyTitle.Location.Y + fantasyTitle.Height + 3) });

                this.Controls.Add(this.literatureTitle = new DefaultLabel("文芸", "literature", new Point(11, 240), true) { Location = new Point(8, fantasyTable.Location.Y + fantasyTable.Height + 15) });
                this.Controls.Add(this.literatureTable = new KeywordsTable(literatureKeywords, model) { Location = new Point(8, literatureTitle.Location.Y + literatureTitle.Height + 3) });

                this.Controls.Add(this.sfTitle = new DefaultLabel("SF", "sf", new Point(11, 520), true) { Location = new Point(8, literatureTable.Location.Y + literatureTable.Height + 15) });
                this.Controls.Add(this.sfTable = new KeywordsTable(sfKeywords, model) { Location = new Point(8, sfTitle.Location.Y + sfTitle.Height + 3) });

                this.defaultSize = new Size(682, sfTable.Location.Y + sfTable.Height + 8);

                //  Model
                this.model = model;
            }
        }
        private class ReplayKeywordTabPage : TabPage
        {
            private List<string> _model;
            public List<string> model
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

            public Size defaultSize;
            private Label replayLabel;
            private KeywordsTable replayTable;
            private static readonly string[][] replayKeywords = new string[][]
            {
                    new string[]
                    {
                        "リプレイ用", "ソード・ワールド2.0"
                    },
                    new string[]
                    {
                        "キーワード", "アリアンロッドRPG2E", "ダブルクロス The 3rd Edition", "メタリックガーディアンRPG",
                        " グランクレストRPG ", "ガーデンオーダー", "ナイトウィザード The 3rd Edition", "アルシャードセイヴァーRPG",
                        "トーキョーN◎VA THE AXLERATION", "ドラゴンアームズ改", "モノトーンミュージアムRPG", "ブレイド・オブ・アルカナ",
                        "セブン＝フォートレス メビウス", "バトルガールプロデュースＲＰＧ エースキラージーン", "バトルガールプロデュースＲＰＧ エースキラージーン",
                        "片道勇者TRPG", "神話創世RPG アマデウス", "デッドラインヒーローズRPG", "常夜国騎士譚RPG ドラクルージュ", "巨獣討伐RPG コロッサルハンター"
                    },
            };

            public ReplayKeywordTabPage(List<string> model)
            {
                this.DoubleBuffered = true;
                this.Location = new Point(4, 26);
                this.Name = "replayKeywordTab";
                this.Size = new Size(682, 494);
                this.Text = "リプレイ用キーワード";
                this.UseVisualStyleBackColor = true;

                this.Controls.Add(this.replayLabel = new DefaultLabel("リプレイ用キーワード", "love", new Point(11, 15), true));
                this.Controls.Add(this.replayTable = new KeywordsTable(replayKeywords, model, 1) { Location = new Point(8, replayLabel.Location.Y + replayLabel.Height + 3) });

                this.defaultSize = new Size(682, replayTable.Location.Y + replayTable.Height + 8);

                //  Model
                this.model = model;
            }
        }

        private class KeywordsTable : TableLayoutPanel
        {
            private List<string> _model;
            public List<string> model
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

            public KeywordsTable(string[][] words, List<string> model, int line = 3)
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

                    int height = 8 + (22 * (int)Math.Ceiling((lineWords.Length - 1) / (double)line));
                    totalHeight += (height + 1);

                    this.RowStyles.Add(new RowStyle(SizeType.Absolute, height));
                    Label label = new DefaultLabel(lineWords[0], lineWords[0], new Point(3, 3), true);
                    label.Padding = new Padding(3);
                    this.Controls.Add(label, 0, i);

                    this.Controls.Add(new WordCheckBoxsPanel(lineWords, model, line), 1, i);
                }
                this.Size = new Size(663, 1 + totalHeight);

                //  Model
                this.model = model;
            }

            private class WordCheckBoxsPanel : Panel
            {
                private List<string> _model;
                public List<string> model
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

                WordCheckBox[] wordCheckBox;

                public WordCheckBoxsPanel(string[] words, List<string> model, int line)
                {
                    this.wordCheckBox = new WordCheckBox[words.Length];
                    for (int i = 1; i < words.Length; i++)
                    {
                        this.Controls.Add(this.wordCheckBox[i] = new WordCheckBox(words[i], model));

                        int x = (i - 1) % line;
                        int y = ((i - 1) - x) / line;
                        this.wordCheckBox[i].Location = new Point(3 + (168 * x), 2 + (22 * y));
                    }

                    this.Dock = DockStyle.Fill;
                    this.Name = "CheckBoxsPanel";
                    this.Size = new Size(480, 6 + (22 * words.Length - 1));

                    //  Model
                    this.model = model;
                }

                private class WordCheckBox : CheckBox
                {
                    private List<string> _model;
                    public List<string> model
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

                    public WordCheckBox(string word, List<string> model)
                    {
                        this.Size = new Size(165, 22);
                        this.Text = word;
                        this.Name = "WordCheckBox " + word;
                        this.UseVisualStyleBackColor = true;
                        this.Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);

                        this.CheckedChanged += new EventHandler((object sender, EventArgs e) =>
                        {
                            if (this.Checked)
                            {
                                AddWord(word);
                            }
                            else
                            {
                                RemoveWord(word);
                            }
                        });

                        //  Model
                        this.model = model;
                    }

                    private void AddWord(string word)
                    {
                        if (!model.Contains(word))
                        {
                            model.Add(word);
                        }
                    }
                    private void RemoveWord(string word)
                    {
                        model.Remove(word);
                    }
                }
            }
        }
    }
}