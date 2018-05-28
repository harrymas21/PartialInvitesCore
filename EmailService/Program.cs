using Infrastructure;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace EmailService
{
    class Program
    {
        public static void SendMail(string message)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("carbonara174@gmail.com");

                mail.To.Add(message);

                mail.Subject = "Party Invites";

                mail.Body = "<h1>Hello, thank you for the RSVP. Drinks are ready! Woohooooo.....</h1>";

                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("carbonara174@gmail.com", "carbonara123");

                    smtp.EnableSsl = true;

                    smtp.Send(mail);
                }
            }
        }

        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = DefaultSettings.Instance.Hostname,
                Port = DefaultSettings.Instance.Port,
                UserName = DefaultSettings.Instance.UserName,
                Password = DefaultSettings.Instance.Password,
                VirtualHost = DefaultSettings.Instance.VirtualHost,
                ContinuationTimeout = new TimeSpan(10, 0, 0, 0)
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "EmailQueue", type: "fanout");

                var queueName = channel.QueueDeclare().QueueName;

                channel.QueueBind(queue: queueName,
                                  exchange: "EmailQueue",
                                  routingKey: "");

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;

                    var message = Encoding.UTF8.GetString(body);

                    SendMail(message);

                    Console.WriteLine("Email sent to: {0}", message);
                };

                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);
            }
        }
    }
}
