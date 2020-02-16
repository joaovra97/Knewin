using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CoonsoleAlgorithms.Algorithms
{
	public class SearchFirstRepeatedIndex : IAlgorithm
	{

		private string _input { get; set; }

		private bool _success { get; set; }


		private List<int> _numberList { get; set; }

		private int _resultIndex { get; set; }

		public SearchFirstRepeatedIndex()
		{
			_numberList = new List<int>();
			_resultIndex = -1;
		}

		public void Begin()
		{
			Console.WriteLine("Bem vindo ao buscador de números repetidos!");
			ReadData();
			ProcessInput();
			Execute();
			ShowResult();
			RetryOrExit();
		}

		public void ReadData()
		{
			Console.WriteLine("Insira uma sequência de números inteiros separados por vírgulas: ");
			_input = Console.ReadLine();
		}

		public void ProcessInput()
		{
			try
			{
				var inputList = Regex.Replace(_input, @"\s+", "").Split(',').ToList();
				_numberList = inputList.Select(int.Parse).ToList();
			}
			catch(Exception e)
			{
				ErroOnProcessInput();
			}

		}

		public void ErroOnProcessInput()
		{
			_numberList = new List<int>();
			Console.WriteLine("Erro ao processar Lista!");
			ReadData();
			ProcessInput();
		}

		public void Execute()
		{
			var auxList = new List<int>();
			foreach(int num in _numberList)
			{
				if (!auxList.Any(n => n == num))
					auxList.Add(num);
				else
					_resultIndex = _numberList.FindIndex(n => n == num);
			}

			_success = _resultIndex != -1;
		}

		public void ShowResult()
		{
			if (_success)
			{
				Console.WriteLine($"Índice do primeiro item duplicado: {_resultIndex}");
				Console.WriteLine($"Valor: {_numberList.ElementAt(_resultIndex)}");
			}
			else
				Console.WriteLine("Nenhum item duplicado encontrado!");			
		}

		public void RetryOrExit()
		{
			Console.WriteLine("Tentar novamente? (y/n)");
			var retry = Console.ReadLine() == "y";

			if (retry)
				Begin();
		}
	}
}
