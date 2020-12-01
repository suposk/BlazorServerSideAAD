using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.Identity.Web;
using Microsoft.Identity.Web.TokenCacheProviders.InMemory;
using Microsoft.Identity.Web.TokenCacheProviders.Distributed;
using Microsoft.Identity.Web.UI;
using BlazorServerAAD.Data;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Client.Services;
using Client.NetCore.Services;
using Azure.Core;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
//using Microsoft.Extensions.Caching.Cosmos;
//using Microsoft.Azure.Cosmos.Fluent;

namespace BlazorServerAAD
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCosmosCache((CosmosCacheOptions cacheOptions) =>
            //{
            //    cacheOptions.ContainerName = Configuration["CosmosCache:ContainerName"];
            //    cacheOptions.DatabaseName = Configuration["CosmosCache:DatabaseName"];
            //    cacheOptions.ClientBuilder = new CosmosClientBuilder(Configuration["CosmosCache:ConnectionString"]);
            //    cacheOptions.CreateIfNotExists = true;
            //});
                       

            bool UseKeyVault = Configuration.GetValue<bool>("UseKeyVault");
            var VaultName = Configuration.GetValue<string>("VaultName");
            string clientSecret = null;
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));

            if (UseKeyVault)
            {
                try
                {
                    var janoSetting = keyVaultClient.GetSecretAsync(VaultName, "JanoSetting").Result.Value;
                    Console.WriteLine($"Secret first 3 char: {janoSetting?.Substring(startIndex: 0, length: 3)}");

                    clientSecret = keyVaultClient.GetSecretAsync(VaultName, "ClientSecret").Result.Value;
                    Console.WriteLine($"Secret first 3 char: {clientSecret?.Substring(startIndex: 0, length: 3)}");
                }
                catch (Exception ex)
                {
                }
                //SecretClientOptions options = new SecretClientOptions()
                //{
                //    Retry =
                //    {
                //        Delay= TimeSpan.FromSeconds(2),
                //        MaxDelay = TimeSpan.FromSeconds(16),
                //        MaxRetries = 5,
                //        Mode = RetryMode.Exponential
                //     }
                //};
                //var client = new SecretClient(new Uri("https://devblazorserversidevault.vault.azure.net/"), new DefaultAzureCredential(), options);
                //KeyVaultSecret secret = client.GetSecret("ClientSecret");
            }

            string ApiEndpoint = Configuration.GetValue<string>("ApiEndpoint");
            services.AddHttpClient("api", (client) => 
            {
                client.BaseAddress = new Uri(ApiEndpoint);
            });

            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(Configuration.GetSection("AzureAd"))
                .EnableTokenAcquisitionToCallDownstreamApi()
                .AddInMemoryTokenCaches()
                ;

            services.Configure<MicrosoftIdentityOptions>(options =>
            {
                options.ResponseType = OpenIdConnectResponseType.Code;

                if (UseKeyVault)                
                    options.ClientSecret = clientSecret;                    
                
            });

            services.AddControllersWithViews()
                .AddMicrosoftIdentityUI();

            services.AddAuthorization(options =>
            {
                // By default, all incoming requests will be authorized according to the default policy
                //Will automatical sign in user
                //options.FallbackPolicy = options.DefaultPolicy;
            });

            services.AddRazorPages();
            services.AddServerSideBlazor()
                .AddMicrosoftIdentityConsentHandler();

            services.AddSingleton<WeatherForecastService>();
            services.AddSingleton<ISampleService, SampleService>();
            services.AddScoped<IVersionService, VersionService>();

            var sec = Configuration.GetValue<string>("AzureAd:ClientSecret");
            Console.WriteLine($"Configuration AzureAd:ClientSecret first 3 char: {sec.Substring(startIndex:0,length:3)}");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
