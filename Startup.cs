using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Advantage.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Advantage.API
{
    public class Startup
    {
        private string _connectionString = null;
        public Startup(IConfiguration configuration)
        {   
            
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {   
            _connectionString ="Server=localhost; Port=5432; User Id=pdev; Password=blub; Database=Advantage.API.Dev; Integrated Security = true; Pooling = true; ";// Configuration["secretConnectionString"];
            
            services.AddCors( opt => 
                {
                    opt.AddPolicy("CorsPolicy",
                    c => c.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
                })
                ;
            services.AddControllers();
            services.AddEntityFrameworkNpgsql()
            .AddDbContext<ApiContext>(
                opt => opt.UseNpgsql(_connectionString));
                services.AddTransient<DataSeed>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataSeed seed)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("CorsPolicy");
                app.UseDeveloperExceptionPage();
                
            }
            app.UseCors("CorsPolicy");
            seed.SeedData(40);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("default", "api/{controller}/{action}/{id?}");
                endpoints.MapControllers().RequireCors("CorsPolicy");
            });
            
        }
    }
}
