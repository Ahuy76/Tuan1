using Microsoft.EntityFrameworkCore; // Thêm using cho EF Core
using TrangSanPham.Data;

var builder = WebApplication.CreateBuilder(args);

// Thêm chu?i k?t n?i trong file appsettings.json
// (B?n c?n ??m b?o r?ng b?n ?ã có chu?i k?t n?i nh? sau trong appsettings.json)
// "ConnectionStrings": {
//     "DefaultConnection": "Server=your_server;Database=your_database;User=your_user;Password=your_password;"
// }

builder.Services.AddControllersWithViews();

// C?u hình DbContext
builder.Services.AddDbContext<ProductContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 21)))); // ??i v?i MySQL

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
