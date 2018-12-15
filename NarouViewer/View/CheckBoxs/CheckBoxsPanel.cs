using NarouViewer.API;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NarouViewer
{
    public class CheckBoxsPanel : Panel
    {
        #region --- Controller ---
        public StringEventHandler controller { get; set; }
        #endregion

        #region --- Child Control ---
        public List<CheckBox> checkBoxs = new List<CheckBox>();
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="data"></param>
        /// <param name="model"></param>
        /// <param name="line"></param>
        public CheckBoxsPanel(string[] data, StringEventHandler controller, int line = 3)
        {
            for (int i = 1; i < data.Length; i++)
            {
                int x = (i - 1) % line;
                int y = ((i - 1) - x) / line;

                CheckBox checkBox = new CheckBox()
                {
                    Text = data[i],
                    Location = new Point(3 + (168 * x), 2 + (22 * y)),
                    Size = new Size(165, 22),
                    UseVisualStyleBackColor = true,
                    Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128)
                };

                string key = data[i];
                checkBox.Click += new EventHandler((object sender, EventArgs e) =>
                {
                    controller?.Invoke(key);
                });

                this.Controls.Add(checkBox);
                this.checkBoxs.Add(checkBox);
            }

            this.Dock = DockStyle.Fill;
            this.Size = new Size(480, 6 + (22 * data.Length - 1));

            //  Controller
            this.controller = controller;
        }
    }
}
