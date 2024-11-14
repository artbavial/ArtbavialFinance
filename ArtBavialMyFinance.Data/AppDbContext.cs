using Microsoft.EntityFrameworkCore;
using ArtbavialMyFinance.Models;

namespace ArtbavialMyFinance.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext()
		{
		}

		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
		}

		public DbSet<User> Users { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Server=Admin-PC13\\SQLEXPRESS;User ID=artbavial;Password=VITbar231089;Initial Catalog=ArtbavialFinanceDB;TrustServerCertificate=True;");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.Property(u => u.Id)
				.ValueGeneratedOnAdd();
		}
	}
}
