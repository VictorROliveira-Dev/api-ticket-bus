namespace APIPassagens.Core.Abstractions;

public interface IMessageBus
{
    Task PublishAsync(string queueName, object message);
    Task SubscribeAsync(string queueName, Func<string, Task> onMessage, CancellationToken cancellationToken);
}
