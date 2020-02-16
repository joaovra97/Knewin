using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CoonsoleAlgorithms.Algorithms
{
	public class SearchFirstRepeatedIndex : Algorithm
	{

		private List<int> _numberList { get; set; }
		private int _resultIndex { get; set; }

		public SearchFirstRepeatedIndex()
		{
			CleanInput();
		}

		public override void Begin()
		{
			Console.WriteLine("Bem vindo ao buscador de números repetidos!");
			ReadData("Insira uma sequência de números inteiros separados por vírgulas: ");
			ProcessInput();
			Execute();
			ShowResult();
			RetryOrExit();
		}

		public override void ProcessInput()
		{
			try
			{
				var inputList = Regex.Replace(_input, @"\s+", "").Split(',').ToList();
				_numberList = inputList.Select(int.Parse).ToList();
			}
			catch(Exception e)
			{
				ErroOnProcessInput("Erro ao processar Lista!");
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
			if (_success)
			{
				Console.WriteLine($"Índice do primeiro item duplicado: {_resultIndex} ({_resultIndex+1}º posição)");
				Console.WriteLine($"Valor: {_numberList.ElementAt(_resultIndex)}");
			}
			else
				Console.WriteLine("Nenhum item duplicado encontrado!");			
		}

		public override void CleanInput()
		{
			_numberList = new List<int>();
			_resultIndex = -1;
			_success = false;
		}
	}
}
