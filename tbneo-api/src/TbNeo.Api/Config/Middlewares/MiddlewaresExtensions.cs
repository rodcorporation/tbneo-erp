using Microsoft.AspNetCore.Builder;

namespace TbNeo.Api.Config.Middlewares
{
    public static class MiddlewaresExtensions
    {
        public static IApplicationBuilder UseCorsMiddleware(this IApplicationBuilder app)
        {
            app.UseCors(options =>
            {
                options.WithOrigins("*")
                       .AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });

            return app;
        }

        public static IApplicationBuilder UseCompressionMiddlaware(this IApplicationBuilder app)
        {
            app.UseResponseCompression();

            return app;
        }

        public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app)
        {
            app.UseSwagger()
               .UseSwaggerUI();

            return app;
        }
    }
}
