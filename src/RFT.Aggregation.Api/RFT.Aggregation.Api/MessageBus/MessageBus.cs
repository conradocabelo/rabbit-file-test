using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace RFT.Aggregation.Api.MessageBus
{
    public interface IMessageBus : IDisposable
    {
        void ConsumeQueue<TEvent>(string queueName, Action<TEvent> action) where TEvent : class;
    }

    public class RabbitMessageBus : IMessageBus
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMessageBus(string connectionString)
        {
            var connectionFactory = new ConnectionFactory() { Uri = new Uri(connectionString) };
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void ConsumeQueue<TEvent>(string exchangeName, Action<TEvent> action) where TEvent : class
        {
            _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);

            var queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: "");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var payload = Encoding.UTF8.GetString(body);
                var eventMessage = JsonSerializer.Deserialize<TEvent>(payload);

                if (eventMessage != null)
                    action(eventMessage);
            };

            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}
