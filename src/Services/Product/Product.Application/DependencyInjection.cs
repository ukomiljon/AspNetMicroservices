using AutoMapper;
using EventBus.Messages;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection; 
using Product.Application.Behaviours;
using System.Reflection;

 

namespace Product.Application
{  

    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>)); 
        }
    }
}
