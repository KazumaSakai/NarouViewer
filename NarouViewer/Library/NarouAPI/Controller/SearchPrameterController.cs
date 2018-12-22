using NarouViewer.API;

namespace NarouViewer
{
    public class SearchPrameterController
    {
        public delegate void NarouSearchEvent(SearchParameter parameter);
        public NarouSearchEvent Search;
    }
}