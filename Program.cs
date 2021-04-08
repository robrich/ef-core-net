using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EFCoreOnNet5
{
	public static class Program
	{

		public static void Main(string[] args)
		{
			IConfiguration configuration = new ConfigurationBuilder()
			  .AddJsonFile("appsettings.json", optional: true)
			  .AddUserSecrets<Startup>()
			  .AddEnvironmentVariables()
			  .AddCommandLine(args)
			  .Build();
			ServiceCollection serviceBuilder = new ServiceCollection();
			serviceBuilder.AddSingleton<IConfiguration>(configuration);
			Startup startup = new Startup(configuration);
			startup.ConfigureServices(serviceBuilder);
			ServiceProvider serviceLocator = serviceBuilder.BuildServiceProvider();

			App? app = serviceLocator.GetService<App>();
			if (app == null)
			{
				throw new ArgumentNullException(nameof(App));
			}
			app.Start();
		}


	}
}
