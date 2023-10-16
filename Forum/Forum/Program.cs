using Forum.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Identity;
//using Forum.Areas.Identity.Data; Kommenter dette ut fordi at det failer

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ListingDbContextConnection");builder.Services.AddDbContext<ListingDbContext>(options =>
    options.UseSqlServer(connectionString));

// Når scaffolding ble lagt til kom det en til listingdbcontext i et annet folder som mest sannsynlig lager feil vet ikke hvorfor den er der eller om den skal være der

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ListingDbContext>();
// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthentication();

app.MapDefaultControllerRoute();

app.Run();
