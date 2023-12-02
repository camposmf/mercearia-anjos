namespace web_api.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public required int Age { get; set; } = 0;
        public required string Name { get; set; } = string.Empty;
        public required string City { get; set; } = string.Empty;
    }
}
