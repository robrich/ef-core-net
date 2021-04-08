using EFCoreOnNet5.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace EFCoreOnNet5.Demos
{
	public class Demo4
	{
		private readonly DemoContext db;

		public Demo4(DemoContext db)
		{
			this.db = db ?? throw new ArgumentNullException(nameof(db));
		}

		public void Start()
		{
			Console.WriteLine("Demo 4: Logging, SQL, and Stored Procedures");
			Logging();

			StoredProcedure();
			ExecuteSql();
			AdoNet();
		}

		private void Logging()
		{
			Console.WriteLine("Reading customers");
			List<Customer> customers = (
				from c in db.Customers
				orderby c.Id
				select c
			).ToList();
			Console.WriteLine($"Read {customers.Count} rows");
			customers[0].Name = "Logged";
			int rows = db.SaveChanges();
			Console.WriteLine($"Wrote {rows} rows");
			Console.WriteLine("---------------------");
		}

		private void StoredProcedure()
		{
			int CustomerId = 1;
			List<Customer> customers = db.Customers.FromSqlInterpolated($"CustomerById {CustomerId}").ToList();
			Console.WriteLine($"From stored procedure, loaded {customers.Count} rows");
			Console.WriteLine("---------------------");
		}

		private void ExecuteSql()
		{
			string Name = "Daphney";
			DateTime BirthDate = DateTime.Parse("1990-06-15");
			int rows = db.Database.ExecuteSqlInterpolated(
				$"insert into dbo.Customers(Name, BirthDate) values ({Name}, {BirthDate})"
			);
			Console.WriteLine($"From inline SQL, saved {rows} rows");
			Console.WriteLine("---------------------");
		}

		private void AdoNet()
		{
			Console.WriteLine("Using ADO.NET to read data");

			List<Customer> results = new List<Customer>();
			string sql = "select Id, Name, BirthDate from dbo.Customers";

			#region Better
			//using (var conn = new SqlConnection(configuration.GetConnectionString("EFCoreOnNet5"))) {
			//	using (var command = new SqlCommand(sql, conn)) {
			#endregion

			using (DbCommand command = db.Database.GetDbConnection().CreateCommand())
			{
				command.CommandText = sql;
				db.Database.OpenConnection();
				using (DbDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						results.Add(new Customer
						{
							// by position:
							Id = (int)reader[0],
							// or by name:
							Name = (string)reader["Name"],
							BirthDate = (DateTime)reader["BirthDate"]
						});
					}
				}
				db.Database.CloseConnection();
			}
			foreach (Customer c in results)
			{
				Console.WriteLine($"{c.Id}: {c.Name}");
			}
			Console.WriteLine("---------------------");
		}

	}
}
