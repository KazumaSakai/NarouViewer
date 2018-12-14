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
    public class GenrePanel : Panel
    {
        private NarouAPI.SearchParameter _model;
        public NarouAPI.SearchParameter model
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
        private GenreTable genreTable;

        public GenrePanel(NarouAPI.SearchParameter model)
        {
            this.DoubleBuffered = true;
            this.Name = "officialKeywordTabPage";
            this.Text = "公式キーワード";

            this.Controls.Add(this.title = new DefaultLabel("公式キーワード", "title", new Point(11, 15), true));
            this.Controls.Add(this.genreTable = new GenreTable(model) { Location = new Point(8, title.Location.Y + title.Height + 3) });

            this.Size = new Size(682, 0);
            this.defaultSize = new Size(682, genreTable.Location.Y + genreTable.Height + 8);
            this.SizeChanged += new EventHandler(OnSizeChanged);

            //  Model
            this.model = model;
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            IUpdateView updateView = this.Parent as IUpdateView;
            if (updateView == null) return;

            updateView.UpdateView();
        }

        private bool isOpen = false;
        private Size requestSize
        {
            get
            {
                return new Size(690, title.Location.Y + title.Height + genreTable.Height + 8);
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

        private class GenreTable : TableLayoutPanel
        {
            private NarouAPI.SearchParameter _model;
            public NarouAPI.SearchParameter model
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

            private readonly string[][] genres = new string[][]
            {
                    new string[]{
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
            };

            public GenreTable(NarouAPI.SearchParameter model, int line = 2)
            {
                this.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                this.ColumnCount = 2;
                this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));
                this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 76F));

                this.DoubleBuffered = true;
                this.Location = new Point(8, 26);
                this.Name = "tableLayoutPanel";
                this.RowCount = genres.Length;

                int totalHeight = 0;
                for (int i = 0; i < genres.Length; i++)
                {
                    string[] lineWords = genres[i];

                    int height = 8 + (22 * (int)Math.Ceiling((lineWords.Length - 1) / (double)line));
                    totalHeight += (height + 1);

                    this.RowStyles.Add(new RowStyle(SizeType.Absolute, height));
                    Label label = new DefaultLabel(lineWords[0], lineWords[0], new Point(3, 3), true);
                    label.Padding = new Padding(3);
                    this.Controls.Add(label, 0, i);

                    this.Controls.Add(new GenreCheckBoxsPanel(lineWords, model, line), 1, i);
                }
                this.Size = new Size(663, 1 + totalHeight);

                //  Model
                this.model = model;
            }

            private class GenreCheckBoxsPanel : Panel
            {
                private NarouAPI.SearchParameter _model;
                public NarouAPI.SearchParameter model
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

                CheckBox[] genreCheckBoxs;

                public GenreCheckBoxsPanel(string[] words, NarouAPI.SearchParameter model, int line)
                {
                    this.genreCheckBoxs = new CheckBox[words.Length];
                    for (int i = 1; i < words.Length; i++)
                    {
                        int x = (i - 1) % line;
                        int y = ((i - 1) - x) / line;
                        this.Controls.Add(this.genreCheckBoxs[i] = new CheckBox()
                        {
                            Text = words[i],
                            Location = new Point(3 + (168 * x), 2 + (22 * y)),
                            Size = new Size(165, 22),
                            UseVisualStyleBackColor = true,
                            Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128)
                        });

                        string key = words[i];
                        this.genreCheckBoxs[i].Click += new EventHandler((object sender, EventArgs e) =>
                        {
                            ClickBox(NarouAPI.SearchParameter.genreString2Enum[key]);
                        });
                    }

                    this.Dock = DockStyle.Fill;
                    this.Name = "CheckBoxsPanel";
                    this.Size = new Size(480, 6 + (22 * words.Length - 1));

                    //  Model
                    this.model = model;
                }

                private void ClickBox(NarouAPI.SearchParameter.Genre genre)
                {
                    if (this.model.genre.HasFlag(genre))
                    {
                        this.model.genre &= ~genre;
                    }
                    else
                    {
                        this.model.genre |= genre;
                    }
                }
            }
        }
    }
}
