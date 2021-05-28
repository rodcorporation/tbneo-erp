using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TbNeo.Application.Commands;
using TbNeo.Application.Commands.Handlers;
using TbNeo.Application.Queries;
using TbNeo.Data;
using TbNeo.Data.Repositories;
using TbNeo.Domain.Core.Communication.Notifications;
using TbNeo.Domain.Repositories;
using TbNeo.WebApp.Api;

namespace TbNeo.Api.Config.ConfigureServices
{
    public static class ConfigureSevicesExtensions
    {
        public static IServiceCollection AddEntityFrameworkConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TbNeoContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }

        public static IServiceCollection AddDependencyInjectionConfigureServices(this IServiceCollection services)
        {
            // Notifications

            services.AddScoped<INotificationHandler<Notification>, NotificationHandler>();

            // Queries

            services.AddScoped<IProjetoQueries, ProjetoQueries>();
            services.AddScoped<IFeatureFlagQueries, FeatureFlagQueries>();

            // Commands

            services.AddScoped<IRequestHandler<ProjetoCadastrarCommand, bool>, ProjetoCommandHandler>();
            services.AddScoped<IRequestHandler<ProjetoEditarCommand, bool>, ProjetoCommandHandler>();
            services.AddScoped<IRequestHandler<FeatureFlagCadastrarCommand, bool>, FeatureFlagCommandHandler>();
            services.AddScoped<IRequestHandler<FeatureFlagEditarCommand, bool>, FeatureFlagCommandHandler>();


            // Repositores
            services.AddScoped<IProjetoRepository, ProjetoRepository>();
            services.AddScoped<IFeatureFlagRepository, FeatureFlagRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            return services;
        }

        public static IServiceCollection AddMediatorConfigureServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup).Assembly);

            return services;
        }

        public static IServiceCollection AddCompressionConfigureServices(this IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.Providers.Add<BrotliCompressionProvider>();
            });

            return services;
        }

        public static IServiceCollection AddSwaggerConfigureServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                                   new OpenApiInfo
                                   {
                                       Title = "Api TbNeo",
                                       Description = "Api do TbNeo",
                                       Version = "v1"
                                   });
            });

            return services;
        }
    }
}
