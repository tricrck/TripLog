using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TripLog.Models;

using System.Linq;
using Microsoft.AspNetCore.Http;


namespace TripLog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            string conStr = this.Configuration.GetConnectionString("TripContext");
            if (conStr != null)
            {
                services.AddDbContext<TripContext>(options => options.UseSqlServer(conStr));
            }
        }

        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // map route for Admin area 
                endpoints.MapAreaControllerRoute(
                    name: "admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

                // map default route
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Trip}/{action=Index}/{id?}/{slug?}");
            });
            app.Use(async (context, next) =>
            {
                var accessor = context.RequestServices.GetRequiredService<IHttpContextAccessor>();
                accessor.HttpContext = context;
                await next();
            });

        }
    }
}