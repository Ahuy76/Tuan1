using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsAPI.Data;  // Ensure this namespace is added
using ProductsAPI.Models; // Ensure this namespace is added
using System.Diagnostics;
using System.Linq;

namespace TrangSanPham.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchQuery, int page = 1, int pageSize = 5)
        {
            // Truy vấn danh sách sản phẩm
            var products = from p in _context.Product
                           select p;

            // Nếu có từ khóa tìm kiếm, lọc danh sách sản phẩm
            if (!string.IsNullOrEmpty(searchQuery))
            {
                products = products.Where(p => p.Name.Contains(searchQuery) || p.Description.Contains(searchQuery));
                ViewBag.CurrentSearchQuery = searchQuery; // Giữ lại từ khóa tìm kiếm
            }

            // Phân trang
            var totalProducts = products.Count();
            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            var productsToDisplay = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Truyền thông tin phân trang vào ViewBag
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(productsToDisplay);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

