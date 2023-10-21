using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace YourApplicationNamespace
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add the necessary services here
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Configuring password
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;

                // Configuring lockout settings
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);

                // Each user requires a unique email
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Setting the idle timeout for the session
                options.Cookie.Name = ".AdventureWorks.Session"; // Setting the name of the session cookie
                options.Cookie.IsEssential = true; // Indicating whether the session cookie is essential
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the request pipeline here
        }
    }
}
