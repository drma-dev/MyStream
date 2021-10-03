using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Blazored.Toast;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using MyStream.Core;
using MyStream.Services;
using System;
using System.Globalization;
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

            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.AddScoped<IStringLocalizer<App>, StringLocalizer<App>>();

            builder.Services.AddBlazoredToast();
            builder.Services.AddBlazoredSessionStorage(config => config.JsonSerializerOptions.WriteIndented = true);
            builder.Services.AddBlazoredLocalStorage(config => config.JsonSerializerOptions.WriteIndented = true);

            builder.Services.AddScoped<IStorageService, StorageService>();
            builder.Services.AddScoped<ProviderServide>();
            builder.Services.AddScoped<Settings>();

            var host = builder.Build();

            ConfigureCulture(host);

            await host.RunAsync();
        }

        private static void ConfigureCulture(WebAssemblyHost host)
        {
            CultureInfo culture;
            var StorageService = host.Services.GetRequiredService<IStorageService>();
            var sett = StorageService.Local.GetItem<Settings>("Settings");

            if (sett != null)
            {
                culture = new CultureInfo(sett.Language.GetName(false));
            }
            else
            {
                culture = CultureInfo.CurrentCulture;

                //save the new settings
                sett = new Settings(StorageService);
                StorageService.Local.SetItem("Settings", sett);
            }

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
    }
}