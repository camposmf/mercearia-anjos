using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api.Database;
using web_api.Models;

namespace web_api.Controllers
{
    [ApiController]
    [Route("shoppingCarts")]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ShoppingCartsController(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private async Task<List<ShoppingCartModel>> GetShoppingCartsByDefault() => await _context.ShoppingCarts.ToListAsync();

        [HttpGet]
        public async Task<ActionResult<List<ShoppingCartModel>>> GetShoppingCarts()
        {
            var shoppingCartsToReturn = await GetShoppingCartsByDefault();
            return Ok(shoppingCartsToReturn);
        }

        [HttpPost]
        public async Task<ActionResult<List<ShoppingCartModel>>> CreateShoppingCart(ShoppingCartModel shoppingCart)
        {
            _context.ShoppingCarts.Add(shoppingCart);
            await _context.SaveChangesAsync();

            var shoppingCartsToReturn = await GetShoppingCartsByDefault();
            return Ok(shoppingCartsToReturn);
        }

        [HttpPut]
        public async Task<ActionResult<List<ShoppingCartModel>>> UpdateShoppingCart(ShoppingCartModel shoppingCart)
        {
            var shoppingCartEntity = await _context.ShoppingCarts.FindAsync(shoppingCart.Id);

            if (shoppingCartEntity == null)
                return NotFound("Shopping Cart not found.");

            shoppingCartEntity.SaleId = shoppingCart.SaleId;
            shoppingCartEntity.ClientId = shoppingCart.ClientId;
            shoppingCartEntity.PurchaseId = shoppingCart.PurchaseId;

            var shoppingCartsToReturn = await GetShoppingCartsByDefault();
            return Ok(shoppingCartsToReturn);
        }

        [HttpDelete("{shoppingCartId}")]
        public async Task<ActionResult<List<ShoppingCartModel>>> DeleteShoppingCart(int shoppingCartId)
        {
            var shoppingCartEntity = await _context.ShoppingCarts.FindAsync(shoppingCartId);

            if (shoppingCartEntity == null)
                return NotFound("Shopping Cart not found.");

            _context.ShoppingCarts.Remove(shoppingCartEntity);
            await _context.SaveChangesAsync();

            var shoppingCartsToReturn = await GetShoppingCartsByDefault();
            return Ok(shoppingCartsToReturn);
        }
    }
}
