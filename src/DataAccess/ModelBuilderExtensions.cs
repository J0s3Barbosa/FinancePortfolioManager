using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyCustomConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Description).HasMaxLength(500);
                entity.Property(p => p.Price).HasPrecision(18, 2);
                entity.Property(p => p.ExpiryDate).IsRequired();
            });

            // Configurações personalizadas para o modelo Investment
            modelBuilder.Entity<Investment>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.Property(i => i.Amount).HasPrecision(18, 2).IsRequired();
                entity.Property(i => i.PurchaseDate).IsRequired();
                entity.HasOne(i => i.Product)
                      .WithMany()
                      .HasForeignKey(i => i.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            var currentDate = DateTime.Now.Date;

            // Calculate expiry date for data that will expire in 7 days
            var expiryDate7Days = currentDate.AddDays(7);

            // Create seed data for products
            var products = new[]
            {
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
            };

            // Create seed data for investments
            var investments = new[]
            {
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
            };

            // Add seed data to the model builder
            modelBuilder.Entity<Product>().HasData(products);
            modelBuilder.Entity<Investment>().HasData(investments);


        }
    }

}
