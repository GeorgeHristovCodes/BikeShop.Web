using BikeShop.Web.Data;
using BikeShop.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.StaticFiles;



var builder = WebApplication.CreateBuilder(args);

// ⛓ Добавяне на базата данни
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Добавяне на Identity с роли и ApplicationUser
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

// ➕ MVC & Razor
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSession(); // ✅ Добавя Session услугата


var app = builder.Build();

// 🔐 Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
// Позволи статични файлове + поддръжка за .webp
var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".webp"] = "image/webp";

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});


app.UseRouting();
app.UseSession(); // ✅ Активира session middleware-а


app.UseAuthentication();
app.UseAuthorization();
app.UseStatusCodePagesWithRedirects("/Home/AccessDenied");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    // 3. Създай роля "Staff" ако не съществува
    if (!await roleManager.RoleExistsAsync("Staff"))
        await roleManager.CreateAsync(new IdentityRole("Staff"));


    // 1. Създай роля "User" ако не съществува
    if (!await roleManager.RoleExistsAsync("User"))
        await roleManager.CreateAsync(new IdentityRole("User"));

    // 2. Създай роля "Admin" ако не съществува
    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new IdentityRole("Admin"));

    // 3. Създай админ потребител, ако не съществува
    string adminEmail = "admin@bikeshop.com";
    string adminPassword = "Admin123!";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        var newAdmin = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(newAdmin, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newAdmin, "Admin");
        }
    }
    // 🔸 Създай staff потребител, ако не съществува
    string staffEmail = "staff@bikeshop.com";
    string staffPassword = "Staff123!";

    var staffUser = await userManager.FindByEmailAsync(staffEmail);
    if (staffUser == null)
    {
        var newStaff = new ApplicationUser
        {
            UserName = staffEmail,
            Email = staffEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(newStaff, staffPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newStaff, "Staff");
        }
    }

    // 🔹 Създай стандартен user, ако не съществува
    string userEmail = "user@bikeshop.com";
    string userPassword = "User123!";

    var normalUser = await userManager.FindByEmailAsync(userEmail);
    if (normalUser == null)
    {
        var newUser = new ApplicationUser
        {
            UserName = userEmail,
            Email = userEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(newUser, userPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newUser, "User");
        }
    }

}


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Bicycle}/{action=ForRent}/{id?}");

app.MapRazorPages();



app.Run();
