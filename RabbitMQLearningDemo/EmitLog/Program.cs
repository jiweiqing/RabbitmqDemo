using System.Text;
using RabbitMQ.Client;

// Fanout 发布/订阅  广播
var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

var message = GetMessage(args);

foreach (var item in message)
{
    var body = Encoding.UTF8.GetBytes(item);
    channel.BasicPublish(exchange: "logs",
                         routingKey: string.Empty,
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