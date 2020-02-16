using System;

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
		public abstract void Begin();
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
			Console.WriteLine(message);
			ReadData(_readDataMessage);
			ProcessInput();
		}		

		public void RetryOrExit()
		{
			Console.WriteLine("Tentar novamente? (y/n)");
			var retry = Console.ReadLine() == "y";

			if (retry)
				Begin();
		}
		#endregion
	}
}
