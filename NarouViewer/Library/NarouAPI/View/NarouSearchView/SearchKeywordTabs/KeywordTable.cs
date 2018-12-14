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
    public class KeywordsTable : TableLayoutPanel
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

        public KeywordsTable(string[][] words, NarouAPI.SearchParameter model, int line = 3)
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

            WordCheckBox[] wordCheckBox;

            public WordCheckBoxsPanel(string[] words, NarouAPI.SearchParameter model, int line)
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

                public WordCheckBox(string word, NarouAPI.SearchParameter model)
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
                    if (!model.searchKeywordList.Contains(word))
                    {
                        model.searchKeywordList.Add(word);
                    }
                }
                private void RemoveWord(string word)
                {
                    model.searchKeywordList.Remove(word);
                }
            }
        }
    }
}
