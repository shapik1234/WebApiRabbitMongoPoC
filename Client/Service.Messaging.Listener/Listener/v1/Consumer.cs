using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Service.Messaging.Listener.Options.v1;

namespace Service.Messaging.Listener.Listener.v1
{
	public class Consumer : IConsumer
	{
		private readonly string hostname;
		private readonly string password;
		private readonly string queueName;
		private readonly string username;
		private IConnection connection;
		private IModel channel;

		public Consumer(IOptions<RabbitMqConfiguration> rabbitMqOptions)
		{
			queueName = rabbitMqOptions.Value.QueueName;
			hostname = rabbitMqOptions.Value.Hostname;
			username = rabbitMqOptions.Value.UserName;
			password = rabbitMqOptions.Value.Password;

			CreateConnection();
		}

		public void Listen(Action<string> action)
		{
			if (ConnectionExists())
			{
				channel = connection.CreateModel();

				channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

				var consumer = new AsyncEventingBasicConsumer(channel);

				consumer.Received += async (ch, e) =>
				{
					var body = e.Body.ToArray();
					var message = Encoding.UTF8.GetString(body);
					action?.Invoke(message);

					await Task.Yield();
				};

				channel.BasicConsume(queueName, true, consumer);
			}
		}

		private void CreateConnection()
		{
			try
			{
				var factory = new ConnectionFactory
				{
					HostName = hostname,
					UserName = username,
					Password = password,
					DispatchConsumersAsync = true,
				};
				connection = factory.CreateConnection();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Could not create connection: {ex.Message}");
			}
		}

		private bool ConnectionExists()
		{
			if (connection != null)
			{
				return true;
			}

			CreateConnection();

			return connection != null;
		}

		public void Dispose()
		{
			connection?.Dispose();
			channel?.Dispose();
		}
	}
}