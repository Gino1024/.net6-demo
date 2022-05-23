using Microsoft.EntityFrameworkCore;

namespace DotNet6.WebAPI.Demo
{
    public static class MigrationExtension
    {
        public static IApplicationBuilder MigrateDatabase<T>(this IApplicationBuilder webHost) where T : DbContext
        {
            using (var scope = webHost.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var db = services.GetRequiredService<T>();
                    db.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }
            return webHost;
        }
    }
}
