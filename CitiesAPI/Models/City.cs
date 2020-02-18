using System;

namespace CitiesAPI.Models
{
	public class City
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public long Population { get; set; }
		public string[] Borders { get; set; }

		public void Validate()
		{
			if (String.IsNullOrEmpty(Name))
				throw new InvalidOperationException("Insira o nome da cidade!");

			if(Borders == null || Borders.Length == 0)
				throw new InvalidOperationException("Insira ao menos uma fronteira!");
		}
	}
}
