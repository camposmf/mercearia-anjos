using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api.Database;
using web_api.Models;

namespace web_api.Controllers
{
    [ApiController]
    [Route("purchases")]
    public class PurchasesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public PurchasesController(DatabaseContext context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async Task<ActionResult<List<PurchaseModel>>> GetPurchases()
        {
            return Ok(await _context.Purchases.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<PurchaseModel>>> CreatePurchase(PurchaseModel purchase)
        {
            // Todo -> Verificar as chaves estrangeiras
            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();
            return Ok(await _context.Purchases.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<PurchaseModel>>> UpdatePurchase(PurchaseModel purchase)
        {
            var purchaseEntity = await _context.Purchases.FindAsync(purchase.Id);

            if (purchaseEntity == null)
                return NotFound("Purchase not found.");

            purchaseEntity.ClientId = purchase.ClientId;
            purchaseEntity.ProductId = purchase.ProductId;

            await _context.SaveChangesAsync();
            return Ok(await _context.Purchases.ToListAsync());
        }

        [HttpDelete("{purchaseId}")]
        public async Task<ActionResult<List<UserModel>>> DeletePurchase(int purchaseId)
        {
            var purchaseEntity = await _context.Purchases.FindAsync(purchaseId);

            if (purchaseEntity == null)
                return NotFound("Purchase not found");

            _context.Purchases.Remove(purchaseEntity);
            await _context.SaveChangesAsync();

            return Ok(await _context.Purchases.ToListAsync());
        }
    }
}
