using BlazorServerSideAAD.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Web.TokenCacheProviders.InMemory;

namespace BlazorServerSideAAD
{
    //v2 

    public class Startup
    {
        public const string scopesToRequest = "User.Read";        
        public static List<string> scopesToRequestList = new List<string>(){ "User.Read"};
        MicrosoftIdentityOptions _microsoftIdentityOptions = null;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AzureAd>(Configuration.GetSection("AzureAd"));


            //services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            //    .AddMicrosoftIdentityWebApp(Configuration.GetSection("AzureAd"));

            ////v2 start
            //services.AddMicrosoftIdentityWebAppAuthentication(Configuration);

            //v2 advanced
            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                    .AddMicrosoftIdentityWebApp(options =>
                    {
                        Configuration.Bind("AzureAD", options);
                        options.Events ??= new OpenIdConnectEvents();
                        options.Events.OnTokenValidated += OnTokenValidatedFunc;
                    });

            ////v3 also api
            //services.AddMicrosoftIdentityWebAppAuthentication(Configuration)
            //             .EnableTokenAcquisitionToCallDownstreamApi(new string[] { scopesToRequest })
            //                  .AddInMemoryTokenCaches();


            ////v4 advanced
            //services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            //                .AddMicrosoftIdentityWebApp(options =>
            //                {
            //                    Configuration.Bind("AzureAD", options);
            //                    options.Events ??= new OpenIdConnectEvents();
            //                    options.Events.OnTokenValidated += OnTokenValidatedFunc;
            //                    _microsoftIdentityOptions = options;
            //                })
            //                .EnableTokenAcquisitionToCallDownstreamApi((op) =>
            //                {
            //                    op.ClientId = _microsoftIdentityOptions.ClientId;
            //                    op.ClientSecret = _microsoftIdentityOptions.ClientSecret;
            //                    op.Instance = _microsoftIdentityOptions.Instance;
            //                    op.TenantId = _microsoftIdentityOptions.TenantId;
            //                    op.RedirectUri = _microsoftIdentityOptions.CallbackPath;
            //                    //op.ClientCapabilities = _microsoftIdentityOptions.

            //                }, scopesToRequestList)
            //                .AddInMemoryTokenCaches();

            //services.AddTokenAcquisition(false);

            services.AddControllersWithViews()
                .AddMicrosoftIdentityUI();

            services.AddAuthorization(options =>
            {
                // By default, all incoming requests will be authorized according to the default policy
                options.FallbackPolicy = options.DefaultPolicy;
            });

            services.AddRazorPages();
            services.AddServerSideBlazor()
                .AddMicrosoftIdentityConsentHandler();
            services.AddSingleton<WeatherForecastService>();

            //services.AddSignalR();

            var sec = Configuration.GetSection("JanoSetting").Value;
            var sec2 = Configuration.GetSection("AzureAd").GetSection("ClientSecret").Value;
            var sec3 = Configuration.GetValue<string>("AzureAd:ClientSecret");
            Console.WriteLine($"Secret : {sec3}");
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

        private async Task OnTokenValidatedFunc(TokenValidatedContext context)
        {
            var res = context.Result;

            // Custom code here
            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}
