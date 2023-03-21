using AunctionApp.BLL.Implementations;
using AunctionApp.BLL.Interfaces;
using AunctionApp.DAL.Database;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TodoList.DAL.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AunctionAppDbContext>(opts =>
{
    // this will only work if there's a section called ConnectionStrings on the appSettings
    // var defaultConn = builder.Configuration.GetConnectionString("DefaultConn");

    var defaultConn = builder.Configuration.GetSection("ConnectionString")["DefaultConn"];

    opts.UseSqlServer(defaultConn);

});

builder.Services.AddControllersWithViews();
// Add services to the container.

builder.Services.AddScoped<IUnitOfWork, UnitOfWork<AunctionAppDbContext>>();
builder.Services.AddScoped<IUserService, UserService>();//todo: show other life-cycles
builder.Services.AddScoped<IProductService, ProductService>();//todo: show other life-cycles
builder.Services.AddScoped<IAdminService, AdminService>();//todo: show other life-cycles
builder.Services.AddAutoMapper(Assembly.Load("AunctionApp.BLL"));

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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
