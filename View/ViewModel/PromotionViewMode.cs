using API.DTO;
using Data.Models;

namespace View.ViewModel
{
    public class PromotionViewMode
    {

           public List<Guid> SelectedProductIds { get; set; }
           public List<ProductDetailDTO> ProductVariants { get; set; }


    }
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    


}
