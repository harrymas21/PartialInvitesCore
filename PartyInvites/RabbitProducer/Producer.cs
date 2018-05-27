using PartyInvites.Infrastructure;
using RabbitMQ.Client;
using System;
using System.Text;

namespace PartyInvites.RabbitProducer
{
    public class Producer : IProducer
    {
        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory() { HostName = DefaultSettings.Instance.Hostname };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "EmailQueue", type: "fanout");

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "EmailQueue",
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
