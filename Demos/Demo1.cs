using EFCoreOnNet5.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreOnNet5.Demos
{
	public class Demo1
	{
		private readonly DemoContext db;

		public Demo1(DemoContext db)
		{
			this.db = db ?? throw new ArgumentNullException(nameof(db));
		}

		public void Start()
		{
			Console.WriteLine("Demo 1: CRUD in Entity Framework Core");
			ReadData();
			UpdateData();
			CreateData();
			DeleteData();
		}

		private void ReadData()
		{
			Console.WriteLine("Reading customers");
			List<Customer> customers = (
				from c in db.Customers
				select c
			).ToList();

			foreach (Customer c in customers)
			{
				Console.WriteLine($"{c.Id}: {c.Name}");
			}
			Console.WriteLine("---------------------");
		}

		private void UpdateData()
		{
			Customer customer = (
				from c in db.Customers
				where c.Id == 1
				select c
			).First();
			customer.Name = "Hellen";
			int rows = db.SaveChanges();
			Console.WriteLine($"Modified {rows} row");
			Console.WriteLine("---------------------");
		}

		private void CreateData()
		{
			Customer customer = new Customer
			{
				Name = "Susan",
				BirthDate = DateTime.Parse("1970-01-02")
			};
			db.Customers.Add(customer);
			int rows = db.SaveChanges();
			Console.WriteLine($"Added {rows} row: {customer.Id}");
			Console.WriteLine("---------------------");
		}

		private void DeleteData()
		{
			Customer customer = (
				from c in db.Customers
				orderby c.Id descending
				select c
			).First();
			db.Customers.Remove(customer);
			int rows = db.SaveChanges();
			Console.WriteLine($"Deleted {rows} row");
			Console.WriteLine("---------------------");
		}

	}
}
