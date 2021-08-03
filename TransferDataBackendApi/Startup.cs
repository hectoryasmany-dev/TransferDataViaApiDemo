using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransferDataBackendApi
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
            string connectionString = Configuration.GetConnectionString("ConnectionString");
            IDataStore dataStore = XpoDefault.GetConnectionProvider(connectionString, AutoCreateOption.SchemaAlreadyExists);
            //dataStore = new DataCacheRoot(dataStore);
            services.AddSingleton(new WebApiDataStoreService(dataStore));
            services.AddControllers();
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddCors(options => {
                options.AddPolicy("AllowCorsPolicy", builder => {
                    // Allow all ports on local host.
                    builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
                    builder.WithHeaders("Content-Type");
                });
            });
            //services.AddCors(options =>
            //    options.AddPolicy("XPO", builder =>
            //        builder.WithOrigins("https://dd35fadc2d62.ngrok.io")
            //            .WithMethods("POST")
            //            .WithHeaders("Content-Type")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            // global cors policy
            
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowCorsPolicy");
            //app.UseCors(x => x
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .SetIsOriginAllowed(origin => true) // allow any origin
            //    .AllowCredentials()); // allow credentials
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
