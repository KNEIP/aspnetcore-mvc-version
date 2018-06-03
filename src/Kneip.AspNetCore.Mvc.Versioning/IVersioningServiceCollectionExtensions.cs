// Copyright (c) KNEIP Communication S.A.. All rights reserved.

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions methods to configure api versioning and swagger.
    /// </summary>
    public static class IVersioningServiceCollectionExtensions
    {
        /// <summary>
        /// Initializes rest api versioning and the associated swagger configuration.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="title">The title of the api - used for the swagger documentation.</param>
        /// <param name="setupAction">Extra setup Action for swagger.</param>
        public static void AddConfiguredApiVersioning(this IServiceCollection services, string title, Action<SwaggerGenOptions> setupAction = null)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            else if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            // Activate api versioning
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
            });

            // Activate Api Explorer - with versioning support
            services.AddMvcCore()
                .AddApiExplorer()
                .AddVersionedApiExplorer(o =>
                {
                    o.GroupNameFormat = "'v'VVV";
                    o.SubstituteApiVersionInUrl = true;
                });

            // Activate swagger json file generation
            services.AddSwaggerGen(
                options =>
                {
                    // Integrate all versions into the swagger files
                    var provider = services.BuildServiceProvider()
                                           .GetRequiredService<IApiVersionDescriptionProvider>();

                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        Info info = new Info()
                        {
                            Title = $"{title} {description.ApiVersion}",
                            Version = description.ApiVersion.ToString()
                        };

                        options.SwaggerDoc(description.GroupName, info);
                    }

                    // Integrate xml docs
                    foreach (var file in Directory.GetFiles(AppContext.BaseDirectory, "*.xml"))
                    {
                        options.IncludeXmlComments(file);
                    }

                    // Extra configuration steps, if any
                    setupAction?.Invoke(options);
                });
        }
    }
}
