using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerSideAAD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            //var host = new HostBuilder()
            //    .ConfigureAppConfiguration((hostcontext, builder) =>
            //    {
            //        if (hostcontext.HostingEnvironment.IsDevelopment())
            //            builder.AddUserSecrets<Program>();                                                            
            //    })
            //    .ConfigureWebHost(web => 
            //    {
            //        web.UseStartup<Startup>();
            //    })
            //    ;                        
            //host.Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}
