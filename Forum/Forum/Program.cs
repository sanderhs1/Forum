using Forum.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Identity;
//using Forum.Areas.Identity.Data; Kommenter dette ut fordi at det failer

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ListingDbContextConnection") ?? throw new
    InvalidOperationException("Connection string 'ListingDbContextConnection' not found");

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ListingDbContext>();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();
builder.Services.AddSession();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
// Database
builder.Services.AddDbContext<ListingDbContext>(options => {
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:ListingDbContextConnection"]);
});

var loggerConfiguration = new LoggerConfiguration()
    .MinimumLevel.Information() // Levels : Trace < Information < Warning < Error < Fatal
    .WriteTo.File($"Logs/app_{DateTime.Now:yyyyMMdd_HHmmss}.log");

loggerConfiguration.Filter.ByExcluding(e => e.Properties.TryGetValue("SourceContext", out var value) && e.Level == LogEventLevel.Information &&
    e.MessageTemplate.Text.Contains("Executed DbCommand"));

var logger = loggerConfiguration.CreateLogger();
builder.Logging.AddSerilog(logger);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.UseAuthentication();

app.MapDefaultControllerRoute();

app.Run();
