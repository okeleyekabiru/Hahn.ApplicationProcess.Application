using Hahn.ApplicationProcess.February2021.Data.EfRepository;
using Hahn.ApplicationProcess.February2021.Domain.interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using Hahn.ApplicationProcess.February2021.Domain.Middlewares;
using MediatR;
using Hahn.ApplicationProcess.February2021.Domain.Application.Command;
using FluentValidation.AspNetCore;
namespace Hahn.ApplicationProcess.February2021.Web
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

            services.AddControllers()
                 .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddScoped<IAssetRepository, AssetRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWorkRepository>();
            services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
         
            services.AddMediatR(typeof(AddAssetCommand));
            services.AddDbContext<HahnDbContext>(opt => {
                opt.UseSqlServer(Configuration.GetConnectionString("hahnConn"));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hahn.ApplicationProcess.February2021.Web", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hahn.ApplicationProcess.February2021.Web v1"));
            }

            //app.UseHttpsRedirection();
            app.UseHttpStatusCodeExceptionMiddleware();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
