using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudWeather.Precipitation.DataAccess;
using CloudWeather.Precipitation.Repositories;
using CloudWeather.Precipitation.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace CloudWeather.Precipitation
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
            services.AddDbContext<PrecipDbContext>(opts =>
            {
                opts.EnableSensitiveDataLogging();
                opts.EnableDetailedErrors();
                opts.UseNpgsql(Configuration.GetConnectionString("AppDb"));
            }, ServiceLifetime.Transient);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CloudWeather.Precipitation", Version = "v1" });
            });

            services.AddTransient<IPrecipitationRepository, PrecipitationRepository>();
            services.AddTransient<IPrecipitationServices, PrecipitationServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CloudWeather.Precipitation v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
