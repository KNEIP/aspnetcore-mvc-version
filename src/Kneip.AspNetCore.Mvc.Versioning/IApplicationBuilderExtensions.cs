// Copyright (c) KNEIP Communication S.A.. All rights reserved.

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extensions methods to configure api versioning and swagger.
    /// </summary>
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        /// Activates swagger ui with support of the api versioning.
        /// </summary>
        /// <param name="app">The current application builder.</param>
        public static void UseConfiguredApiVersioning(this IApplicationBuilder app)
        {
            // The api version descriptor
            var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    // Swagger available at the entry point of the application
                    options.RoutePrefix = string.Empty;

                    // Display versions in swagger documentation
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                });
        }
    }
}
