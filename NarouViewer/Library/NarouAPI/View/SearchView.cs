using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;
using NarouViewer.API;

namespace NarouViewer
{
    public class SearchView : Panel, IUpdateView
    {
        //  Controller
        private SearchController controller;

        //  Child View
        private SearchParameterView searchParameterView;
        private NovelDataListView novelDataListView;

        public SearchView(SearchController controller = null)
        {
            this.controller = controller ?? new SearchController();
            this.controller.searchPrameterController.Search += new SearchPrameterController.NarouSearchEvent(Search);
            this.controller.novelDataContoller.TagClicked += new StringEventHandler(AddTag);
            this.controller.novelDataContoller.TitleLinkClicked += new StringEventHandler(TitleClicked);
            this.controller.novelDataContoller.WriterLinkClicked += new StringEventHandler(WriterClicked);
            this.controller.novelDataContoller.GenreLinkClicked += new IntEventHandler(GenreClicked);

            this.searchParameterView = new SearchParameterView(this.controller.searchPrameterController);
            this.novelDataListView = new NovelDataListView(null, controller.novelDataContoller);
            this.Search(searchParameterView.model);

            this.Controls.Add(searchParameterView);
            this.Controls.Add(novelDataListView);

            //  Parent
            Control parent =  this.Parent;
            EventHandler parentSizeChanged = new EventHandler((object sender, EventArgs e) =>
            {
                Size spaceSize = this.Parent.ClientSize - this.Size;
                this.Location = new Point(spaceSize.Width / 2, 3);
            });
            this.ParentChanged += new EventHandler((object sender, EventArgs e) =>
            {
                if(parent != null) parent.SizeChanged -= parentSizeChanged;
                this.Parent.SizeChanged += parentSizeChanged;
                parent = this.Parent;
            });

            this.BorderStyle = BorderStyle.FixedSingle;

            this.UpdateView();
        }

        private async void Search(NarouAPI.SearchParameter searchParameter)
        {
            novelDataListView.model = await NarouAPI.GetSearchData(searchParameter);
        }
        private void AddTag(string tag)
        {
            if (tag == "") return;
            searchParameterView.AddKeyword(tag);
        }
        private void TitleClicked(string ncode)
        {
            if (ncode == "") return;

            /*
            string html = await NarouAPI.GetNovel(ncode);
            List<(string title, string updateDay, string reUpdateDay)> list = AnalyzeNovelPage.Analyaze(html);

            foreach (var item in list)
            {
                Console.WriteLine("{0}\n{1}\n{2}\n", item.title, item.updateDay, item.reUpdateDay);
            }
            */
        }
        private void WriterClicked(string userid)
        {
            if (userid == "0") return;
            System.Diagnostics.Process.Start(String.Format("https://mypage.syosetu.com/{0}/", userid));
        }
        private void GenreClicked(int genre)
        {
            searchParameterView.SelectGenre(genre);
        }

        public void UpdateView()
        {
            int nowY = 0;
            searchParameterView.Location = new Point(0, nowY);
            nowY += searchParameterView.Height + 10;

            novelDataListView.Location = new Point(0, nowY);
            nowY += novelDataListView.Height;

            this.Size = new Size(Math.Max(searchParameterView.Width, novelDataListView.Width) + 2, nowY);
        }
    }
}
