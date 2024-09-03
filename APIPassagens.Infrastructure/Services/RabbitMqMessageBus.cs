using APIPassagens.Core.Abstractions;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace APIPassagens.Infrastructure.Services;

public class RabbitMqMessageBus : IMessageBus
{
    private readonly IConnection _connection;

    public RabbitMqMessageBus(IConnection connection)
    {
        _connection = connection;
    }

    public async Task PublishAsync(string queueName, object message)
    {
        using var channel = _connection.CreateModel();
        channel.QueueDeclare(queue: queueName, durable: true,exclusive: false ,autoDelete: false, arguments: null);

        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

        channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
        await Task.CompletedTask;
    }

    public async Task SubscribeAsync(string queueName, Func<string, Task> onMessage, CancellationToken cancellationToken)
    {
        using var channel = _connection.CreateModel();
        channel.QueueDeclare(queue: queueName, durable: true, exclusive: false ,autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            await onMessage(message);
        };

        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        await Task.CompletedTask;
    }
}
