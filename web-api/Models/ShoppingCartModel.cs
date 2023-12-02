using System;

namespace web_api.Models
{
    public class ShoppingCartModel
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int ClientId { get; set; }
        public int PurchaseId { get; set; }
    }
}
