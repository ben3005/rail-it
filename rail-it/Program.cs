using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace rail_it
{
	class Program
	{
		static void Main(string[] args)
		{
			TaskRunner.runTask(tmpTask, 20, 4);
			Console.ReadKey();
		}

		static void tmpTask()
		{
			Console.WriteLine("task running on thread {0} ", Thread.CurrentThread.ManagedThreadId);
		}
	}
}
