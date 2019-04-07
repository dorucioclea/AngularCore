using AdminGateway.Helpers;
using AngularCore.Microservices.Gateways.Api.Extensions;
using AngularCore.Microservices.Gateways.Api.Helpers;
using AngularCore.Microservices.Gateways.Api.Services;
using AngularCore.Microservices.Gateways.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdminGateway
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
            services.Configure<ApplicationConfig>(appSettingsSection);

            services.AddCustomJwt(appSettingsSection.GetValue<string>("Secret"));
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(c => new ApiConfig(Configuration));
            services.AddScoped<IHttpClient, StandardHttpClient>();
            services.AddScoped<IAdminIdentityApiService, IdentityApiService>();
            services.AddScoped<IAdminUsersApiService, UsersApiService>();
            services.AddScoped<IAdminSearchApiService, SearchApiService>();
            services.AddScoped<IAdminImagesApiService, ImagesApiService>();
            services.AddScoped<IAdminPostsApiService, PostsApiService>();
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
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
