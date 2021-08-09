﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Service.Messaging.Listener.Listener.v1;
using Service.Messaging.Listener.Options.v1;
using ServicesShared.Core;
using System;
using System.IO;

namespace TestCoreService.Client
{
	class Program
	{
		static void Main(string[] args)
		{
			var host = AppStartup();
			var service = host.Services.GetService<IWindowsService>();

			service.OnStart();

			Console.ReadKey();
		}

		static void BuildConfig(IConfigurationBuilder builder)
		{
			// Check the current directory that the application is running on 
			// Then once the file 'appsetting.json' is found, we are adding it.
			// We add env variables, which can override the configs in appsettings.json
			builder.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddEnvironmentVariables();
		}

		static IHost AppStartup()
		{
			var builder = new ConfigurationBuilder();
			BuildConfig(builder);

			var configRoot = builder.Build();
			// Specifying the configuration for serilog
			var serilizeLogger = new LoggerConfiguration() 
							.ReadFrom.Configuration(configRoot) // connect serilog to our configuration folder
							.Enrich.FromLogContext() 
							.WriteTo.Console()
							.CreateLogger(); 


			var host = Host.CreateDefaultBuilder() 
						.ConfigureServices((context, services) => {

							//messaging service
							services.Configure<RabbitMqConfiguration>(configRoot.GetSection(nameof(RabbitMqConfiguration)));
							bool.TryParse(configRoot["BaseServiceSettings:UserabbitMq"], out var useRabbitMq);
							if (useRabbitMq)
							{
								services.AddSingleton<ICustomerListener, CustomerListener>();
							}

							services.AddSingleton((ILogger)serilizeLogger);
							services.AddSingleton<ILoggerHandler, LoggerHandler>();
							services.AddScoped<IWindowsService, ServiceInstance>();})
						.UseSerilog()
						.Build(); 

			return host;
		}
	}
}
