using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

// Cria conexão com o node local do RabbitMQ
var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

// Declara a fila
channel.QueueDeclare(
    queue: "hello",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);

Console.WriteLine(" [*] Waiting for messages");

// Obtem mensagens da fila
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Received: {message}");
};

// Consome as mensagens da fila
channel.BasicConsume(
    queue: "hello",
    autoAck: true,
    consumer: consumer);

Console.WriteLine("Press [enter] to exit.");
Console.ReadLine();
