using Colorful;
using CoonsoleAlgorithms.Algorithms;
using System;
using System.Drawing;
using Console = Colorful.Console;

namespace CoonsoleAlgorithms
{
	class Program
	{
		public static SearchFirstRepeatedIndex searchFirstRepeatedIndex = new SearchFirstRepeatedIndex();
		public static ConfirmPalindrome confirmPalindrome = new ConfirmPalindrome();
		public static FigletFont font = FigletFont.Load("../../fonts/larry3d.flf");
		public static Figlet figlet = new Figlet(font);

		static void Main(string[] args)
		{
			SelectAlg();
		}

		public static void SelectAlg()
		{
			Console.Clear();
			Console.WriteLine(figlet.ToAscii("Knewin Test"), ColorTranslator.FromHtml("#8AFFEF"));
			Console.WriteLine("Selecione um algoritmo: ");
			Console.WriteLine("1 - Buscador de Números Repetidos", Color.Yellow);
			Console.WriteLine("2 - Verificador de Palíndromos", Color.Yellow);
			Console.WriteLine("0 - Sair", Color.Gray);
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
