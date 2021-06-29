using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using challenge_OLX.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net;
using challenge_OLX.Models;

namespace challenge_OLX
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // --------------------------

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext> (opt => opt.UseInMemoryDatabase("Database"));
            services.AddScoped<DataContext, DataContext>();
            services.AddControllers();
            services.AddMvc();
        }

        // --------------------------

        public string LoadJsonFile()
        {
            WebClient client = new WebClient();
            string json = client.DownloadString("http://grupozap-code-challenge.s3-website-us-east-1.amazonaws.com/sources/source-2.json");
            return json;
        }

        // --------------------------

        public string JsonDeserializer(string json)
        {
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Imoveis>>(json);
            return obj.ToString();
        }

        // --------------------------

        public void SaveImovel(string imovelobj, IServiceProvider serviceProvider)
        {
            Imoveis unitImovel;
            Imoveis imovel = new Imoveis();
            var context = serviceProvider.GetService<DataContext>();
            
            foreach (var item in imovelobj)
            {
                unitImovel = item;
                context.Imoveis.Add(unitImovel);
                context.SaveChanges();
            }
        }

        // --------------------------

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            string  json       = LoadJsonFile();
            string  imovelobj  = JsonDeserializer(json);
            
            SaveImovel(imovelobj, serviceProvider);

            // -----------

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
