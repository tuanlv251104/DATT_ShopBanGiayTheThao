using Data.Models;
using DataProcessing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }
        
            
        public DbSet<ApplicationUser> Accounts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }
        public DbSet<PaymentHistory> PaymentHistories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Sole> Soles { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<SelectedImage> SelectedImages { get; set; }
        public DbSet<ProductDetailPromotion> ProductDetailPromotions { get; set; }
        public DbSet<ShippingUnit> ShippingUnits { get; set; }
        public DbSet<OrderAdress> OrderAdresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           //optionsBuilder.UseSqlServer("Data Source=THANHTONG\\SQLEXPRESS01;Initial Catalog=DUANTHUCTAP;Integrated Security=True;Trust Server Certificate=True");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentHistory>()
                .HasOne(ph => ph.Order)
                .WithMany(o => o.paymentHistories)
                .HasForeignKey(ph => ph.OrderId);

            // Khai báo Entity cho ProductDetailPromotion
            modelBuilder.Entity<ProductDetailPromotion>()
                .HasKey(pd => pd.Id); // Khóa chính là Id

            // Khóa ngoại cho ProductDetail
            modelBuilder.Entity<ProductDetailPromotion>()
                .HasOne(pd => pd.ProductDetail)
                .WithMany(p => p.ProductDetailPromotions)
                .HasForeignKey(pd => pd.ProductDetailId)
                .OnDelete(DeleteBehavior.Cascade); // Thay đổi hành vi khi xóa (nếu cần)

            // Khóa ngoại cho Promotion
            modelBuilder.Entity<ProductDetailPromotion>()
                .HasOne(pd => pd.Promotion)
                .WithMany(p => p.ProductDetailPromotions)
                .HasForeignKey(pd => pd.PromotionId)
                .OnDelete(DeleteBehavior.Cascade); // Thay đổi hành vi khi xóa (nếu cần)

            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Order>()
            //    .HasOne(o => o.Address)
            //    .WithMany()
            //    .HasForeignKey(o => o.AddressId)
            //    .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Address>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.AccountId)
                .OnDelete(DeleteBehavior.Restrict);
            //SeedData for Account
           var adminRoleId = Guid.NewGuid();
           var customerRoleId = Guid.NewGuid();
            var employeeRoleId = Guid.NewGuid();
            var guestRoleId = Guid.NewGuid();

            modelBuilder.Entity<IdentityRole<Guid>>().HasData
                (
                    new IdentityRole<Guid>
                    {
                        Id = adminRoleId,
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    }, new IdentityRole<Guid>
                    {
                        Id = customerRoleId,
                        Name = "Customer",
                        NormalizedName = "CUSTOMER"
                    }, new IdentityRole<Guid>
                    {
                        Id = employeeRoleId,
                        Name = "Employee",
                        NormalizedName = "EMPLOYEE"
                    }, new IdentityRole<Guid>
                    {
                        Id = guestRoleId,
                        Name = "Guest",
                        NormalizedName = "GUEST"
                    }
                );
            var adminUserId = Guid.NewGuid();
            var CustomerUserId = Guid.NewGuid();
            var EmployeeUserId = Guid.NewGuid();
            var GuestUserId = Guid.NewGuid();

            var passwordHasher = new PasswordHasher<ApplicationUser>();

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = adminUserId,
                    UserName = "admin@example.com",
                    NormalizedUserName = "ADMIN@EXAMPLE.COM",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    Name = "Admin User",
                    CIC = "002204004364",
                    PhoneNumber = "0123456789",
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    PasswordHash = passwordHasher.HashPassword(null, "AdminPass123!"),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser
                {
                    Id = CustomerUserId,
                    UserName = "user@example.com",
                    NormalizedUserName = "USER@EXAMPLE.COM",
                    Email = "user@example.com",
                    NormalizedEmail = "USER@EXAMPLE.COM",
                    Name = "Regular User",
                    CIC = "004204004364",

                    PhoneNumber = "0987654321",
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    PasswordHash = passwordHasher.HashPassword(null, "UserPass123!"),
                    SecurityStamp = Guid.NewGuid().ToString()
                }

            );

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                },
                new IdentityUserRole<Guid>
                {
                    UserId = CustomerUserId,
                    RoleId = customerRoleId
                }
            );
        }



    }
}
