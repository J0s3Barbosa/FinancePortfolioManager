
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
    public class InvestmentConfigurations : IEntityTypeConfiguration<Investment>
    {

        public void Configure(EntityTypeBuilder<Investment> builder)
        {
            builder.Property(x => x.Id);

            builder.Property(x => x.Amount).HasPrecision(18, 2).IsRequired();

            builder.Property(x => x.PurchaseDate).IsRequired();

            builder.HasOne(x => x.Product).WithMany()
                      .HasForeignKey(i => i.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Investments").HasKey(x => x.Id);

            builder.HasData(
                  new Investment
                  {
                      Id = 1,
                      ProductId = 1,
                      ClientId = 101,
                      Amount = 1000.00m,
                      PurchaseDate = new DateTime(2024, 1, 1)
                  },
                new Investment
                {
                    Id = 2,
                    ProductId = 2,
                    ClientId = 102,
                    Amount = 2000.00m,
                    PurchaseDate = new DateTime(2024, 2, 1)
                }
                );
        }
    }
}
