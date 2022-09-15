using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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
            RegisterExchange(exchangeName);
            string queueName = RegisterQueue(exchangeName);
            
            EventingBasicConsumer consumer = AddCallBack(action);

            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }

        private EventingBasicConsumer AddCallBack<TEvent>(Action<TEvent> action) where TEvent : class
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                TEvent? eventMessage = DeserializeResponse<TEvent>(ea);

                if (eventMessage != null)
                    action(eventMessage);
            };
            return consumer;
        }

        private static TEvent? DeserializeResponse<TEvent>(BasicDeliverEventArgs ea) where TEvent : class
        {
            var body = ea.Body.ToArray();
            var payload = Encoding.UTF8.GetString(body);
            var eventMessage = JsonSerializer.Deserialize<TEvent>(payload);
            return eventMessage;
        }

        private string RegisterQueue(string exchangeName)
        {
            var queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: "");
            return queueName;
        }

        private void RegisterExchange(string exchangeName)
        {
            _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);
        }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}
