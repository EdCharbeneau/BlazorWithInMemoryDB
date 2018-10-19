using StateHasChanged4.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StateHasChanged4.Server.Models;

namespace StateHasChanged4.Server.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {

        private readonly CustomersContext _context;

        public SampleDataController(CustomersContext context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public IEnumerable<Customer> ReadCustomers()
        {
            return _context.Customers.ToList();
        } 

        [HttpPost("[action]")]
        public void CreateCustomer([FromBody]Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

    }
}
