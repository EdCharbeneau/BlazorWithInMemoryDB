using Microsoft.EntityFrameworkCore;
using StateHasChanged4.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StateHasChanged4.Server.Models
{
    public class CustomersContext : DbContext
    {
        public CustomersContext()
        { }

        public CustomersContext(DbContextOptions<CustomersContext> options)
            : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                        .HasKey(c => c.Id);
        }
    }
}
