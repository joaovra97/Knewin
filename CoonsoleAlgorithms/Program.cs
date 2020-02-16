using CoonsoleAlgorithms.Algorithms;
using System;

namespace CoonsoleAlgorithms
{
	class Program
	{
		public static SearchFirstRepeatedIndex searchFirstRepeatedIndex = new SearchFirstRepeatedIndex();
		public static ConfirmPalindrome confirmPalindrome = new ConfirmPalindrome();

		static void Main(string[] args)
		{
			SelectAlg();
		}

		public static void SelectAlg()
		{
			Console.WriteLine("Selecione um algoritmo: ");
			Console.WriteLine("1 - Buscador de Números Repetidos");
			Console.WriteLine("2 - Verificador de Palíndromos");
			Console.WriteLine("0 - Sair");
			var choice = Console.ReadKey().KeyChar;
			Console.WriteLine();

			if (choice == '1')			
				searchFirstRepeatedIndex.Begin();
			
			if (choice == '2')			
				confirmPalindrome.Begin();	
			
			if (choice != '0')
			{
				System.Console.WriteLine("Selecione um dos dois algoritmos!");			
				SelectAlg();
			}
		}
	}
}
