using EFCoreOnNet5.Data;
using EFCoreOnNet5.Demos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EFCoreOnNet5
{
	public class Startup
	{
		private readonly IConfiguration configuration;

		public Startup(IConfiguration configuration)
		{
			this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		public void ConfigureServices(IServiceCollection services)
		{
			string connectionstring = configuration.GetConnectionString("EFCoreOnNet5");
			services.AddDbContext<DemoContext>(options =>
			{
				options.UseSqlServer(connectionstring);
				#region Demo4
				//options.LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuting });
				#endregion
			});

			services.AddTransient<App>();
			services.AddTransient<Demo1>();
			services.AddTransient<Demo2>();
			services.AddTransient<Demo3>();
			services.AddTransient<Demo4>();
			services.AddTransient<Demo5>();
		}

	}
}
