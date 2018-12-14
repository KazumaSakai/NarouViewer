using NarouViewer.API;
using System;
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
        private CheckBox[] checkBoxs;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="data"></param>
        /// <param name="model"></param>
        /// <param name="line"></param>
        public CheckBoxsPanel(string[] data, StringEventHandler controller, int line = 3)
        {
            this.checkBoxs = new CheckBox[data.Length];
            for (int i = 1; i < data.Length; i++)
            {
                int x = (i - 1) % line;
                int y = ((i - 1) - x) / line;
                this.Controls.Add(this.checkBoxs[i] = new CheckBox()
                {
                    Text = data[i],
                    Location = new Point(3 + (168 * x), 2 + (22 * y)),
                    Size = new Size(165, 22),
                    UseVisualStyleBackColor = true,
                    Font = new Font("MS UI Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 128)
                });

                string key = data[i];
                this.checkBoxs[i].Click += new EventHandler((object sender, EventArgs e) =>
                {
                    controller?.Invoke(key);
                });
            }

            this.Dock = DockStyle.Fill;
            this.Size = new Size(480, 6 + (22 * data.Length - 1));

            //  Controller
            this.controller = controller;
        }
    }
}
