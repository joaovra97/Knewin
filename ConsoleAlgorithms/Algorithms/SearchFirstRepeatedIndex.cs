using Colorful;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Drawing;
using Console = Colorful.Console;

namespace CoonsoleAlgorithms.Algorithms
{
	public class SearchFirstRepeatedIndex : Algorithm
	{
		public static FigletFont _font = FigletFont.Load("../../fonts/mini.flf");
		public static Figlet _figlet = new Figlet(_font);

		private List<int> _numberList { get; set; }
		private int _resultIndex { get; set; }

		public SearchFirstRepeatedIndex()
		{
			CleanInput();
		}

		public override void Begin(bool onlyMessage = false)
		{
			Console.Clear();
			Console.WriteLine(_figlet.ToAscii("Buscador de Numeros"), ColorTranslator.FromHtml("#ffa500"));
			Console.WriteLine(_figlet.ToAscii("Repetidos"), ColorTranslator.FromHtml("#ffa500"));
			Console.WriteLine("Bem vindo ao buscador de números repetidos!", Color.PeachPuff);
			ReadData("Insira uma sequência de números inteiros, de -2147483647 a 2147483647, separados por vírgulas: ");
			
			if (!onlyMessage)
			{
				ProcessInput();
				Execute();
				ShowResult();
				RetryOrExit();
			}
		}

		public override void ProcessInput()
		{
			try
			{
				var inputList = Regex.Replace(_input, @"\s+", "").Split(',').ToList();
				_numberList = inputList.Select(int.Parse).ToList();
			}
			catch
			{
				ErroOnProcessInput("Erro ao processar Lista! Insira uma sequência válida de números inteiros!");
			}

		}

		public override void Execute()
		{
			var auxList = new List<int>();
			foreach(int num in _numberList)
			{
				if (!auxList.Any(n => n == num))
					auxList.Add(num);
				else
				{
					_resultIndex = _numberList.FindIndex(n => n == num);
					break;
				}
			}

			_success = _resultIndex != -1;
		}

		public override void ShowResult()
		{
			Console.WriteLine();
			if (_success)
			{
				Console.WriteLine($"Índice do primeiro item duplicado: {_resultIndex} ({_resultIndex+1}º posição)", Color.LightGreen);
				Console.WriteLine($"Valor: {_numberList.ElementAt(_resultIndex)}", Color.LightGreen);
			}
			else
				Console.WriteLine("Nenhum item duplicado encontrado!", Color.Red);

			Console.WriteLine("", Color.White);
			CleanInput();
		}

		public override void CleanInput()
		{
			_numberList = new List<int>();
			_resultIndex = -1;
			_success = false;
		}
	}
}
