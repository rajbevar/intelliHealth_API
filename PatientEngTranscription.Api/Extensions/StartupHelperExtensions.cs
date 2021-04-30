using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using PatientEngTranscription.Shared.Configurations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace PatientEngTranscription.Api.Extensions
{
    public static class StartupHelperExtensions
    {
        public static IServiceCollection ConfigureRootConfiguration(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            services.AddOptions();

            services.Configure<AWSConfiguration>(options => configuration.GetSection(ConfigurationConstants.AWSKey).Bind(options));
            services.Configure<RootConfiguration>(options => configuration.GetSection("RootConfiguration").Bind(options));
            //   services.Configure<AWSConfiguration>(configuration.GetSection(ConfigurationConstants.AWSKey));

          //  var rootConfiguration = services.BuildServiceProvider().GetService<RootConfiguration>();

            services.TryAddSingleton<RootConfiguration>();
            services.TryAddSingleton<AWSConfiguration>();

            //var rootConfiguration = services.BuildServiceProvider().GetService<RootConfiguration>();
            //rootConfiguration.ContentRootPath = hostingEnvironment.ContentRootPath;
            //rootConfiguration.WebRootPath = hostingEnvironment.WebRootPath;

            return services;
        }

    }
}
