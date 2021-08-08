using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Blazored.Toast;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MyStream.Core;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStream
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services
                .AddBlazorise(options => options.ChangeTextOnKeyPress = true)
                .AddBootstrapProviders()
                .AddFontAwesomeIcons()
                .AddBlazoredToast();

            builder.RootComponents.Add<App>("#app");

            builder.Services
                .AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddBlazoredToast();
            builder.Services.AddBlazoredSessionStorage(config => config.JsonSerializerOptions.WriteIndented = true);
            builder.Services.AddBlazoredLocalStorage(config => config.JsonSerializerOptions.WriteIndented = true);

            builder.Services.AddScoped<IStorageService, StorageService>();

            //builder.Services.AddLogging(builder => builder
            //    .AddBrowserConsole()
            //    .SetMinimumLevel(LogLevel.Error)
            //);

            //builder.Services.AddLogging(logging =>
            //{
            //    logging.AddProvider(new CosmosLoggerProvider());
            //});

            var host = builder.Build();

            await host.RunAsync();
        }
    }
}