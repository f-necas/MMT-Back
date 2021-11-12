﻿namespace MMT_Back.ApplicationBuilderExtensions
{
    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Register Swagger endpoints.
        /// </summary>
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, string routePrefix)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
                c.RoutePrefix = routePrefix;
            });

            return app;
        }
    }
}
