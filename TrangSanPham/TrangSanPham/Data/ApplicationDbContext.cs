using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models;

namespace ProductsAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet<Product> đại diện cho bảng "product" trong cơ sở dữ liệu
        public DbSet<Product> Product { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình bảng Product
            modelBuilder.Entity<Product>()
                .ToTable("product") // Chỉ định tên bảng là "product"
                .HasIndex(e => e.Code).IsUnique(); // Đảm bảo mã sản phẩm là duy nhất

            // Cấu hình mặc định cho cột CreatedAt
            modelBuilder.Entity<Product>().Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Cấu hình mặc định cho cột UpdatedAt
            modelBuilder.Entity<Product>().Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Nếu cần có thêm cấu hình khác cho bảng Product, thêm vào đây
        }

        // Nếu có các DbSet khác (ví dụ: Category, Brand), hãy khai báo chúng tương tự như DbSet<Product> 
        // public DbSet<Category> Categories { get; set; }
    }
}
