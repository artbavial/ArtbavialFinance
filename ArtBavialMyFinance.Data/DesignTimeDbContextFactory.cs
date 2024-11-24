using ArtBavialMyFinance.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ArtbavialMyFinance.Data
{
	public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
	{
		public AppDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
			optionsBuilder.UseSqlServer("Server=Admin-PC13\\SQLEXPRESS;User ID=artbavial;Password=VITbar231089;Initial Catalog=ArtbavialFinanceDB;TrustServerCertificate=True;");

			return new AppDbContext(optionsBuilder.Options);
		}
	}
}
