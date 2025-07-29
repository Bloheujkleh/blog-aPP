using WebApplication7;
using WebApplication7.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRepository, Repository>();

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
startup.Configure(app, app.Environment);

// Admin kullanıcı ve rolü seed et
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedRolesAndAdminAsync(services);
}
app.UseAuthentication();
app.UseAuthorization();

app.Run();

// ===> Seed fonksiyonu aşağıda tanımlı <===
static async Task SeedRolesAndAdminAsync(IServiceProvider services)
{
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    string adminRole = "Admin";
    string adminEmail = "admin@site.com";
    string adminPassword = "Admin123!";

    // 1. Rol yoksa oluştur
    if (!await roleManager.RoleExistsAsync(adminRole))
    {
        await roleManager.CreateAsync(new IdentityRole(adminRole));
    }

    // 2. Kullanıcı varsa ekleme
    var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
    if (existingAdmin == null)
    {
        var newUser = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(newUser, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newUser, adminRole);
            Console.WriteLine("✅ Admin user created and assigned to Admin role.");
        }
        else
        {
            foreach (var err in result.Errors)
                Console.WriteLine($"❌ {err.Description}");
        }
    }
    else
    {
        Console.WriteLine("ℹ️ Admin user already exists.");
    }
}
