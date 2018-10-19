using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StateHasChanged4.Server.Models;
using StateHasChanged4.Shared;
using GenFu;
using System.Collections.Generic;

namespace StateHasChanged4.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Run();
        }

        public static IWebHost CreateWebHostBuilder(string[] args)
        {
            var hostBuilder = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

            var host = hostBuilder.Build();

            //SeedDatabase(host);

            return host;
        }

        private static void SeedDatabase(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<CustomersContext>();

                dbContext.Database.EnsureCreated();

                List<Customer> customers = A.ListOf<Customer>(10);

                dbContext.Customers.AddRange(customers);
                dbContext.SaveChanges();
            }
        }
    }
}
