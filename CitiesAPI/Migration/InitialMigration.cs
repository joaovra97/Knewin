using CitiesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitiesAPI.Migration
{
	public class InitialMigration
	{
		private readonly CityContext _context;

		public InitialMigration(CityContext context)
		{
			_context = context;
		}

		public void CreateCities()
		{
			List<City> cities = new List<City>();

			cities.Add(new City() { Name = "Florianópolis", Population = 500973, Borders = new string[] { "São José" } });
			cities.Add(new City() { Name = "São José", Population = 242927, Borders = new string[] { "Florianópolis", "Antônio Carlos" } });
			cities.Add(new City() { Name = "Antônio Carlos", Population = 8411, Borders = new string[] { "São José", "São Pedro de Alcântara" } });
			cities.Add(new City() { Name = "São Pedro de Alcântara", Population = 5139, Borders = new string[] { "Antônio Carlos" } });

			_context.Cities.AddRange(cities);
			_context.SaveChanges();
		}
	}
}
