using Blazored.LocalStorage;
using FooCargoWebUI.APIClient;
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


            //builder.Services.AddScoped(sp =>
            //{
            //    var httpClient = new HttpClient { BaseAddress = new Uri(Config.ApiHost) };

            //    //Http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", loginResult.Token);
            //    return httpClient;
            //});

            builder.Services.AddScoped<TokenHttpClientDelegatingHandler>();

            builder.Services.AddHttpClient("ServerAPI", client =>
            {
                client.BaseAddress = new Uri(Config.ApiHost);
            }).AddHttpMessageHandler<TokenHttpClientDelegatingHandler>(); 

            // in case you want to add multuple apis

            //builder.Services.AddHttpClient("FooAPI", client =>
            //{
            //    client.BaseAddress = new Uri(Config.ApiHost);
            //});


            builder.Services.AddScoped<AuthenticationStateProvider, FooCargoAuthenticationStateProvider>();
            builder.Services.AddScoped<Auth>();
            builder.Services.AddScoped<ApiClient>();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddBlazoredLocalStorage();

            await builder.Build().RunAsync();
        }
    }
}
