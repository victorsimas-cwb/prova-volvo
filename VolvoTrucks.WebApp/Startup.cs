using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VolvoTrucks.DataAccess;
using VolvoTrucks.Repositories;
using VolvoTrucks.Services;

namespace VolvoTrucks.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<VolvoTruckContext>(opt => opt.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=VolvoTrucks; Trusted_Connection=True; MultipleActiveResultSets=true"));
            services.AddScoped<ITruckRepository, TruckRepository>();
            services.AddScoped<ITruckModelRepository, TruckModelRepository>();
            services.AddScoped<ITruckService, TruckService>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, VolvoTruckContext dbContext)
        {
            

            if (env.IsDevelopment())
            {
                dbContext.Database.Migrate();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Trucks}/{action=Index}/{id?}");
            });
        }
    }
}
