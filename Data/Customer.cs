using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace EFCoreOnNet5.Data
{
	[DebuggerDisplay("{Id}, {Name}, {BirthDate.ToShortDateString()}")]
	public class Customer
	{
		public int Id { get; set; }
		public string Name { get; set; } = "";
		public DateTime? BirthDate { get; set; }

		#region Demo3
#pragma warning disable CS8618 // To null or not to null navigation properties
		public List<Phone> Phones { get; set; }
#pragma warning restore CS8618 // To null or not to null navigation properties
		#endregion
	}
}
