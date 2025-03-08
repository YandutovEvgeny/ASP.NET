using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pcf.GivingToCustomer.WebHost.Extensions.ServicesExtensions
{
    public static class MassTransitServiceExtension
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration["RmqSettings:Host"], configuration["RmqSettings:VHost"], h =>
                    {
                        h.Username(configuration["RmqSettings:Login"]);
                        h.Password(configuration["RmqSettings:Password"]);
                    });
                });
            });

            return services;
        }
    }
}