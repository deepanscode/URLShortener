using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Service;
using URLShortener.Service.Contracts;
using URLShortener.Service.Models;
using URLShortener.Service.Repositories;

[assembly: FunctionsStartup(typeof(Startup))]
namespace URLShortener.Service
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddSingleton(config);
            builder.Services.AddSingleton(config.GetSection("Values:CosmosDbOptions").Get<CosmosDbOptions>());
            builder.Services.AddTransient<IUrlMappinRepository, UrlMappinRepository>();

        }
    }
}
