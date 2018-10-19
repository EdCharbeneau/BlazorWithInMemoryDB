using Microsoft.AspNetCore.Blazor.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using StateHasChanged4.Server.Models;
using System.Linq;
using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
using StateHasChanged4.Shared;
using System.Collections.Generic;
using GenFu;
using System;

namespace StateHasChanged4.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<CustomersContext>(options =>
                        options.UseInMemoryDatabase(databaseName: "BlazorDemo"));


            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Octet,
                    WasmMediaTypeNames.Application.Wasm,
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            /* Inject the service provider */ IServiceProvider serviceProvider)
        {
            
            var o = new DbContextOptionsBuilder<CustomersContext>().UseInMemoryDatabase(databaseName: "BlazorDemo");

            //Resolve and use the service
            using (var dbContext = serviceProvider.GetService<CustomersContext>())
            {
                dbContext.Database.EnsureCreated();
                A.Configure<Customer>().Fill(c => c.Id, () => 0);
                List<Customer> customers = A.ListOf<Customer>(10);

                dbContext.Customers.AddRange(customers);
                dbContext.SaveChanges();
            }

            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}");
            });

            app.UseBlazor<Client.Startup>();
        }

    }
}
