using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api.Database;
using web_api.Models;

namespace web_api.Controllers
{
    [ApiController]
    [Route("sales")]
    public class SalesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public SalesController(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private async Task<List<SaleModel>> GetSalesByDefault() => await _context.Sales.ToListAsync();

        [HttpGet]
        public async Task<ActionResult<List<SaleModel>>> GetSales()
        {
            var salesToReturn = await GetSalesByDefault();
            return Ok(salesToReturn);
        }

        [HttpPost]
        public async Task<ActionResult<List<SaleModel>>> CreateSale(SaleModel sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            var salesToReturn = await GetSalesByDefault();
            return Ok(salesToReturn);
        }

        [HttpPut]
        public async Task<ActionResult<List<SaleModel>>> UpdateSale(SaleModel sale)
        {
            var saleEntity = await _context.Sales.FindAsync(sale.Id);

            if (saleEntity == null)
                return NotFound("Sales not found.");

            saleEntity.ClientId = sale.ClientId;
            saleEntity.EmployeeId = sale.EmployeeId;
            saleEntity.ProductId = sale.ProductId;
            saleEntity.SaleDate = sale.SaleDate;

            await _context.SaveChangesAsync();
            var salesToReturn = await GetSalesByDefault(); 

            return Ok(salesToReturn);
        }

        [HttpDelete("{saleId}")]
        public async Task<ActionResult<List<SaleModel>>> DeleteSale(int saleId)
        {
            var saleEntity = await _context.Sales.FindAsync(saleId);

            if (saleEntity == null)
                return NotFound("Sales not found.");

            _context.Sales.Remove(saleEntity);
            await _context.SaveChangesAsync();

            var salesToReturn = await GetSalesByDefault(); 
            return Ok(salesToReturn);
        }
    }
}