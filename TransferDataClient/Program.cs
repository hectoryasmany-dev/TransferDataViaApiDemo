using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransferDataClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            builder.Services.AddXpoDefaultUnitOfWork(true, options =>
  options.UseConnectionString(WebApiDataStoreClient.GetConnectionString("https://localhost:44307/xpo/"))
  .UseConnectionPool(false)
  .UseThreadSafeDataLayer(false)
  .UseEntityTypes(typeof(Customer), typeof(Order)));
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                });
    }
}
