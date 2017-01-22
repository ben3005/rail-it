using System;
using System.Threading;

namespace rail_it
{
	class Program
	{
		static void Main(string[] args)
		{
			var taskRunner = new TaskRunner();
			taskRunner.runTask(tmpTask, 20);
			Console.ReadKey();
		}

		static void tmpTask(int runNumber)
		{
			Console.WriteLine("run number:{0} running on thread {1} ", runNumber, Thread.CurrentThread.ManagedThreadId);
		}
	}
}
