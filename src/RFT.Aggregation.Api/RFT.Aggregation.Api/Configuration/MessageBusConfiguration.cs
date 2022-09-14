using RFT.Aggregation.Api.IntegrationHandler;
using RFT.Aggregation.Api.MessageBus;

namespace RFT.Aggregation.Api.Configuration
{
    public static class MessageBusConfiguration
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
        {
            var messageBusConnection = configuration.GetSection("MESSAGE_BUS_CONNECTION")?.Value;

            if (string.IsNullOrEmpty(messageBusConnection)) throw new ArgumentNullException();     

            services.AddSingleton<IMessageBus>(new RabbitMessageBus(messageBusConnection))
                    .AddHostedService<FileIntegrationHandler>();

            return services;
        }
    }
}
