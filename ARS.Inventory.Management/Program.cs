using ARS.Inventory.Management.Application.Factories;
using ARS.Inventory.Management.Application.Services;
using ARS.Inventory.Management.Data;
using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Infrastructure.Interfaces;
using ARS.Inventory.Management.Infrastructure.Repository.Context;
using ARS.Inventory.Management.Infrastructure.Repository.Infrastucture;
using ARS.Inventory.Management.Web.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddFilter("Microsoft", LogLevel.Warning)
        .AddFilter("System", LogLevel.Warning)
        .AddFilter("NonHostConsoleApp.Program", LogLevel.Debug)
        .AddConsole();
});
ILogger logger = loggerFactory.CreateLogger<Program>();

builder.Services.AddSingleton<ILogger>(logger);


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddOptions();
builder.Services.AddDbContext<InventoryDbContext>(options =>
        options.UseSqlServer(
            connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//AutoMapper Setup
builder.Services.AddAutoMapper(
        typeof(AutoMapperProfile));

builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
builder.Services.AddWebOptimizer(pipeline =>
{
    pipeline.MinifyJsFiles("~/Scripts/*.js", "~/Scripts/**/*.js");
    pipeline.MinifyCssFiles("~/Content/**/*.css");
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserService, UserService>();

//builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AdditionalUserClaimsPrincipalFactory>();
builder.Services.AddDbContext<InventoryDbContext>();
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<InventoryDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();


}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UserManagement}/{action=Login}/{id?}");
app.MapRazorPages();
app.Run();
