/*
    RabbitMQ Hello World
    See documentation: https://www.rabbitmq.com/tutorials/tutorial-one-dotnet
 */

using System.Text;
using RabbitMQ.Client;

// Cria conexão com o node local do RabbitMQ
var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

// Cria uma fila
channel.QueueDeclare(
    queue: "hello",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);

// Cria uma mensagem
Console.WriteLine("Insert a message: ");
var message = Console.ReadLine();

var body = Encoding.UTF8.GetBytes(message);

// Envia a mensagem
channel.BasicPublish(
    exchange: string.Empty,
    routingKey: "hello",
    basicProperties: null,
    body: body);
Console.WriteLine($" [x] Sent {message}");

Console.WriteLine("Press [enter] to exit.");
Console.ReadLine();