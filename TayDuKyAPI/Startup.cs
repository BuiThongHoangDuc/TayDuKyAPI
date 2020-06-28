using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TayDuKyAPI.Models;
using TayDuKyAPI.Repository;
using TayDuKyAPI.Service;

namespace TayDuKyAPI
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
            services.AddSwaggerGen(gen =>
            {
                gen.SwaggerDoc("v1.0", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "PRM391 API", Version = "v1.0" });
            });

            services.AddDbContext<PRM391Context>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("PRM391_Final")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IScenarioRepository, ScenarioRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IScenarioService, ScenarioService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(UI =>
            {
                UI.SwaggerEndpoint("/swagger/v1.0/swagger.json", "V1.1");
            });

            app.UseMvc();
        }
    }
}
