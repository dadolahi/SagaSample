using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderService.Core.Abstraction.DbContexts;
using OrderService.Core.Abstraction.Services;
using OrderService.Core.Domin.Entities;
using OrderService.Endpoints.Api.Services;
using OrderService.Infra.DataLayer.Contexts;
using OrderService.Infra.Services.Services;
using SagaSample.Share.Common.Abstract;
using SagaSample.Share.RabbitPublisher;
using SagaSample.Share.Toolkit;

namespace OrderService.Endpoints.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OrderServiceDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<IOrderServicedbContext, OrderServiceDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            services.AddSwaggerGen();

            var rabbitConfig = Configuration.GetSection("rabbit");
            services.Configure<RabbitOptions>(rabbitConfig);

            services.AddScoped<IOrderService, Infra.Services.Services.OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRabbitMQPublisher, RabbitMQPublisher>();
            services.AddScoped<InventoryOrderRejectedConsumer, InventoryOrderRejectedConsumer>();
            services.AddScoped<InventoryOrderAcceptedConsumer, InventoryOrderAcceptedConsumer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOrderServicedbContext dbContext)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Service");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           
        }
    }
}
