using Microsoft.EntityFrameworkCore;
//using SacnSystemServer.Data;
using SacnSystemServer.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<ApplicationDBContext>(option =>
//    // option.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
//    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddSignalR();

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
app.MapHub<UserHub>("/hubs/User");
app.Run();
