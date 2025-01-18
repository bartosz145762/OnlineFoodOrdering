using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineMealOrdering.Data;
using OnlineMealOrdering.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSession();
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>();


var app = builder.Build();

// �rodowisko
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();


app.UseSession(); 

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();


    if (!dbContext.MenuItems.Any())
    {
        dbContext.MenuItems.AddRange(
            new MenuItem { Name = "Pizza Margherita", Description = "Klasyczna włoska pizza z serem i pomidorami", Price = 25.99m },
            new MenuItem { Name = "Spaghetti Bolognese", Description = "Makaron z sosem pomidorowym i mięsem mielonym", Price = 22.50m },
            new MenuItem { Name = "Sałatka Cezar", Description = "Świeża sałatka z kurczakiem i dressingiem Cezar", Price = 18.99m },
            new MenuItem { Name = "Zupa Pomidorowa", Description = "Domowa zupa pomidorowa z makaronem", Price = 10.50m },
            new MenuItem { Name = "Burger Wołowy", Description = "Soczysty burger z wołowiny, serem i warzywami", Price = 19.99m },
            new MenuItem { Name = "Frytki", Description = "Złociste i chrupiące frytki z solą", Price = 7.50m },
            new MenuItem { Name = "Deser Tiramisu", Description = "Tradycyjny włoski deser na bazie kawy", Price = 12.99m },
            new MenuItem { Name = "Sok Pomarańczowy", Description = "Świeżo wyciskany sok z pomarańczy", Price = 8.99m }
        );
        dbContext.SaveChanges();
    }
}

app.Run();
