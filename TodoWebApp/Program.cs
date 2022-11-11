using Microsoft.EntityFrameworkCore;
using TodoWebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connection = builder.Configuration.GetConnectionString("LocaldbConnection");
ArgumentNullException.ThrowIfNullOrEmpty(connection);
builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlServer(connection));
builder.Logging.AddAzureWebAppDiagnostics();
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
    pattern: "{controller=Todos}/{action=Index}/{id?}");

app.Run();
