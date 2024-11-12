using DataProcessing.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace View.ViewModel
{
    public class CreateAddressViewModel
    {
        public Address Address { get; set; } = new Address();
        public IEnumerable<SelectListItem> Provinces { get; set; }
        public IEnumerable<SelectListItem> Districts { get; set; }
        public IEnumerable<SelectListItem> Wards { get; set; }
    }
}
