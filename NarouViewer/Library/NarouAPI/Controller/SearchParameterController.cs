using NarouViewer.API;

namespace NarouViewer
{
    public class SearchPrameterController
    {
        public delegate void NarouSearchEvent(NarouAPI.SearchParameter parameter);
        public NarouSearchEvent Search;
    }
}