using EFCoreOnNet5.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreOnNet5.Demos
{
	public class Demo3
	{
		private readonly DemoContext db;

		public Demo3(DemoContext db)
		{
			this.db = db ?? throw new ArgumentNullException(nameof(db));
		}

		public void Start()
		{
			Console.WriteLine("Demo 3: Joins");
			LoadChildren();
			FilterOnParent();
		}

		private void LoadChildren()
		{
			/*
			select *
			from dbo.Phones
			where CustomerId = 1
			*/
			Customer customer = (
				from c in db.Customers
				where c.Id == 1
				select c
			).First();
			Console.WriteLine($"Phones for {customer.Name}");
			foreach (Phone phone in customer.Phones)
			{
				Console.WriteLine($"  {phone.PhoneType}: {phone.PhoneNumber}");
			}
			Console.WriteLine("---------------------");
		}
		// ... add .Include(c => c.Phones)
		// ... .Where(p => p.PhoneType == "W")

		private void FilterOnParent()
		{
			string name = "Theresa Pearson";
			List<Phone> phones = (
				from p in db.Phones
				where p.Customer.Name == name
				select p
			).ToList();
			Console.WriteLine($"Phones for {name}");
			foreach (Phone phone in phones)
			{
				Console.WriteLine($"{phone.PhoneType}: {phone.PhoneNumber}");
			}
			Console.WriteLine("---------------------");
		}
		// .Include(p => p.Customer)

	}
}
