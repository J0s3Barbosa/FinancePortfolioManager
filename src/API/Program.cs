using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddTransient<IProductRepository, ProductRepository>();
        builder.Services.AddTransient<IInvestmentRepository, InvestmentRepository>();
        builder.Services.AddTransient<IProductService, ProductService>();
        builder.Services.AddTransient<IInvestmentService, InvestmentService>();
        builder.Services.AddScoped<IEmailNotificationService, EmailNotificationService>();
        builder.Services.AddScoped<IEmailSender, EmailSender>();
        // Add email settings from configuration
        builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddApiVersioning();
        builder.Services.AddMemoryCache();
        builder.Services.AddResponseCaching();

        builder.Services.AddDbContextPool<MainContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        //options.UseLazyLoadingProxies();
        // Enable sensitive data logging for debugging
        options.EnableSensitiveDataLogging(true);
    });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
