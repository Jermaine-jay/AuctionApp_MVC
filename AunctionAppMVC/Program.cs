using AunctionApp.BLL.Extensions;
using AunctionApp.DAL.Database;
using AunctionAppMVC.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TodoList.DAL.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AunctionAppDbContext>(opts =>
{
    var defaultConn = builder.Configuration.GetSection("ConnectionString")["DefaultConn"];
    opts.UseSqlServer(defaultConn);
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
if (!app.Environment.IsDevelopment())
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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    ServiceExtensions.Configure(services);
}

await DataAccess.EnsurePopulatedAsync(app);
await SeedUsers.EnsurePopulatedAsync(app);


app.Run();
