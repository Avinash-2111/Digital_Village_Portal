using Microsoft.EntityFrameworkCore;
using MyVillageApp.Data;
using MyVillageApp.Data.Repositories;
using MyVillageApp.Services.Interfaces;
using MyVillageApp.Services.Users;
using MyVillageApp.Domain.Users;
using Microsoft.AspNetCore.Identity;

using MyVillageApp.Services.Gallery;
using Microsoft.AspNetCore.Http.Features;
using MyVillageApp.Services.VehicleTypes;
using MyVillageApp.Services.FamilyMembers;
using MyVillageApp.Services.Pets;
using MyVillageApp.Services.Families;


var builder = WebApplication.CreateBuilder(args);


// Add MVC
builder.Services.AddControllersWithViews();


// Database connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISchemeService, SchemeService>();
builder.Services.AddScoped<IProfessionService, ProfessionService>();
builder.Services.AddScoped<ILeaderService, LeaderService>();
builder.Services.AddScoped<IVillageGalleryService, VillageGalleryService>();
builder.Services.AddScoped<IPensionService, PensionService>();
builder.Services.AddScoped<IFamilyService, FamilyService>();
builder.Services.AddScoped<IVehicleTypeService, VehicleTypeService>();
builder.Services.AddScoped<IVehicleTypeService, VehicleTypeService>();
builder.Services.AddScoped<IFamilyMemberService, FamilyMemberService>();
builder.Services.AddScoped<IFamilyMemberMappingService, FamilyMemberMappingService>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<IFamilyPetService, FamilyPetService>();
builder.Services.AddScoped<IWishService, WishService>();
builder.Services.AddScoped<IWishMediaService, WishMediaService>();

builder.Services.AddScoped<ICelebrationService, CelebrationService>();
builder.Services.AddScoped<ICelebrationMediaService, CelebrationMediaService>();

builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
builder.Services.AddSession();

var app = builder.Build();


// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

// Default Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    

    await SeedAdminUserAsync(context);
}
app.Run();

static async Task SeedAdminUserAsync(ApplicationDbContext context)
{
    if (!await context.Users.AnyAsync(x => x.Role == "Admin"))
    {
        var adminUser = new User
        {
            UserName = "admin",
            Email = "admin@gmail.com",
            PhoneNumber = "9999999999",
            Gender = "Male",
            Age = 30,
            Role = "Admin",
            Password="admin@123"
        };

       
        context.Users.Add(adminUser);
        await context.SaveChangesAsync();
    }
}