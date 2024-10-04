using Microsoft.EntityFrameworkCore;
using TrangSanPham.Models;

namespace TrangSanPham.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
