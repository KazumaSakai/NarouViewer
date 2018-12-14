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
    public class SearchKeywordTabControl : TabControl, IRequestSize, IAnimationOpen
    {
        #region --- Model ---
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
        #endregion

        #region --- Controller ---
        public StringEventHandler controller { get; set; }
        #endregion

        #region --- IRequestSize ---
        public Size RequestSize
        {
            get
            {
                return new Size(690, isOpen ? (30 + (this.TabPages[SelectedIndex] as IRequestSize).RequestSize.Height) : 0);
            }
        }
        #endregion

        #region --- 子コントロール ---
        private CheckBoxsTablesTabPage officialKeywordTabPage;
        private CheckBoxsTablesTabPage recommendKeywordTabPage;
        private CheckBoxsTablesTabPage replayKeywordTabPage;
        #endregion

        #region --- データ ---
        private static readonly (string[][] words, string title, int line)[] officialData = new (string[][], string title, int)[]
        {
            (new string[][]
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
            }, "公式キーワード", 3)
        };
        private static readonly (string[][] words, string title, int line)[] recommendData = new (string[][], string title, int)[]
        {
            (new string[][]
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
            }, "恋愛", 3),
            (new string[][]
            {
                new string[]
                {
                    "ハイファンタジー", "オリジナル戦記"
                },
                new string[]
                {
                    "ローファンタジー", "伝奇"
                }
            }, "ファンタジー", 3),
            (new string[][]
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
            }, "文芸", 3),
            (new string[][]
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
            }, "SF", 3)
        };
        private static readonly (string[][] words, string title, int line)[] replayData = new (string[][], string title, int)[]
        {
            (new string[][]
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
                }
            }, "リプレイ用キーワード", 3)
        };
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="model">モデル</param>
        public SearchKeywordTabControl(NarouAPI.SearchParameter model, StringEventHandler controller)
        {
            this.Font = new Font("ＭＳ Ｐゴシック", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            this.Name = "searchKeywordTabs";
            this.SelectedIndex = 0;

            this.Controls.Add(this.officialKeywordTabPage = new CheckBoxsTablesTabPage(officialData, "公式キーワード", controller));
            this.Controls.Add(this.recommendKeywordTabPage = new CheckBoxsTablesTabPage(recommendData, "おすすめキーワード", controller));
            this.Controls.Add(this.replayKeywordTabPage = new CheckBoxsTablesTabPage(replayData, "リプレイ用キーワード", controller));

            this.SelectedIndexChanged += new EventHandler((object sender, EventArgs e) =>
            {
                SizeUpdate();
            });

            //  Size
            this.SizeChanged += new EventHandler(OnSizeChanged);
            this.Size = RequestSize;

            //  Model
            this.model = model;

            //  Controller
            this.controller = controller;
        }

        /// <summary>
        /// サイズが変更されたときのイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSizeChanged(object sender, EventArgs e)
        {
            IUpdateView updateView = this.Parent as IUpdateView;
            if (updateView == null) return;

            updateView.UpdateView();
        }

        #region --- アニメーション ---
        private bool isOpen = false;
        #endregion
        public bool AnimationOpen(bool use_Animation, bool open)
        {
            isOpen = open;
            SizeUpdate(use_Animation);
            return isOpen;
        }
        public bool AnimationOpen(bool use_Animation)
        {
            return AnimationOpen(!isOpen);
        }
        private void SizeUpdate(bool use_Animation = true)
        {
            if(use_Animation)
            {
                Size oldSize = this.Size;
                Size deltaSize = RequestSize - oldSize;
                int needFrame = Math.Abs(deltaSize.Height) / 70;

                Animator.Animate(Math.Max(1, needFrame), (frame, frequency) =>
                {
                    if (!Visible || IsDisposed) return false;

                    double value = (double)frame / (double)frequency;

                    this.SuspendLayout();
                    this.Size = new Size(oldSize.Width + (int)(deltaSize.Width * value), oldSize.Height + (int)(deltaSize.Height * value));
                    this.ResumeLayout();
                    return true;
                });
            }
            else
            {
                this.Size = RequestSize;
            }
        }
    }
}
