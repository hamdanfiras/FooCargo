using Blazored.LocalStorage;
using FooCargo.CoreModels;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FooCargoWebUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            // sample on how to inject multiple http clients
            //builder.Services.AddScoped(sp => new FooApiClient { BaseAddress = new Uri(Config.ApiHost) });
            //builder.Services.AddScoped(sp => new BarApiClient { BaseAddress = new Uri(Config.ApiHost) });


            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(Config.ApiHost) });
            builder.Services.AddScoped<AuthenticationStateProvider, FooCargoAuthenticationStateProvider>();
            builder.Services.AddScoped<Auth>();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddBlazoredLocalStorage();

            await builder.Build().RunAsync();
        }
    }
}
