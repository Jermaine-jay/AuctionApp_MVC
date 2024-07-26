using AunctionApp.BLL.Extensions;
using AunctionApp.DAL.Database;
using AunctionAppMVC.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AunctionApp.DAL.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AunctionAppDbContext>(opts =>
{
    var defaultConn = builder.Configuration.GetConnectionString("DefaultConnection");
    opts.UseNpgsql(defaultConn);
});

builder.Services.Configure<EmailSenderOptions>(builder.Configuration.GetSection("EmailSenderOptions"));

builder.Services.RegisterServices();
builder.Services.ConfigureIdentity();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork<AunctionAppDbContext>>();
builder.Services.AddAutoMapper(Assembly.Load("AunctionApp.BLL"));

builder.Services.AddControllersWithViews();
// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment() || !app.Environment.IsProduction() || !app.Environment.IsStaging())
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
    pattern: "{controller=Home}/{action=Index}/{id?}");


//await app.SeedRole();
//await app.EnsurePopulatedUsersAsync();
//await app.EnsurePopulatedAsync();

app.Run();
