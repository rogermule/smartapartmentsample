using Elasticsearch.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nest;
using SampleApartmentSample.Data.Abstractions;
using SampleApartmentSample.Data.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartApartmentSample
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
            var pool = new SingleNodeConnectionPool(new Uri("https://search-smartapartmentexam-3r6p7firyszgxglldhhnuhevny.us-east-2.es.amazonaws.com/"));
            var settings = new ConnectionSettings(pool)
                .BasicAuthentication(Configuration.GetSection("AwsElasticServer")["Username"],
                                        Configuration.GetSection("AwsElasticServer")["Password"]);
            var client = new ElasticClient (settings);
            services.AddSingleton(client);


            services.AddSingleton<IMgmtElasticSearch, MgmtElasticSearch>();
            services.AddSingleton<IPropertiesElasticSearch, PropertiesElasticSearch>();

            services.AddControllers().AddNewtonsoftJson(options =>
                 options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
