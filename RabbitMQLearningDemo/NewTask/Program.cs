using System.ComponentModel.Design;
using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

// 队列持久化
channel.QueueDeclare(queue: "task_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
var message = GetMessage(args);

// 消息持久化
var properties = channel.CreateBasicProperties();
properties.Persistent = true;

foreach (var item in message)
{
    var body = Encoding.UTF8.GetBytes(item);


    channel.BasicPublish(exchange: string.Empty,
        routingKey: "task_queue",
        basicProperties: null,
        body: body);

    Console.WriteLine($" [x] Sent {message}");
}


Console.WriteLine(" Press [enter] to exit.");

Console.ReadLine();

IEnumerable<string> GetMessage(string[] args)
{
    yield return ((args.Length > 0) ? string.Join(" ", args) : "task_queue World1!");
    yield return ((args.Length > 0) ? string.Join(" ", args) : "task_queue World2!");
    yield return ((args.Length > 0) ? string.Join(" ", args) : "task_queue World3!");
    yield return ((args.Length > 0) ? string.Join(" ", args) : "task_queue World4!");
    yield return ((args.Length > 0) ? string.Join(" ", args) : "task_queue World5!");
    yield return ((args.Length > 0) ? string.Join(" ", args) : "task_queue World6!");

}