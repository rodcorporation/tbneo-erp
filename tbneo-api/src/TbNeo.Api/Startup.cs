using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TbNeo.Api.Config.ConfigureServices;
using TbNeo.Api.Config.Middlewares;

namespace TbNeo.WebApp.Api
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
            services.AddControllers();

            services.AddEntityFrameworkConfigureServices(Configuration)
                    .AddAppSettingsConfigureServices(Configuration)
                    .AddAuthenticationConfigureServices(Configuration)
                    .AddDependencyInjectionConfigureServices()
                    .AddMediatorConfigureServices()
                    .AddCompressionConfigureServices()
                    .AddSwaggerConfigureServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCorsMiddleware()
               .UseCompressionMiddlaware()
               .UseSwaggerMiddleware();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
