using CustomerApi.Data.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;

namespace CustomerApi.Data.Database
{
	public abstract class MongoContext<T> : IMongoContext<T>
	{
		public MongoContext(IOptions<MongoDatabaseConfiguration> settings)
		{
			if (settings == null || settings.Value.ConnectionString == null || settings.Value.ConnectionString.Length == 0
				|| settings.Value.DatabaseName == null || settings.Value.DatabaseName.Length == 0)
			{
				//throw new ArgumentNullException(nameof(settings));
			}

			//Client = new MongoClient(settings.Value.ConnectionString);
			//Database = Client.GetDatabase(settings.Value.DatabaseName);			
		}

		protected IMongoClient Client {get; set;}

		protected IMongoDatabase Database { get; set; }

		public IClientSessionHandle Session { get; set; }

		public IMongoCollection<T> GetCollection<T>()
		{
			return Database.GetCollection<T>(typeof(T).Name);
		}
	}
}
