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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext> (opt => opt.UseInMemoryDatabase("Database"));
            services.AddScoped<DataContext, DataContext>();
            services.AddControllers();
            services.AddMvc();
        }

        //public class Imovel
        //{
        //    public int          usableAreas   { get; set; }
        //    public string       listingType   { get; set; }
        //    public string       createdAt     { get; set; }


        //    public string listingStatus { get; set; }
        //    public string id { get; set; }
        //    public int parkingSpaces { get; set; }
        //    public string updatedAt { get; set; }
        //    public bool owner { get; set; }

        //    //public List<string> images        { get; set; }

        //    //public struct Adress // ?
        //    //{
        //    //    public string City         { get; set; }
        //    //    public string neighborhood { get; set; }
        //    //    // geoLocation

        //    //        public string precision { get; set; }
        //    //        // location
        //    //            public float lon { get; set; }
        //    //            public float lat { get; set; }
        //    //}; //

        //    public int bathrooms { get; set; }
        //    public int bedrooms { get; set; }

        //    // pricingInfos
        //    //public double yearlyIptu     { get; set; }
        //    //public double price          { get; set; }
        //    //public string businessType   { get; set; }
        //    //public float monthlyCondoFee { get; set; }

        //}

        public string LoadJson()
        {
            WebClient client = new WebClient();
            
            string json = client.DownloadString("http://grupozap-code-challenge.s3-website-us-east-1.amazonaws.com/sources/source-2.json");
          
            return json;
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            string json = LoadJson();

            var objImovel = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Imoveis>>(json);

            //Imoveis unitimovel = new Imoveis();

            //var context = app.ApplicationServices.GetService<DataContext>();
            var context = serviceProvider.GetService<DataContext>();

            //List<Imoveis> listImovel = new List<Imoveis>();
            Imoveis unitImovel = new Imoveis();

            //unitimovel = context[1];

            //context.Imoveis.Add(listImovel[1]);
            //context.SaveChanges(); //Async

            foreach (var item in objImovel)
            {
                unitImovel = item;

                context.Imoveis.Add(unitImovel);
                context.SaveChanges(); //Async
            }

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
