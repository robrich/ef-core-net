using Microsoft.EntityFrameworkCore;

namespace EFCoreOnNet5.Data
{
	public class DemoContext : DbContext
	{
		public DemoContext(DbContextOptions<DemoContext> options) : base(options)
		{
		}

		public DbSet<Customer> Customers => Set<Customer>();
		public DbSet<Phone> Phones => Set<Phone>();
	}
}
