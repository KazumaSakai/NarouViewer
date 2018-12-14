using NarouViewer.API;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NarouViewer
{
    public class CheckBoxsTable : TableLayoutPanel
    {
        #region --- Controller ---
        private StringEventHandler controller;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="model"></param>
        /// <param name="line"></param>
        public CheckBoxsTable(string[][] data, StringEventHandler controller, int line = 2)
        {
            this.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            this.ColumnCount = 2;
            this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));
            this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 76F));

            this.DoubleBuffered = true;
            this.Location = new Point(8, 26);
            this.RowCount = data.Length;

            int totalHeight = 0;
            for (int i = 0; i < data.Length; i++)
            {
                string[] lineData = data[i];

                int height = 8 + (22 * (int)Math.Ceiling((lineData.Length - 1) / (double)line));
                totalHeight += (height + 1);

                this.RowStyles.Add(new RowStyle(SizeType.Absolute, height));
                Label label = new DefaultLabel(lineData[0], lineData[0], new Point(3, 3), true);
                label.Padding = new Padding(3);
                this.Controls.Add(label, 0, i);

                this.Controls.Add(new CheckBoxsPanel(lineData, controller, line), 1, i);
            }

            this.Size = new Size(663, 1 + totalHeight);

            //  Controller
            this.controller = controller;
        }

    }
}
