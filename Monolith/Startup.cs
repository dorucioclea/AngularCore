using AngularCore.Helpers;
using AngularCore.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AngularCore.Extensions;
using AngularCore.Services;
using AngularCore.Repositories.Impl;
using AutoMapper;
using AngularCore.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using AngularCore.Services.Impl;
using AngularCore.Helpers.Security;
using Microsoft.AspNetCore.Authorization;

namespace AngularCore
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
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
                
            services.AddDbContext<ApplicationContext>( options =>
                options.UseMySql(appSettingsSection.GetValue<string>("DbConnection"))
            );
            services.AddAutoMapper();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsAdmin", policy => policy.Requirements.Add(new IsAdminRequirement()));
            });
            services.AddCustomJwt(appSettingsSection.GetValue<string>("Secret"));

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();

            services.AddTransient<IImageService, ImageService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddSingleton<IAuthorizationHandler, IsAdminHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
