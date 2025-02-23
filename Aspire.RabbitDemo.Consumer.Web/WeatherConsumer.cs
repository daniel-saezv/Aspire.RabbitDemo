using Aspire.RabbitDemo.ServiceDefaults;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Aspire.RabbitDemo.Consumer.Web;

public class WeatherConsumer(WeatherDataService weatherDataService, IConnection connection) : IHostedService
{
    private readonly WeatherDataService _weatherDataService = weatherDataService;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var channel = connection.CreateModel();

        channel.QueueDeclare(RabbitStatic.QueueName, durable: true, exclusive: false, autoDelete: true);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (model, ea) =>
        {
            Console.WriteLine("Event recieved");
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var forecasts = JsonSerializer.Deserialize<WeatherForecast[]>(message);
            _weatherDataService.UpdateWeatherData(forecasts);
        };

       channel.BasicConsume(queue: RabbitStatic.QueueName, autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        //Stop Logic close connections channels...

        throw new NotImplementedException();
    }
}
