using Colorful;
using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using Console = Colorful.Console;

namespace CoonsoleAlgorithms.Algorithms
{
	public class ConfirmPalindrome : Algorithm
	{
		public static FigletFont _font = FigletFont.Load("../../fonts/mini.flf");
		public static Figlet _figlet = new Figlet(_font);

		private string _inputWithoutSpace { get; set; }

		public ConfirmPalindrome()
		{
			CleanInput();
		}

		public override void Begin(bool onlyMessage = false)
		{
			Console.Clear();
			Console.WriteLine(_figlet.ToAscii("Verificador de Palindromos"), ColorTranslator.FromHtml("#0000ff"));
			Console.WriteLine("Bem vindo ao verificador de palíndromos!", Color.SkyBlue);
			Console.WriteLine("Um palíndromo é uma frase ou palavra que se pode ler, indiferentemente, da esquerda para a direita ou vice-versa", Color.SkyBlue);
			ReadData("Insira uma string: ");

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
			if(!string.IsNullOrEmpty(_input))
				_inputWithoutSpace = Regex.Replace(_input, @"\s+", "").ToLower();
			else
				ErroOnProcessInput("Erro ao processar string! Insira ao menos um caractere!");
		}

		public override void Execute()
		{
			_success = _inputWithoutSpace.SequenceEqual(_inputWithoutSpace.Reverse());
		}

		public override void ShowResult()
		{
			Console.WriteLine();
			if (_success)
				Console.WriteLine("O termo é um palíndromo!", Color.LightGreen);
			else
				Console.WriteLine("O termo não é um palíndromo!", Color.Red);

			Console.WriteLine("", Color.White);
			CleanInput();
		}

		public override void CleanInput()
		{
			_inputWithoutSpace = String.Empty;
			_success = false;
		}
	}
}
