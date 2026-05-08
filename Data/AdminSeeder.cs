using Microsoft.AspNetCore.Identity;

namespace TrainTicketManagement.Data
{
    public static class AdminSeeder
    {
        // FIXED: No more hardcoded personal email/password
        // Credentials now come from appsettings.json → AdminSettings section
        public static async Task SeedAdmin(IServiceProvider serviceProvider, IConfiguration config)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string adminEmail    = config["AdminSettings:Email"]    ?? "admin@trainticket.com";
            string adminPassword = config["AdminSettings:Password"] ?? "Admin@123";

            // Ensure roles exist
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await roleManager.RoleExistsAsync("User"))
                await roleManager.CreateAsync(new IdentityRole("User"));

            // Create admin user if not present
            var existing = await userManager.FindByEmailAsync(adminEmail);
            if (existing == null)
            {
                var adminUser = new IdentityUser
                {
                    UserName       = adminEmail,
                    Email          = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                else
                    foreach (var err in result.Errors)
                        Console.WriteLine($"[AdminSeeder] {err.Description}");
            }
        }
    }
}
