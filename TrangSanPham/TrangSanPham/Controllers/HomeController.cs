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
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        // Constructor accepts DbContext
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context; // Store DbContext for database access
        }

        // Index action to get the list of products from the database
        public IActionResult Index()
        {
            // Retrieve all products from the product table
            var products = _context.Product.ToList(); // Use Product here

            // Pass the list of products to the view
            return View(products);
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
