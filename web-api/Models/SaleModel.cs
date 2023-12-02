using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
namespace web_api.Models
{
    public class SaleModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int EmployeeId { get; set; }
        public int ProductId { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
