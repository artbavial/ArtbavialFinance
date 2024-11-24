using Microsoft.EntityFrameworkCore;
using ArtbavialFinance.Models;
using ArtBavialMyFinance.Models;

namespace ArtBavialMyFinance.Data
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
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Transaction> Transactions { get; set; }
		public DbSet<Currency> Currencies { get; set; }

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

			// Конфигурация внешних ключей с поведением ON DELETE NO ACTION
			modelBuilder.Entity<Account>()
				.HasOne(a => a.User)
				.WithMany(u => u.Accounts)
				.HasForeignKey(a => a.UserId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Currency>()
				.HasOne(c => c.User)
				.WithMany(u => u.Currencies)
				.HasForeignKey(c => c.UserId)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Transaction>()
				.HasOne(t => t.User)
				.WithMany(u => u.Transactions)
				.HasForeignKey(t => t.UserId)
				.OnDelete(DeleteBehavior.NoAction);

			// Конфигурация типов данных decimal
			modelBuilder.Entity<Account>()
				.Property(a => a.Balance)
				.HasColumnType("decimal(18, 2)");

			modelBuilder.Entity<Currency>()
				.Property(c => c.ExchangeRate)
				.HasColumnType("decimal(18, 2)");

			modelBuilder.Entity<Transaction>()
				.Property(t => t.Amount)
				.HasColumnType("decimal(18, 2)");
		}
	}
}
