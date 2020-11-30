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

            string ApiEndpoint = Configuration.GetValue<string>("ApiEndpoint");
            services.AddHttpClient("api", (client) => 
            {
                client.BaseAddress = new Uri(ApiEndpoint);
            });

            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(Configuration.GetSection("AzureAd"))
                .EnableTokenAcquisitionToCallDownstreamApi()
                //.EnableTokenAcquisitionToCallDownstreamApi(new string[] { "User.Read", "Mail.Read" })            
                //.EnableTokenAcquisitionToCallDownstreamApi(new string[] { "User.Read", "Mail.Read", "user_impersonation" })
                .AddInMemoryTokenCaches()
                ;

            services.Configure<MicrosoftIdentityOptions>(options =>
            {
                options.ResponseType = OpenIdConnectResponseType.Code;
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

            var sec = Configuration.GetSection("JanoSetting").Value;
            var sec2 = Configuration.GetSection("AzureAd").GetSection("ClientSecret").Value;
            var sec3 = Configuration.GetValue<string>("AzureAd:ClientSecret");
            Console.WriteLine($"Secret : {sec3.Substring(startIndex:0,length:3)}");
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
