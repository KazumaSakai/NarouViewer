using NarouViewer.API;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NarouViewer
{
    public class CheckBoxsTablesTabPage : TabPage, IRequestSize
    {
        #region --- Controller ---
        private StringEventHandler controller;
        #endregion

        #region --- IRequestSize ---
        public Size RequestSize { get; set; }
        #endregion

        #region --- 子コントロール ---
        private Label[] titles;
        private CheckBoxsTable[] tables;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="model"></param>
        public CheckBoxsTablesTabPage((string[][] word, string title, int line)[] data, string text,  StringEventHandler controller)
        {
            //  Init
            this.DoubleBuffered = true;
            this.Text = text;

            //  Table Line
            this.titles = new Label[data.Length];
            this.tables = new CheckBoxsTable[data.Length];
            int nowY = 15;
            for (int i = 0; i < data.Length; i++)
            {
                Label title = new DefaultLabel(data[i].title, "title", new Point(11, nowY), true);
                nowY += title.Height + 3;
                CheckBoxsTable table = new CheckBoxsTable(data[i].word, controller, data[i].line) { Location = new Point(8, nowY) };
                nowY += table.Height + 15;

                this.titles[i] = title;
                this.tables[i] = table;

                this.Controls.Add(title);
                this.Controls.Add(table);
            }

            //  Size
            this.RequestSize = new Size(682, nowY);
            this.Size = RequestSize;

            //  Controller
            this.controller = controller;
        }
    }
}
