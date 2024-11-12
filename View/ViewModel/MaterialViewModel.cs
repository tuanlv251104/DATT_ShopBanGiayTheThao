using DataProcessing.Models;
using Microsoft.AspNetCore.Mvc;

namespace View.ViewModel
{
    public class MaterialViewModel
    {
        public Material NewMaterial { get; set; }
        public IEnumerable<Material> Materials { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int RowsPerPage { get; set; }
    }
}
