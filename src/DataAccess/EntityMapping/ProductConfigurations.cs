
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityMapping
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Id).IsRequired(true);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            builder.Property(x => x.Description).HasMaxLength(500);

            builder.Property(x => x.Price).HasPrecision(18, 2);

            builder.Property(x => x.ExpiryDate).IsRequired();

            builder.ToTable("Products").HasKey(x => x.Id);

            var currentDate = DateTime.Now.Date;

            // Calculate expiry date for data that will expire in 7 days
            var expiryDate7Days = currentDate.AddDays(7);

            builder.HasData(
                 new Product
                 {
                     Id = 1,
                     Name = "Investment Product A",
                     Description = "Description for Investment Product A",
                     Price = 100.50m,
                     ExpiryDate = expiryDate7Days //  expiring in 7 days
                 },
                new Product
                {
                    Id = 2,
                    Name = "Investment Product B",
                    Description = "Description for Investment Product B",
                    Price = 150.75m,
                    ExpiryDate = currentDate.AddDays(-7) //  already expired data
                }
                );
        }
    }
}
