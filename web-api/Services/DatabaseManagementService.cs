using web_api.Database;
using Microsoft.EntityFrameworkCore;

namespace web_api.Services
{
    public static class DatabaseManagementService
    {
        public static void MigrationInitialisation(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var serviceDb = serviceScope.ServiceProvider.GetService<DatabaseContext>();
                serviceDb?.Database.Migrate();
            }
        }
    }
}
