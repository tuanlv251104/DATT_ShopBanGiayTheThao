using DataProcessing.Models;

namespace View.ViewModel
{
    public class ColorViewModel
    {
        public Color? color { get; set; }
        public IEnumerable<Color>? colors { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int RowsPerPage { get; set; }
    }
}
