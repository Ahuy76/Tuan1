using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrangSanPham.Data;
using TrangSanPham.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrangSanPham.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductsController : Controller // Kế thừa từ Controller thay vì ControllerBase
    {
        private readonly ProductContext _context;

        public ProductsController(ProductContext context)
        {
            _context = context;
        }

        // GET /products/all: trả về tất cả dữ liệu có trong bảng “product”
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET /products/{product_id}: trả về dữ liệu của record có id tương ứng trong bảng
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        // POST /products: tạo một record mới và lưu vào bảng “product”
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        // PUT /products/{product_id}: chỉnh sửa record có id tương ứng trong bảng
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE /products/{product_id}: xoá record có id tương ứng trong bảng
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET /products/display: trả về một trang web html tĩnh để hiển thị các data có trong bảng products
        [HttpGet("display")]
        public IActionResult GetProductsHtml()
        {
            var products = _context.Products.ToList(); // Lấy tất cả sản phẩm
            return View(products); // Trả về View với danh sách sản phẩm
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
