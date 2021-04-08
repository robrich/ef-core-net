using EFCoreOnNet5.Data;
using System;

namespace EFCoreOnNet5.Demos
{
	public class Demo5
	{
		private readonly DemoContext db;

		public Demo5(DemoContext db)
		{
			this.db = db ?? throw new ArgumentNullException(nameof(db));
		}

		public void Start()
		{
			Console.WriteLine("Demo 5: Getting Started");
			CreateDatabaseFromCode();
			CreateCodeFromDatabase();
		}

		private void CreateDatabaseFromCode()
		{
			db.Database.EnsureCreated();
		}

		private void CreateCodeFromDatabase()
		{
			/*
```
dotnet tool install --global dotnet-ef
# or
dotnet tool update --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef dbcontext scaffold "Data Source=.;Initial Catalog=EFCoreOnNet5;Trusted_Connection=True" --data-annotations --context-dir Data2 --output-dir Data2 Microsoft.EntityFrameworkCore.SqlServer
```
			*/
		}

	}
}
