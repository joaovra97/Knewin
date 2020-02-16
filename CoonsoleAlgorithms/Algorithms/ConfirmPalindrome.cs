using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CoonsoleAlgorithms.Algorithms
{
	public class ConfirmPalindrome : Algorithm
	{
		private string _inputWithoutSpace { get; set; }

		public ConfirmPalindrome()
		{
			CleanInput();
		}

		public override void Begin()
		{
			Console.WriteLine("Bem vindo ao verificador de palíndromos!");
			ReadData("Insira uma string: ");
			ProcessInput();
			Execute();
			ShowResult();
			RetryOrExit();
		}

		public override void ProcessInput()
		{			
			_inputWithoutSpace = Regex.Replace(_input, @"\s+", "");
		}

		public override void Execute()
		{
			_success = _inputWithoutSpace.SequenceEqual(_inputWithoutSpace.Reverse());
		}

		public override void ShowResult()
		{
			if (_success)
				Console.WriteLine("O termo é um palíndromo!");
			else
				Console.WriteLine("O termo NÃO é um palíndromo!");
		}

		public override void CleanInput()
		{
			_inputWithoutSpace = String.Empty;
			_success = false;
		}
	}
}
