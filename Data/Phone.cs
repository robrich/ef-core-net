using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace EFCoreOnNet5.Data
{
	[DebuggerDisplay("{Id}, {PhoneType}, {PhoneNumber}")]
	public class Phone
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(10)]
		public string PhoneNumber { get; set; } = "";

		[Required]
		[StringLength(1, MinimumLength = 1)]
		public string PhoneType { get; set; } = "";

#pragma warning disable CS8618 // To null or not to null navigation properties

		public int CustomerId { get; set; }

		[ForeignKey(nameof(CustomerId))]
		public Customer Customer { get; set; }
		//             ^-- to null or not to null

#pragma warning restore CS8618

	}
}
