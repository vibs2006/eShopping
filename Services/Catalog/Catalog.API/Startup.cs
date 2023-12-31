﻿using Catalog.Application.Handlers;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Catalog.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiVersioning();
            services
                .AddHealthChecks()
                .AddMongoDb(
                Configuration["DatabaseSettings:ConnectionString"] ?? ""
                , "Catalog Mongodb Health check"
                , HealthStatus.Degraded);

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1",new OpenApiInfo {
                                    Title = "Catalog API",
                                    Version = "v1"
               }); 
            });
                
            //DI
            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(CreateProductHandler).GetTypeInfo().Assembly);
            services.AddScoped<ICatalogContext, CatalogContext>();
            //services.AddScoped<ICorrelationIdGenerator, CorrelationIdGenerator>();
            services.AddScoped<ICatalogContext, CatalogContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBrandRepository, ProductRepository>();
            services.AddScoped<ITypesRepository, ProductRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { 
                endpoints.MapControllers();
                //endpoints.MapHealthChecks("/health", new HealthCheckOptions { 
                
                //} );
            });
        }
    }
}
