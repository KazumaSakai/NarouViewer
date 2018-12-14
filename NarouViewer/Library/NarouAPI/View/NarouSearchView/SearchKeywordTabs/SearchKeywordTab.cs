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
    public class SearchKeywordTab : TabPage, IRequestSize
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

        public Size requestSize { get; set; }

        public SearchKeywordTab(NarouAPI.SearchParameter model, string tabText, (string[][] words, string title, int line)[] data)
        {
            this.DoubleBuffered = true;
            this.Text = tabText;
            this.UseVisualStyleBackColor = true;

            Label[] titles = new Label[data.Length];

            int nowY = 15;
            for (int i = 0; i < data.Length; i++)
            {
                //  Title Line
                Label title = new DefaultLabel(data[i].title, "title", new Point(11, nowY), true);
                nowY += title.Height + 3;

                //  Table Line
                TableLayoutPanel table = new KeywordsTable(data[i].words, model, data[i].line) { Location = new Point(8, nowY) };
                nowY += table.Height + 15;

                this.Controls.Add(title);
                this.Controls.Add(table);
            }

            this.requestSize = new Size(682, nowY);

            //  Model
            this.model = model;
        }
    }
}
