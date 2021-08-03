using DevExpress.Xpo.DB;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TransferDataWASM_Client.Model;

namespace TransferDataWASM_Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            //added by me
            builder.Services.AddXpoDefaultUnitOfWork(true, options =>
                options.UseConnectionString(WebApiDataStoreClient.GetConnectionString("https://localhost:44397/xpo/"))
                .UseConnectionPool(false)
                .UseThreadSafeDataLayer(false)
                .UseEntityTypes(typeof(Clients)));
                builder.Services.AddDevExpressBlazor();

            await builder.Build().RunAsync();
        }
    }
}
