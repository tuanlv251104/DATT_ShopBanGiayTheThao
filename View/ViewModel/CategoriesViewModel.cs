using DataProcessing.Models;

namespace View.ViewModel
{
    public class CategoriesViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set;}
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int RowsPerPage { get; set; }
    }
}
