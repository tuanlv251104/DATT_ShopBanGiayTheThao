using DataProcessing.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class Product_Search_Filter_ViewModel
    {
            public Guid? SelectedColorId { get; set; }
            public Guid? SelectedCategoryId { get; set; }
            public Guid? SelectedBrandId { get; set; }
            public Guid? SelectedSoleId { get; set; }
            public Guid? SelectedSizeId { get; set; }

            // Dropdowns
            public List<Color> Colors { get; set; } = new List<Color>();
            public List<Category> Categories { get; set; } = new List<Category>();
            public List<Brand> Brands { get; set; } = new List<Brand>();
            public List<Sole> Soles { get; set; } = new List<Sole>();
            public List<Size> Sizes { get; set; } = new List<Size>();

            // List of product details
            public List<ProductDetailsViewModel> ProductDetails { get; set; } = new List<ProductDetailsViewModel>();
        }

    }
    public class ProductDetailsViewModel
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string ColorName { get; set; }
        public string BrandName { get; set; }
        public string MaterialName { get; set; }
        public string CategoryName { get; set; }
        public string SoleTypeName { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public float Weight { get; set; }
    }
