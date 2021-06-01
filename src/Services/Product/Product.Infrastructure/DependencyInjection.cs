using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Product.Application.Behaviours;
using Product.Application.Features.Products.Commands.CreateProduct;
using Product.Application.Features.Products.Commands.Settings;
using Product.Infrastructure.Repositories; 
using Microsoft.Extensions.Configuration;

namespace Product.Infrastructure
{  

    public static class ServiceExtensions
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // mongodb injection
            services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));
            services.AddSingleton<IDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
            services.AddSingleton<IProductRepository>(_ => new MongoDdProductRepository(_.GetRequiredService<IDatabaseSettings>()));


            // inmemory injection
            //services.AddSingleton<ISwitchRepository>(new InMemorySwitchRepository());
        }
    }
}
