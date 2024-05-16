using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DataAccess
{

    public class MainContext : DbContext
    {
     
        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Investment> Investments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //modelBuilder.ApplyCustomConfigurations();
            //modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }


    }
}
