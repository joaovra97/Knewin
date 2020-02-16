using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoonsoleAlgorithms.Algorithms
{
	public interface IAlgorithm
	{
		void Begin();
		void ReadData();
		void ProcessInput();
		void ErroOnProcessInput();
		void Execute();
		void ShowResult();
		void RetryOrExit();
	}
}
