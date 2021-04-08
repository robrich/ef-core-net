using EFCoreOnNet5.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EFCoreOnNet5.Demos
{
	public class Demo2
	{
		private readonly DemoContext db;
		private readonly IConfiguration configuration;

		public Demo2(DemoContext db, IConfiguration configuration)
		{
			this.db = db ?? throw new ArgumentNullException(nameof(db));
			this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		public void Start()
		{
			Console.WriteLine("Demo 2: Data Validation and Entity Tracking");
			DataValidation();
			DataValidationByHand();

			RegularEntityTracking();
			DisconnectedData();
		}

		private void DataValidation()
		{
			Customer customer = new Customer
			{
				Name = "A really long name in the name field that is much longer than the field allows",
				BirthDate = DateTime.Parse("1890-01-02")
			};
			db.Customers.Add(customer);
			try
			{
				int rows = db.SaveChanges();
				Console.WriteLine($"Added {rows} row: {customer.Id}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
			}
			Console.WriteLine("---------------------");
		}
		// ... add data validation attributes and try again

		private void DataValidationByHand()
		{
			Customer customer = new Customer
			{
				Name = "A really long name in the name field that is much longer than the field allows",
				BirthDate = DateTime.Parse("1890-01-02")
			};
			db.Customers.Add(customer);

			List<ValidationResult> validationResults = new List<ValidationResult>();
			if (!Validator.TryValidateObject(customer, new ValidationContext(customer), validationResults, true))
			{
				Console.WriteLine("Error validating customer");
				foreach (ValidationResult error in validationResults)
				{
					Console.WriteLine($"{string.Join(", ", error.MemberNames)}: {error.ErrorMessage}");
				}
			}
			if (validationResults.Count < 1)
			{
				int rows = db.SaveChanges();
				Console.WriteLine($"Added {rows} row: {customer.Id}");
			}
			else
			{
				//throw new YourTypeOfException(validationResults);
			}
			Console.WriteLine("---------------------");
		}



		private void RegularEntityTracking()
		{
			Customer customer = (
				from c in db.Customers
				where c.Id == 1
				select c
			).First();
			customer.Name = "Darlene";
			int rows = db.SaveChanges();
			Console.WriteLine($"Modified {rows} row");
			Console.WriteLine("---------------------");
		}

		// E.g. ASP.NET, Web API, gRPC, etc
		private void DisconnectedData()
		{
			// simulated REST call to get data:
			int id = 1;
			Customer customer;
			using (DemoContext db = MakeDbContext())
			{
				customer = (
					from c in db.Customers.AsNoTracking()
					where c.Id == id
					select c
				).First();
			}
			// pretend we return this to the caller

			// UI changed the data:
			customer.Name = "Stacey";

			// simulated REST call to update data:
			using (DemoContext db = MakeDbContext())
			{
				int rows = db.SaveChanges();
				Console.WriteLine($"Modified {rows} row");
			}
			Console.WriteLine("---------------------");
		}
		// db.Customers.Update(customer);
		// db.Entry(customer).State = EntityState.Modified;

		private DemoContext MakeDbContext()
		{
			DbContextOptionsBuilder<DemoContext> optionsBuilder = new DbContextOptionsBuilder<DemoContext>();
			optionsBuilder.UseSqlServer(configuration.GetConnectionString("EFCoreOnNet5"));
			DemoContext db = new DemoContext(optionsBuilder.Options);
			db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
			return db;
		}

	}
}
