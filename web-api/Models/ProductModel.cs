namespace web_api.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public double Price { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
    }
}
