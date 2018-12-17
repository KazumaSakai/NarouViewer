using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;
using NarouViewer.API;
using System.Text.RegularExpressions;

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

        private void Search(NarouAPI.SearchParameter searchParameter)
        {
            Task.Run(() =>
            {
                novelDataListView.model = NarouAPI.GetSearchData(searchParameter).Result;
            });
        }
        private void AddTag(string tag)
        {
            if (tag == "") return;
            searchParameterView.AddKeyword(tag);
        }
        private void TitleClicked(string ncode)
        {
            if (ncode == "") return;
            System.Diagnostics.Process.Start(String.Format("https://ncode.syosetu.com/{0}/", ncode));
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
