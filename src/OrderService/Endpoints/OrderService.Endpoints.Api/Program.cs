using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderService.Endpoints.Api.Services;
using System;

namespace OrderService.Endpoints.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.Run();

          //  using (var serviceScope = host.Services.CreateScope())
          //  {
           //     var services = serviceScope.ServiceProvider;
//
                //try
                //{
                 //   var acceptedOrderConsumer = services.GetRequiredService<InventoryOrderAcceptedConsumer>();
                 //   acceptedOrderConsumer.Consume();
                //}
                //catch (Exception ex)
                //{
                //    var logger = services.GetRequiredService<ILogger<Program>>();
                //    logger.LogError(ex, "An error occurred.");
                //}
                //try
                //{
               //     var rejectedOrderConsumer = services.GetRequiredService<InventoryOrderRejectedConsumer>();
               //     rejectedOrderConsumer.Consume();
                //}
                //catch (Exception ex)
                //{
                //    var logger = services.GetRequiredService<ILogger<Program>>();
                //    logger.LogError(ex, "An error occurred.");
                //}
          //  }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}
