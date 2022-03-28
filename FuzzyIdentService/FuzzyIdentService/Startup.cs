using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuzzyIdentService.Abstractions;
using FuzzyIdentService.Fuzzy_Services;
using FuzzyIdentService.Models.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using FuzzyIdentService.Utils.Dependency_Injection;
using FuzzyIdentService.Models.Entities;
using FuzzyIdentService.Utils.Dependency_Injection.Services;
using FuzzyIdentService.Utils.Dependency_Injection.Services.UserService;
using FuzzyIdentService.Utils.Dependency_Injection.Services.UsersManagingService;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using Microsoft.AspNetCore.Http;
using React.AspNet;
using JavaScriptEngineSwitcher.V8;

namespace FuzzyIdentService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureDependency(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context,service) =>
            {
                service.AddSingleton<BaseRepository<BaseUser>>();
            });
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<UserContext>(options => options.UseSqlServer(connection));
            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFuzzyHandler, FuzzyHandlerScope>();
            services.AddScoped<IUserManagingService, UserManagingService>();
            services.AddScoped<IImportService, ImportService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddMvc();
            services.AddReact();
            services.AddControllers();
            // Make sure a JS engine is registered, or you will get an error!
            services.AddJsEngineSwitcher(options => options.DefaultEngineName = V8JsEngine.EngineName)
                .AddV8();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
