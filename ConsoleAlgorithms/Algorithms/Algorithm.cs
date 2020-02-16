using System;
using System.Drawing;
using Console = Colorful.Console;

namespace CoonsoleAlgorithms.Algorithms
{
	public abstract class Algorithm
	{
		#region fields
		protected string _input { get; set; }
		protected bool _success { get; set; }
		protected string _readDataMessage { get; set; }
		#endregion

		#region abstract functions
		public abstract void Begin(bool onlyMessage = false);
		public abstract void ProcessInput();
		public abstract void Execute();
		public abstract void ShowResult();
		public abstract void CleanInput();
		#endregion

		#region regular functions
		public void ReadData(string message)
		{
			_readDataMessage = message;
			Console.WriteLine(message);
			_input = Console.ReadLine();
		}

		public void ErroOnProcessInput(string message)
		{
			CleanInput();
			Console.WriteLine();
			Console.WriteLine(message, Color.Red);
			Console.WriteLine("Pressione qualquer botão para continuar...", Color.Gray);
			Console.ReadKey();
			Begin(onlyMessage: true);
			ProcessInput();
		}		

		public void RetryOrExit()
		{
			Console.WriteLine("Tentar novamente? (y/n)", Color.Gray);
			var retry = Console.ReadKey().KeyChar == 'y';
			Console.WriteLine();

			if (retry)
				Begin();
		}
		#endregion
	}
}
