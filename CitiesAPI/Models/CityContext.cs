using Microsoft.EntityFrameworkCore;

namespace CitiesAPI.Models
{
	public class CityContext : DbContext
	{
		public CityContext(DbContextOptions<CityContext> options) : base(options)
		{

		}

		public DbSet<City> Cities { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<City>()
				.Property(e => e.Borders)
				.HasConversion(
					v => string.Join(',', v),
					v => v.Split(',',System.StringSplitOptions.RemoveEmptyEntries)
				);
		}
	}
}
