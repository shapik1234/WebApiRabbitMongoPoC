using CustomerApi.Authentication.Models.v1;
using CustomerApi.Authentication.Options.v1;
using CustomerApi.Authentication.Services.v1;
using CustomerApi.Data;
using CustomerApi.Data.Database;
using CustomerApi.Data.Entities;
using CustomerApi.Data.Options;
using CustomerApi.Data.Repository.v1;
using CustomerApi.Messaging.Send.Options.v1;
using CustomerApi.Messaging.Send.Sender.v1;
using CustomerApi.Middlewares;
using CustomerApi.Service.v1.Command;
using CustomerApi.Service.v1.Query;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CustomerApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddHealthChecks();
			services.AddOptions();
			services.AddMvc();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "Customer Api",
					Description = "A simple API to create or update customers",
					Contact = new OpenApiContact
					{
						Name = "Shapovalov Danylo",
						Email = "dobriyshapik@gmail.com",
						Url = new Uri("https://www.linkedin.com/in/shapik/")
					}
				});
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = actionContext =>
				{
					var actionExecutingContext =
						actionContext as ActionExecutingContext;

					if (actionContext.ModelState.ErrorCount > 0
						&& actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
					{
						return new UnprocessableEntityObjectResult(actionContext.ModelState);
					}

					return new BadRequestObjectResult(actionContext.ModelState);
				};
			});
			//messaging service
			services.Configure<RabbitMqConfiguration>(Configuration.GetSection(nameof(RabbitMqConfiguration)));
			bool.TryParse(Configuration["BaseServiceSettings:UserabbitMq"], out var useRabbitMq);
			if (useRabbitMq)
			{
				services.AddSingleton<ICustomerUpdateSender, CustomerUpdateSender>();
			}
			//autentication service
			services.Configure<AuthenticationServiceConfiguration>(Configuration.GetSection(nameof(AuthenticationServiceConfiguration)));
			services.AddSingleton<IUserAuthenticationService, UserAuthenticationService>();
			//mongo database service
			services.Configure<MongoDatabaseConfiguration>(Configuration.GetSection(nameof(MongoDatabaseConfiguration)));
			services.AddSingleton(typeof(CustomerContext));
			services.AddSingleton<ICustomerRepository, CustomerRepository>();
			//patterns
			services.AddAutoMapper(typeof(Startup));
			services.AddMediatR(Assembly.GetExecutingAssembly());
			services.AddTransient<IRequestHandler<AuthenticateUserCommand, AuthenticateResponseModel>, AuthenticateUserCommandHandler>();
			services.AddTransient<IRequestHandler<CreateCustomerCommand, Customer>, CreateCustomerCommandHandler>();
			services.AddTransient<IRequestHandler<UpdateCustomerCommand, Customer>, UpdateCustomerCommandHandler>();
			services.AddTransient<IRequestHandler<GetCustomerByIdQuery, Customer>, GetCustomerByIdQueryHandler>();
			services.AddTransient<IRequestHandler<GetCustomersQuery, List<Customer>>, GetCustomersQueryHandler>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer API V1");
				c.RoutePrefix = string.Empty;
			});
			app.UseRouting();			

			// custom jwt auth middleware
			app.UseMiddleware<JwtMiddleware>();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapHealthChecks("/health");
				endpoints.MapControllerRoute(
				   name: "default",
				   pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
