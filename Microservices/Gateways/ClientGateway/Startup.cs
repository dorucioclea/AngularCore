using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AngularCore.Microservices.Gateways.Http;
using ClientGateway.Helpers;
using ClientGateway.Services;

namespace ClientGateway
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton(c => new ApiConfig(Configuration));
            services.AddScoped<IHttpClient, StandardHttpClient>();
            services.AddScoped<IIdentityApiService, IdentityApiService>();
            services.AddScoped<IUsersApiService, UsersApiService>();
            services.AddScoped<ISearchApiService, SearchApiService>();
            services.AddScoped<IImagesApiService, ImagesApiService>();
            services.AddScoped<IPostsApiService, PostsApiService>();
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

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
