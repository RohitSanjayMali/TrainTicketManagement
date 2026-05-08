using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrainTicketManagement.Data;
using TrainTicketManagement.Services;

var builder = WebApplication.CreateBuilder(args);


// ========================================
// MVC
// ========================================
builder.Services.AddControllersWithViews();


// ========================================
// DATABASE
// ========================================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


// ========================================
// IDENTITY
// ========================================
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit           = false;
    options.Password.RequireLowercase       = false;
    options.Password.RequireUppercase       = false;
    options.Password.RequiredLength         = 6;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>();


// ========================================
// COOKIE SETTINGS
// ========================================
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath         = "/Auth/Login";
    options.AccessDeniedPath  = "/Auth/AccessDenied";
    options.ExpireTimeSpan    = TimeSpan.FromDays(7);
    options.SlidingExpiration = true;
});


// ========================================
// SERVICES
// ========================================
builder.Services.AddScoped<ITrainService,   TrainService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IPdfService,     PdfService>();


var app = builder.Build();


// ========================================
// SEEDING — FIXED: cleaned up, no duplicate admin creation
// ========================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var config   = scope.ServiceProvider.GetRequiredService<IConfiguration>();

    await SeedData.Initialize(services);

    // FIXED: AdminSeeder now reads credentials from appsettings.json
    // No more hardcoded emails/passwords in code
    await AdminSeeder.SeedAdmin(services, config);
}


// ========================================
// MIDDLEWARE
// ========================================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


// ========================================
// ROUTING
// ========================================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
