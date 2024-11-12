using View.IServices;
using View.Servicecs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using DataProcessing.Models;
using Microsoft.AspNetCore.Identity;
using API.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDistributedMemoryCache(); 
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  
    options.Cookie.HttpOnly = true;  
    options.Cookie.IsEssential = true; 
});
builder.Services.AddAuthorizationCore();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<IProductServices, ProductServices>();
builder.Services.AddHttpClient<ISoleServices, SoleServices>();
builder.Services.AddHttpClient<IBrandServices, BrandServices>();
builder.Services.AddHttpClient<ICartServices, CartService>();
builder.Services.AddHttpClient<ICategoryServices, CategoryServices>();
builder.Services.AddHttpClient<IMaterialServices, MaterialServices>();
builder.Services.AddHttpClient<IPromotionServices, PromotionServices>();
builder.Services.AddHttpClient<IVoucherServices, VoucherServices>();
builder.Services.AddHttpClient<ISizeServices, SizeServices>();
builder.Services.AddHttpClient<IColorServices, ColorServices>();
builder.Services.AddHttpClient<IImageServices, ImageServices>();
builder.Services.AddHttpClient<ISelectedImageServices, SelectedImageServices>();
builder.Services.AddHttpClient<IAccountService, AccountService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddHttpClient<IProductDetailService, ProductDetailService>();
builder.Services.AddHttpClient<IShippingUnitServices,ShippingUnitServices>();
builder.Services.AddHttpClient<IProductDetailPromotionServices, ProductDetailPromotionServices>();
builder.Services.AddHttpClient<IOrderServices, OrderServices>();
builder.Services.AddHttpClient<IAddressService,AddressServices>();
builder.Services.AddHttpContextAccessor();

//
builder.Services.AddAuthentication(options =>
{
    // Đặt cookie cho giao diện web
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Mặc định cho MVC
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = "/Account/Login"; // Trang đăng nhập nếu người dùng chưa đăng nhập
    options.LogoutPath = "/Account/Logout"; // Trang đăng xuất
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Thời gian tồn tại của cookie
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),
        RoleClaimType = ClaimTypes.Role,
        ClockSkew = TimeSpan.Zero
    };
});



//
builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMVC",
    builder =>
    {
        builder.WithOrigins("https://localhost:7075")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//app.UseMiddleware<JwtToken>();


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowMVC");
app.UseRouting();
app.UseSession(); 
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}");

app.Run();
