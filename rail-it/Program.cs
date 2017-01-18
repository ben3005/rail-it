using System;
using System.Threading;

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
