using Microsoft.EntityFrameworkCore;
using web_api.Models;

namespace web_api.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> option)
            : base(option) { }

        public DbSet<UserModel> Users => Set<UserModel>();
        public DbSet<ClientModel> Clients => Set<ClientModel>();
        public DbSet<ProductModel> Products => Set<ProductModel>();
        public DbSet<PurchaseModel> Purchases => Set<PurchaseModel>();
        public DbSet<EmployerModel> Employers => Set<EmployerModel>();
        public DbSet<SaleModel> Sales => Set<SaleModel>();
        public DbSet<ShoppingCartModel> ShoppingCarts => Set<ShoppingCartModel>();
    }
}
