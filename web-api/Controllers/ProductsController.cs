using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api.Database;
using web_api.Models;

namespace web_api.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ProductsController(DatabaseContext context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductModel>>> GetProducts()
        {
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<ProductModel>>> CreateProduct(ProductModel product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<ProductModel>>> UpdateProduct(ProductModel product)
        {
            var productEntity = await _context.Products.FindAsync(product);

            if (productEntity == null)
                return NotFound("Product not found.");

            productEntity.Name = product.Name;
            productEntity.Price = product.Price;

            await _context.SaveChangesAsync();
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpDelete("productId")]
        public async Task<ActionResult<List<ProductModel>>> DeleteProduct(int productId)
        {
            var productEntity = await _context.Products.FindAsync(productId);

            if (productEntity == null)
                return NotFound("Product not found.");

            _context.Products.Remove(productEntity);
            await _context.SaveChangesAsync();
            return Ok(await _context.Products.ToListAsync());
        }
    }
}
