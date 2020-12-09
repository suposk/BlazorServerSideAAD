using AutoMapper;
using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Server.Entities;
using Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiNetCore3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
            //    .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));

            //services.AddAuthentication(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)
            //        .AddMicrosoftIdentityWebApi(Configuration);

            //services.AddMicrosoftIdentityWebApiAuthentication(Configuration)
            //    .EnableTokenAcquisitionToCallDownstreamApi()
            //    .AddInMemoryTokenCaches();
            //;

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddAuthentication(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)
                    .AddMicrosoftIdentityWebApi(Configuration, "AzureAd")
                        .EnableTokenAcquisitionToCallDownstreamApi()
                        .AddInMemoryTokenCaches();

            services.AddControllers();
            //services.AddControllers(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
            services.AddScoped<IVersionRepository, VersionRepository>();

            services.AddScoped<IRepository<AppVersion>>(sp =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var ctx = serviceProvider.GetService<AppVersionContext>();                

                //var ctx = new AppVersionContext(Configuration.GetConnectionString("SqlLiteConnString"));
                //if (ctx.ChangeTracker != null)
                //    ctx.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                IRepository<AppVersion> obj = new Repository<AppVersion>(ctx);
                return obj;
            });

            services.AddDbContext<AppVersionContext>(options =>
            {
                //sql Lite                
                options.UseSqlite(Configuration.GetConnectionString("SqlLiteConnString"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
