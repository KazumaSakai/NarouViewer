using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.IO;
using System.IO.Compression;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace NarouViewer.API
{
    /// <summary>
    /// 小説家になろう API
    /// </summary>
    public class NarouAPI
    {
        public class NovelData
        {
            public int allcount { get; set; }
            public string title { get; set; }
            public string ncode { get; set; }
            public int userid { get; set; }
            public string writer { get; set; }
            public string story { get; set; }
            public int biggenre { get; set; }
            public int genre { get; set; }
            public string gensaku { get; set; }
            public string keyword { get; set; }
            public string general_firstup { get; set; }
            public string general_lastup { get; set; }
            public int novel_type { get; set; }
            public int end { get; set; }
            public int general_all_no { get; set; }
            public int length { get; set; }
            public int time { get; set; }
            public int isstop { get; set; }
            public int isr15 { get; set; }
            public int isbl { get; set; }
            public int isgl { get; set; }
            public int iszankoku { get; set; }
            public int istensei { get; set; }
            public int istenni { get; set; }
            public int pc_or_k { get; set; }
            public int global_point { get; set; }
            public int fav_novel_cnt { get; set; }
            public int review_cnt { get; set; }
            public int all_point { get; set; }
            public int all_hyoka_cnt { get; set; }
            public int sasie_cnt { get; set; }
            public int kaiwaritu { get; set; }
            public string novelupdated_at { get; set; }
            public string updated_at { get; set; }
            public int weekly_unique { get; set; }
        }

        /// <summary>
        /// 出力パラメータ
        /// </summary>
        public class SearchParameter
        {
            /// <summary>
            /// 出力する形式
            /// </summary>
            public enum OutType
            {
                yaml,
                json,
                php
            }
            /// <summary>
            /// 出力する順序
            /// </summary>
            public enum Order
            {
                /// <summary>
                /// 新着順
                /// </summary>
                newest,
                /// <summary>
                /// 閲覧者の多い順(未実装)
                /// </summary>
                allunique,
                /// <summary>
                /// ブックマーク数の多い順
                /// </summary>
                favnovelcnt,
                /// <summary>
                /// レビュー数の多い順
                /// </summary>
                reviewcnt,
                /// <summary>
                /// 総合評価の高い順
                /// </summary>
                hyoka,
                /// <summary>
                /// 総合評価の低い順
                /// </summary>
                hyokaasc,
                /// <summary>
                /// 感想の多い順
                /// </summary>
                impressioncnt,
                /// <summary>
                /// 評価者数の多い順
                /// </summary>
                hyokacnt,
                /// <summary>
                /// 評価者数の少ない順
                /// </summary>
                hyokacntasc,
                /// <summary>
                /// 週間ユニークユーザの多い順
                /// <para>毎週火曜日早朝リセット</para>
                /// <para>(前週の日曜日から土曜日分)</para>
                /// </summary>
                weekly,
                /// <summary>
                /// 小説本文の文字数が多い順
                /// </summary>
                lengthdesc,
                /// <summary>
                /// 小説本文の文字数が少ない順
                /// </summary>
                lengthasc,
                /// <summary>
                /// Nコードが新しい順
                /// </summary>
                ncodedesc,
                /// <summary>
                /// 古い順
                /// </summary>
                old
            }
            /// <summary>
            /// 最終掲載日
            /// </summary>
            public enum LastUp
            {
                /// <summary>
                /// 未指定
                /// </summary>
                none,
                /// <summary>
                /// 今週
                /// </summary>
                thisWeek,
                /// <summary>
                /// 先週
                /// </summary>
                lastWeek,
                /// <summary>
                /// 過去七日間
                /// </summary>
                sevenDay,
                /// <summary>
                /// 今月
                /// </summary>
                thisMonth,
                /// <summary>
                /// 先月
                /// </summary>
                lastMonth
            }
            /// <summary>
            /// 出力する項目
            /// </summary>
            [Flags] public enum ItemFlag
            {
                /// <summary>
                /// 小説名
                /// </summary>
                title = 1,
                /// <summary>
                /// Nコード
                /// </summary>
                ncode = 1 << 1,
                /// <summary>
                /// 作者のユーザID
                /// </summary>
                userid = 1 << 2,
                /// <summary>
                /// 作者名
                /// </summary>
                writer = 1 << 3,
                /// <summary>
                /// 小説のあらすじ
                /// </summary>
                story = 1 << 4,
                /// <summary>
                /// 大ジャンル
                /// <para>1: 恋愛</para>
                /// <para>2: ファンタジー</para>
                /// <para>3: 文芸</para>
                /// <para>4: SF</para>
                /// <para>99: その他</para>
                /// <para>98: ノンジャンル</para>
                /// </summary>
                biggenre = 1 << 5,
                /// <summary>
                /// ジャンル
                /// <para>101: 異世界, 102: 現実世界</para>
                /// <para>201: ハイファンタジー, 202: ローファンタジー</para>
                /// <para>301: 純文学, 302: ヒューマンドラマ, 303: 歴史, 304: 推理, 305: ホラー, 306: アクション, 307: コメディー</para>
                /// <para>401: VRゲーム, 402: 宇宙, 403: 空想科学, 404: パニック</para>
                /// <para>9901: 童話, 9902: 詩, 9903: エッセイ, 9904: リプレイ, 9999: その他</para>
                /// <para>9801: ノンジャンル</para>
                /// </summary>
                genre = 1 << 6,
                /// <summary>
                /// キーワード
                /// </summary>
                keyword = 1 << 7,
                /// <summary>
                /// 初回掲載日
                /// </summary>
                general_firstup = 1 << 8,
                /// <summary>
                /// 最終掲載日
                /// </summary>
                general_lastup = 1 << 9,
                /// <summary>
                /// 連載、短編
                /// </summary>
                noveltype = 1 << 10,
                /// <summary>
                /// 完結
                /// </summary>
                end = 1 << 11,
                /// <summary>
                /// 全掲載部分数
                /// </summary>
                general_all_no = 1 << 12,
                /// <summary>
                /// 小説文字数
                /// </summary>
                length = 1 << 13,
                /// <summary>
                /// 読了時間(分単位)
                /// </summary>
                time = 1 << 14,
                /// <summary>
                /// 長期連載停止中
                /// </summary>
                isstop = 1 << 15,
                /// <summary>
                /// R15
                /// </summary>
                isr15 = 1 << 16,
                /// <summary>
                /// ボーイズラブ
                /// </summary>
                isbl = 1 << 17,
                /// <summary>
                /// ガールズラブ
                /// </summary>
                isgl = 1 << 18,
                /// <summary>
                /// 残酷な描写あり
                /// </summary>
                iszankoku = 1 << 19,
                /// <summary>
                /// 異世界転生
                /// </summary>
                istensei = 1 << 20,
                /// <summary>
                /// 異世界転移
                /// </summary>
                istenni = 1 << 21,
                /// <summary>
                /// 投稿端末
                /// </summary>
                pc_or_k = 1 << 22,
                /// <summary>
                /// 総合得点
                /// </summary>
                global_point = 1 << 23,
                /// <summary>
                /// ブックマーク数 
                /// </summary>
                fav_novel_cnt = 1 << 24,
                /// <summary>
                /// レビュー数
                /// </summary>
                review_cnt = 1 << 25,
                /// <summary>
                /// 評価点 
                /// </summary>
                all_point = 1 << 26,
                /// <summary>
                /// all_hyoka_cnt
                /// </summary>
                all_hyoka_cnt = 1 << 27,
                /// <summary>
                /// sasie_cnt
                /// </summary>
                sasie_cnt = 1 << 28,
                /// <summary>
                /// kaiwaritu
                /// </summary>
                kaiwaritu = 1 << 29,
                /// <summary>
                /// novelupdated_at
                /// </summary>
                novelupdated_at = 1 << 30,
                /// <summary>
                /// updated_at
                /// </summary>
                updated_at = 1 << 31
            }
            /// <summary>
            /// 検索ワード対象
            /// </summary>
            [Flags] public enum SearchWordTarget
            {
                title = 1,
                summary = 1 << 1,
                keyword = 1 << 2,
                writerName = 1 << 3
            }
            /// <summary>
            /// ビッグジャンル
            /// </summary>
            [Flags] public enum BigGenre
            {
                /// <summary>
                /// 1: 恋愛
                /// </summary>
                love = 1,
                /// <summary>
                /// 2: ファンタジー
                /// </summary>
                fantasy = 1 << 1,
                /// <summary>
                /// 3: 文芸
                /// </summary>
                literature = 1 << 2,
                /// <summary>
                /// 4: SF
                /// </summary>
                sf = 1 << 3,
                /// <summary>
                /// 99: その他
                /// </summary>
                other = 1 << 4,
                /// <summary>
                /// 98: ノンジャンル
                /// </summary>
                nongenre = 1 << 5
            }
            /// <summary>
            /// ジャンル
            /// </summary>
            [Flags] public enum Genre
            {
                /// <summary>
                /// 101: 異世界〔恋愛〕
                /// </summary>
                other_worldy = 1,
                /// <summary>
                /// 102: 現実世界〔恋愛〕
                /// </summary>
                real_world = 1 << 1,
                /// <summary>
                /// 201: ハイファンタジー〔ファンタジー〕
                /// </summary>
                high_fantasy = 1 << 2,
                /// <summary>
                /// 202: ローファンタジー〔ファンタジー〕
                /// </summary>
                low_fantasy = 1 << 3,
                /// <summary>
                /// 301: 純文学〔文芸〕
                /// </summary>
                pure_literature = 1 << 4,
                /// <summary>
                /// 302: ヒューマンドラマ〔文芸〕
                /// </summary>
                human_drama = 1 << 5,
                /// <summary>
                /// 303: 歴史〔文芸〕
                /// </summary>
                history = 1 << 6,
                /// <summary>
                /// 304: 推理〔文芸〕
                /// </summary>
                reasoning = 1 << 7,
                /// <summary>
                /// 305: ホラー〔文芸〕
                /// </summary>
                horror = 1 << 8,
                /// <summary>
                /// 306: アクション〔文芸〕
                /// </summary>
                action = 1 << 9,
                /// <summary>
                /// 307: コメディー〔文芸〕
                /// </summary>
                comedy = 1 << 10,
                /// <summary>
                /// 401: VRゲーム〔SF〕
                /// </summary>
                vr_game = 1 << 11,
                /// <summary>
                /// 402: 宇宙〔SF〕
                /// </summary>
                universe = 1 << 12,
                /// <summary>
                /// 403: 空想科学〔SF〕
                /// </summary>
                science_fiction = 1 << 13,
                /// <summary>
                /// 404: パニック〔SF〕
                /// </summary>
                panic = 1 << 14,
                /// <summary>
                /// 9901: 童話〔その他〕
                /// </summary>
                fairy_tale = 1 << 15,
                /// <summary>
                /// 9902: 詩〔その他〕
                /// </summary>
                poem = 1 << 16,
                /// <summary>
                /// 9903: エッセイ〔その他〕
                /// </summary>
                essay = 1 << 17,
                /// <summary>
                /// 9904: リプレイ〔その他〕
                /// </summary>
                replay = 1 << 18,
                /// <summary>
                /// 9999: その他〔その他〕
                /// </summary>
                other = 1 << 19,
                /// <summary>
                /// 9801: ノンジャンル〔ノンジャンル〕
                /// </summary>
                nongenre = 1 << 20
            }

            /// <summary>
            /// 検索するキーワード
            /// </summary>
            [Flags] public enum SearchKeyWord
            {
                /// <summary>
                /// R15
                /// </summary>
                r15 = 1,
                /// <summary>
                /// ボーイズラブ
                /// </summary>
                boysLove = 1 << 1,
                /// <summary>
                /// ガールズラブ
                /// </summary>
                girlsLove = 1 << 2,
                /// <summary>
                /// 残酷な描写あり
                /// </summary>
                brutal = 1 << 3,
                /// <summary>
                /// 異世界転生
                /// </summary>
                reincarnation = 1 << 4,
                /// <summary>
                /// 異世界転移
                /// </summary>
                transition = 1 << 5
            }
            /// <summary>
            /// 検索する小説タイプ
            /// </summary>
            public enum SearchNovelType
            {
                /// <summary>
                /// 未指定
                /// </summary>
                none,
                /// <summary>
                /// 短編小説
                /// </summary>
                short_Story,
                /// <summary>
                /// 連載小説
                /// </summary>
                series,
                /// <summary>
                /// 連載中小説
                /// </summary>
                serializing,
                /// <summary>
                /// 完結した小説
                /// </summary>
                complete,
                /// <summary>
                /// 完結した連載小説
                /// </summary>
                complete_Series,
            }
            /// <summary>
            /// 検索する文体
            /// </summary>
            [Flags] public enum SearchStyle
            {
                /// <summary>
                /// 字下げが適切
                /// </summary>
                indent = 1,
                /// <summary>
                /// 改行が適切
                /// </summary>
                line = 1 << 1
            }

            /// <summary>
            /// GZIPにて圧縮して送信するか
            /// </summary>
            public bool useGZIP;
            /// <summary>
            /// 出力する形式
            /// </summary>
            public OutType outType;
            /// <summary>
            /// 出力する項目(bit)
            /// </summary>
            public ItemFlag getItemFlag;
            /// <summary>
            /// 出力する数
            /// </summary>
            public int limit;
            /// <summary>
            /// 出力を開始する位置
            /// </summary>
            public int start;
            /// <summary>
            /// 出力する順序
            /// </summary>
            public Order order;
            /// <summary>
            /// 検索ワード
            /// </summary>
            public string word;
            /// <summary>
            /// 検索除外ワード
            /// </summary>
            public string notWord;
            /// <summary>
            /// 検索ワード対象
            /// </summary>
            public SearchWordTarget searchWordTarget;
            /// <summary>
            /// 検索するビッグジャンル
            /// </summary>
            public BigGenre bigGenre;
            /// <summary>
            /// 検索除外するビッグジャンル
            /// </summary>
            public BigGenre notBigGenre;
            /// <summary>
            /// 検索するジャンル
            /// </summary>
            public Genre genre;
            /// <summary>
            /// 検索除外するジャンル
            /// </summary>
            public Genre notGenre;
            /// <summary>
            /// 検索するユーザーID
            /// </summary>
            public int[] search_UserID;
            /// <summary>
            /// 検索するキーワード
            /// </summary>
            public SearchKeyWord searchKeyword;
            /// <summary>
            /// 検索する最小文字数
            /// </summary>
            public int min_Length;
            /// <summary>
            /// 検索する最大文字数
            /// </summary>
            public int max_Length;
            /// <summary>
            /// 検索する最小会話率
            /// </summary>
            public int min_Per_Talk;
            /// <summary>
            /// 検索する最大会話率
            /// </summary>
            public int max_Per_Talk;
            /// <summary>
            /// 検索する最小挿絵数
            /// </summary>
            public int min_Illustration;
            /// <summary>
            /// 検索する最大挿絵数
            /// </summary>
            public int max_Illustration;
            /// <summary>
            /// 検索する最低読了時間
            /// </summary>
            public int min_Time;
            /// <summary>
            /// 検索する最大読了時間
            /// </summary>
            public int max_Time;
            /// <summary>
            /// 検索するNコード
            /// </summary>
            public string[] ncode;
            /// <summary>
            /// 検索する小説タイプ
            /// </summary>
            public SearchNovelType searchNovelType;
            /// <summary>
            /// 検索する文体
            /// </summary>
            public SearchStyle search_Style;
            /// <summary>
            /// 連載停止中の小説を検索しない
            /// </summary>
            public bool search_NotStop;
            /// <summary>
            /// ピックアップ作品のみ検索する
            /// </summary>
            public bool search_PickUp;
            /// <summary>
            /// 検索する最終掲載日
            /// </summary>
            public LastUp lastUp;
            /// <summary>
            /// 週間ユニークユーザー数を取得する
            /// </summary>
            public bool weekly;
            /// <summary>
            /// 検索ワードリスト
            /// </summary>
            public List<string> searchKeywordList = new List<string>();
            public List<string> eSearchKeywordList = new List<string>();

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="useGZIP">GZIPにて圧縮するか</param>
            /// <param name="outType">出力する形式</param>
            /// <param name="getItemFlag">出力する項目(bit)</param>
            /// <param name="limit">出力する数</param>
            /// <param name="start">出力を開始する位置</param>
            /// <param name="order">出力する順序</param>
            /// <param name="word">検索ワード</param>
            /// <param name="notWord">検索除外ワード</param>
            /// <param name="search_Title">作品名を検索する対象とする</param>
            /// <param name="search_Summary">あらすじを検索対象とする</param>
            /// <param name="search_KeyWord">キーワードを検索対象とする</param>
            /// <param name="search_WriterName">作者名を検索対象とする</param>
            public SearchParameter()
            {
                this.useGZIP = true;
                this.outType = OutType.json;
                this.getItemFlag = 0;
                this.limit = 20;
                this.start = 1;
                this.order = Order.newest;
                this.word = "";
                this.notWord = "";
                this.searchWordTarget = 0;
                this.bigGenre = 0;
                this.notBigGenre = 0;
                this.genre = 0;
                this.notGenre = 0;
                this.search_UserID = null;
                this.searchKeyword = 0;
                this.min_Length = 0;
                this.max_Length = 0;
                this.min_Per_Talk = 0;
                this.max_Per_Talk = 0;
                this.min_Illustration = 0;
                this.max_Illustration = 0;
                this.min_Time = 0;
                this.max_Time = 0;
                this.ncode = null;
                this.searchNovelType = 0;
                this.search_Style = 0;
                this.search_NotStop = false;
                this.search_PickUp = false;
                this.lastUp = 0;
                this.weekly = true;
            }

            public static string ItemFlagToString(ItemFlag itemFlag)
            {
                string str = "";

                string[] code = { "t", "n", "u", "w", "s", "bg", "g", "k", "gf", "gl", "nt", "e", "ga", "l", "ti", "i", "ir", "ibl", "igl", "izk", "its", "iti", "p", "gp", "f", "r", "a", "ah", "sa", "ka", "nu", "ua" };
                bool isFirst = true;
                for (int i = 0; i < 31; i++)
                {
                    if(itemFlag.HasFlag((ItemFlag)(1 << i)))
                    {
                        if (isFirst) isFirst = false;
                        else str += "-";

                        str += code[i];
                    }
                }
                

                return str;
            }
            public static string BigGenreToString(BigGenre bigGenre)
            {
                string str = "";
                string[] code = { "1", "2", "3", "4", "99", "98" };
                bool isFirst = true;
                for (int i = 0; i < 6; i++)
                {
                    if (bigGenre.HasFlag((BigGenre)(1 << i)))
                    {
                        if (isFirst) isFirst = false;
                        else str += "-";

                        str += code[i];
                    }
                }
                return str;
            }

            public static readonly Dictionary<Genre, int> genreEnum2int = new Dictionary<Genre, int>
            {
                { Genre.other_worldy, 101 },
                { Genre.real_world, 102 },
                { Genre.high_fantasy, 201 },
                { Genre.low_fantasy, 202 },
                { Genre.pure_literature, 301 },
                { Genre.human_drama, 302 },
                { Genre.history, 303 },
                { Genre.reasoning, 304 },
                { Genre.horror, 305 },
                { Genre.action, 306 },
                { Genre.comedy, 307 },
                { Genre.vr_game, 401 },
                { Genre.universe, 402 },
                { Genre.science_fiction, 403 },
                { Genre.panic, 404 },
                { Genre.fairy_tale, 9901 },
                { Genre.poem, 9902 },
                { Genre.essay, 9903 },
                { Genre.replay, 9904 },
                { Genre.other, 9999 },
                { Genre.nongenre, 9801 }
            };
            public static readonly Dictionary<string, Genre> genreString2Enum = new Dictionary<string, Genre>
            {
                { "異世界", Genre.other_worldy },
                { "現実世界", Genre.real_world },
                { "ハイファンタジー" , Genre.high_fantasy },
                { "ローファンタジー" , Genre.low_fantasy },
                { "純文学" , Genre.pure_literature },
                { "ヒューマンドラマ" , Genre.human_drama },
                { "歴史" , Genre.history },
                { "推理" , Genre.reasoning },
                { "ホラー" , Genre.horror },
                { "アクション" , Genre.action },
                { "コメディー" , Genre.comedy },
                { "VRゲーム" , Genre.vr_game },
                { "宇宙" , Genre.universe },
                { "空想科学" , Genre.science_fiction },
                { "パニック" , Genre.panic },
                { "童話" , Genre.fairy_tale },
                { "詩" , Genre.poem },
                { "エッセイ" , Genre.essay },
                { "リプレイ" , Genre.replay },
                { "その他" , Genre.other },
                { "ノンジャンル" , Genre.nongenre }
            };
            public static readonly Dictionary<int, string> genreint2String = new Dictionary<int, string>()
            {
                { 101, "異世界〔恋愛〕" },
                { 102, "現実世界〔恋愛〕"},
                { 201, "ハイファンタジー〔ファンタジー〕" },
                { 202, "ローファンタジー〔ファンタジー〕" },
                { 301, "純文学〔文芸〕" },
                { 302, "ヒューマンドラマ〔文芸〕" },
                { 303, "歴史〔文芸〕" },
                { 304, "推理〔文芸〕" },
                { 305, "ホラー〔文芸〕" },
                { 306, "アクション〔文芸〕" },
                { 307, "コメディー〔文芸〕" },
                { 401, "VRゲーム〔SF〕" },
                { 402, "宇宙〔SF〕" },
                { 403, "空想科学〔SF〕" },
                { 404, "パニック〔SF〕" },
                { 9901, "童話〔その他〕" },
                { 9902, "詩〔その他〕" },
                { 9903, "エッセイ〔その他〕" },
                { 9904, "リプレイ〔その他〕" },
                { 9999, "その他〔その他〕" },
                { 9801, "ノンジャンル〔ノンジャンル〕" },
            };
            public static readonly Dictionary<int, Genre> genreint2Enum = new Dictionary<int, Genre>()
            {
                { 101 , Genre.other_worldy },
                { 102 , Genre.real_world },
                { 201 , Genre.high_fantasy },
                { 202 , Genre.low_fantasy },
                { 301 , Genre.pure_literature },
                { 302 , Genre.human_drama },
                { 303 , Genre.history },
                { 304 , Genre.reasoning },
                { 305 , Genre.horror },
                { 306 , Genre.action },
                { 307 , Genre.comedy },
                { 401 , Genre.vr_game },
                { 402 , Genre.universe },
                { 403 , Genre.science_fiction },
                { 404 , Genre.panic },
                { 9901, Genre.fairy_tale },
                { 9902, Genre.poem },
                { 9903, Genre.essay },
                { 9904, Genre.replay },
                { 9999, Genre.other },
                { 9801, Genre.nongenre }
            };

            public static string GenreToString(Genre genre)
            {
                string str = "";
                bool isFirst = true;
                for (int i = 0; i < 21; i++)
                {
                    Genre g = (Genre)(1 << i);
                    if (genre.HasFlag(g))
                    {
                        if (isFirst) isFirst = false;
                        else str += "-";

                        str += genreEnum2int[g].ToString();
                    }
                }
                return str;
            }
            
            public override string ToString()
            {
                string args = "out=" + outType.ToString();

                if (useGZIP) args += "&gzip=5";
                if (getItemFlag != 0) args += "&of=" + ItemFlagToString(getItemFlag);
                if (limit != 20) args += "&lim=" + limit.ToString();
                if (start != 1) args += "&st=" + start.ToString();
                if (order != Order.newest) args += "&order=" + order.ToString();
                if (word != "" || notWord != "")
                {
                    if (word != "" || searchKeywordList.Count != 0)
                    {
                        args += "&word=" + word;

                        StringBuilder sb = new StringBuilder();
                        foreach (string keyword in searchKeywordList)
                        {
                            sb.Append(" ");
                            sb.Append(keyword);
                        }
                        args += sb.ToString();
                    }
                    if (notWord != "" || eSearchKeywordList.Count != 0)
                    {
                        args += "&notword=" + notWord;

                        StringBuilder sb = new StringBuilder();
                        foreach (string keyword in eSearchKeywordList)
                        {
                            sb.Append(" ");
                            sb.Append(keyword);
                        }
                        args += sb.ToString();
                    }

                    if (searchWordTarget != 0)
                    {
                        bool search_title = searchWordTarget.HasFlag(SearchWordTarget.title);
                        bool search_summary = searchWordTarget.HasFlag(SearchWordTarget.summary);
                        bool search_keyword = searchWordTarget.HasFlag(SearchWordTarget.keyword);
                        bool search_writerName = searchWordTarget.HasFlag(SearchWordTarget.writerName);

                        args += "&title=1" + (search_title ? "1" : "0");
                        args += "&ex=1" + (search_summary ? "1" : "0");
                        args += "&keyword=1" + (search_keyword ? "1" : "0");
                        args += "&wname=1" + (search_writerName ? "1" : "0");
                    }
                }
                if(bigGenre != 0) args += "&biggenre=" + (BigGenreToString(bigGenre));
                else if(notBigGenre != 0) args += "&notbiggenre=" + (BigGenreToString(notBigGenre));
                if (genre != 0) args += "&genre=" + (GenreToString(genre));
                else if (notGenre != 0) args += "&notgenre=" + (GenreToString(notGenre));
                if (search_UserID != null && search_UserID.Length > 0)
                {
                    args += "&userid=";
                    bool isFirst = true;
                    foreach(int userid in search_UserID)
                    {
                        if(isFirst) isFirst = false;
                        else args += "-";
                        args += userid.ToString();
                    }
                }
                if(searchKeyword != 0)
                {
                    bool isr15 = searchKeyword.HasFlag(SearchKeyWord.r15);
                    bool boysLove = searchKeyword.HasFlag(SearchKeyWord.boysLove);
                    bool girlsLove = searchKeyword.HasFlag(SearchKeyWord.girlsLove);
                    bool brutal = searchKeyword.HasFlag(SearchKeyWord.brutal);
                    bool reincarnation = searchKeyword.HasFlag(SearchKeyWord.reincarnation);
                    bool transition = searchKeyword.HasFlag(SearchKeyWord.transition);

                    args += "&isr15=" + (isr15 ? "1" : "0");
                    args += "&isbl=" + (boysLove ? "1" : "0");
                    args += "&isgl=" + (girlsLove ? "1" : "0");
                    args += "&iszankoku=" + (brutal ? "1" : "0");
                    args += "&isristensei15=" + (reincarnation ? "1" : "0");
                    args += "&istenni=" + (transition ? "1" : "0");
                }
                if (min_Length != 0 || max_Length != 0)
                {
                    if(min_Length != 0)
                    {
                        if (max_Length == 0)
                        {
                            args += "&length=" + min_Length + "-";
                        }
                        else if (min_Length >= max_Length)
                        {
                            args += "&length=" + min_Length;
                        }
                        else
                        {
                            args += "&length=" + min_Length + "-" + max_Length;
                        }
                    }else
                    {
                        args += "&length=-" + max_Length;
                    }
                }
                if (min_Per_Talk != 0 || max_Per_Talk != 0)
                {
                    if (min_Per_Talk != 0)
                    {
                        if (max_Per_Talk == 0)
                        {
                            args += "&kaiwaritu=" + min_Per_Talk + "-";
                        }
                        else if (min_Per_Talk >= max_Per_Talk)
                        {
                            args += "&kaiwaritu=" + min_Per_Talk;
                        }
                        else
                        {
                            args += "&kaiwaritu=" + min_Per_Talk + "-" + max_Per_Talk;
                        }
                    }
                    else
                    {
                        args += "&kaiwaritu=-" + max_Per_Talk;
                    }
                }
                if (min_Illustration != 0 || max_Illustration != 0)
                {
                    if (min_Illustration != 0)
                    {
                        if (max_Illustration == 0)
                        {
                            args += "&sasie=" + min_Illustration + "-";
                        }
                        else if (min_Illustration >= max_Illustration)
                        {
                            args += "&sasie=" + min_Illustration;
                        }
                        else
                        {
                            args += "&sasie=" + min_Illustration + "-" + max_Illustration;
                        }
                    }
                    else
                    {
                        args += "&sasie=-" + max_Illustration;
                    }
                }
                if (min_Time != 0 || max_Time != 0)
                {
                    if (min_Time != 0)
                    {
                        if (max_Time == 0)
                        {
                            args += "&time=" + min_Time + "-";
                        }
                        else if (min_Time >= max_Time)
                        {
                            args += "&time=" + min_Time;
                        }
                        else
                        {
                            args += "&time=" + min_Time + "-" + max_Time;
                        }
                    }
                    else
                    {
                        args += "&time=-" + max_Time;
                    }
                }
                if (ncode != null && ncode.Length > 0)
                {
                    args += "&ncode=";
                    bool isFirst = true;
                    foreach (string nc in ncode)
                    {
                        if (isFirst) isFirst = false;
                        else args += "-";
                        args += nc;
                    }
                }
                if (searchNovelType != 0)
                {
                    string[] code = { "", "t", "r", "re", "ter", "er" };
                    args += "&type=" + code[(int)searchNovelType];
                }
                if (search_Style != 0)
                {
                    bool indent = search_Style.HasFlag(SearchStyle.indent);
                    bool line = search_Style.HasFlag(SearchStyle.line);

                    if(indent)
                    {
                        if(line)
                        {
                            args += "&buntai=6";
                        }
                        else
                        {
                            args += "&buntai=4-6";
                        }
                    }
                    else
                    {
                        args += "&buntai=2-6";
                    }
                }
                if (search_NotStop)
                {
                    args += "&stop=1";
                }
                if (search_PickUp)
                {
                    args += "&ispickup=1";
                }
                if (lastUp != 0)
                {
                    args += "&lastup=" + lastUp.ToString();
                }
                if (weekly) args += "&opt=weekly";

                return args;
            }
        }
        private static string url = "https://api.syosetu.com/novelapi/api/?";

        public static async Task<List<NovelData>> GetSearchData()
        {
            return await GetSearchData(new SearchParameter());
        }
        public static async Task<List<NovelData>> GetSearchData(SearchParameter getParameter)
        {
            string url = NarouAPI.url + getParameter.ToString();

            List<NovelData> getData = null;
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if(response.IsSuccessStatusCode)
                {
                    if(getParameter.useGZIP)
                    {
                        using (var ms = await response.Content.ReadAsStreamAsync())
                        {
                            Stream stream = await response.Content.ReadAsStreamAsync();
                            string data = DecompressGZIP(stream);

                            switch(getParameter.outType)
                            {
                                case SearchParameter.OutType.json:
                                    getData = JsonConvert.DeserializeObject<List<NovelData>>(data);
                                    break;

                                case SearchParameter.OutType.yaml:
                                    var deserializer = new Deserializer();
                                    getData = deserializer.Deserialize<List<NovelData>>(data);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        string str = await response.Content.ReadAsStringAsync();
                    }
                }
            }

            return getData;
        }

        public static async Task<string> GetHtml(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";

            HttpWebResponse res = (HttpWebResponse)await req.GetResponseAsync();

            Stream s = res.GetResponseStream();
            StreamReader sr = new StreamReader(s);

            return sr.ReadToEnd();
        }
        public static async Task<Novel> GetNovel(string ncode)
        {
            string html = await GetHtml(String.Format("https://ncode.syosetu.com/{0}/", ncode));
            
            return new Novel(html);
        }
        public static async Task<NovelPage> GetNovelPage(string ncode, int page)
        {
            string html = await GetHtml(String.Format("https://ncode.syosetu.com/{0}/{1}/", ncode, page));
            return new NovelPage(html);
        }

        private static string DecompressGZIP(Stream stream)
        {
            string result = "";

            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream gzipStream = new GZipStream(stream, CompressionMode.Decompress))
                {
                    gzipStream.CopyTo(ms);
                }

                result = Encoding.UTF8.GetString(ms.ToArray());
            }

            return result;
        }

    }
}