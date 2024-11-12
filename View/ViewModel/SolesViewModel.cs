using DataProcessing.Models;

namespace View.ViewModel
{
    public class SolesViewModel
    {
        public Sole sole { get; set; }
        public IEnumerable<Sole> soles { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int RowsPerPage { get; set; }
    }
}
