using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataProcessing.Models;

namespace View.Data
{
    public class ViewContext : DbContext
    {
        public ViewContext (DbContextOptions<ViewContext> options)
            : base(options)
        {
        }

        public DbSet<DataProcessing.Models.Sole> Sole { get; set; } = default!;

        public DbSet<DataProcessing.Models.Product>? Product { get; set; }

        public DbSet<DataProcessing.Models.Color>? Color { get; set; }

        public DbSet<DataProcessing.Models.Size>? Size { get; set; }

        public DbSet<DataProcessing.Models.Category>? Category { get; set; }

        public DbSet<DataProcessing.Models.Brand>? Brand { get; set; }

        public DbSet<DataProcessing.Models.Material>? Material { get; set; }
        public DbSet<DataProcessing.Models.Promotion>? Promotion {  get; set; }

        public DbSet<DataProcessing.Models.ShippingUnit>? shippingUnits { get; set; }
        public DbSet<DataProcessing.Models.ProductDetail>? ProductDetail { get; set; }
        public DbSet<DataProcessing.Models.Voucher>? Voucher { get; set; }

    }
}
