using GiftShopBUSINESS.Extensions;
using GiftShopBUSINESS.Mapper.ProductMappers;
using GiftShopBUSINESS.Services.Abstractions;
using GiftShopBUSINESS.Services.Interfaces;
using GiftShopCORE.Identity;
using GiftShopDAL.DB;
using GiftShopDAL.Extensions;
using GiftShopDAL.Repositories.Abstractions;
using GiftShopDAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Dependency injection
builder.Services.AddDALServices();
builder.Services.AddBusinessServices();

// Add services to the container.
builder.Services.AddControllersWithViews();

///Database
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});


///UserIdentity
builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.Password.RequireUppercase = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=DashBoard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
