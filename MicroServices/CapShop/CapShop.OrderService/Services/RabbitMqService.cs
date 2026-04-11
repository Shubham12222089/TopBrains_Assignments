using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace OrderService.Services
{
    public class RabbitMqService
    {
        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "orderQueue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );


            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: "",
                routingKey: "orderQueue",
                basicProperties: null,
                body: body
            );
        }
    }
}