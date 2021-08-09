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
			Client = new MongoClient(settings.Value.ConnectionString);
			Database = Client.GetDatabase(settings.Value.DatabaseName);			
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
