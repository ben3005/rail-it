using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace rail_it
{
	public class TaskRunner
	{
		public static void runTask(Action theMethod, int totalRuns = 1, int threads = 1)
		{
			if (totalRuns <= 0)
			{
				throw new ArgumentException("Task must run at least once");
			}
			if (threads <= 0)
			{
				throw new ArgumentException("Task must run on at least one thread");
			}

			var currentScheduler = new LimitedConcurrencyLevelTaskScheduler(threads);
			var taskFactory = new TaskFactory(currentScheduler);
			CancellationTokenSource cts = new CancellationTokenSource();

			object lockObj = new object();
			List<Task> tasks = new List<Task>();
			for (int j = 0; j < totalRuns; j++)
			{
				lock (lockObj)
				{
					tasks.Add(taskFactory.StartNew(() => theMethod(), cts.Token));
				}
			}
			Task.WaitAll(tasks.ToArray());
		}
	}
}
