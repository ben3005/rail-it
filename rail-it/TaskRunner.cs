using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace rail_it
{
	public class TaskRunner
	{
		private readonly TaskScheduler currentScheduler;
		private readonly TaskFactory taskFactory;
		private readonly CancellationTokenSource cts;
		private readonly object lockObj;
		public TaskRunner()
		{
			currentScheduler = TaskScheduler.Current;
			taskFactory = new TaskFactory(currentScheduler);
			cts = new CancellationTokenSource();
			lockObj = new object();
		}

		public TaskRunner(int maxConcurrency)
		{
			if (maxConcurrency < 1)
			{
				throw new ArgumentException("Maximum concurrency cannot be less than 1", "maxConcurrency");
			}
			currentScheduler = new LimitedConcurrencyLevelTaskScheduler(maxConcurrency);
			taskFactory = new TaskFactory(currentScheduler);
			cts = new CancellationTokenSource();
			lockObj = new object();
		}
		public void runTask(Action theMethod, int totalRuns = 1)
		{
			validateParameters(totalRuns);

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

		public void runTask(Action<int> theMethod, int totalRuns = 1)
		{
			validateParameters(totalRuns);

			List<Task> tasks = new List<Task>();
			for (int i = 0; i < totalRuns; i++)
			{
				var currTask = i + 1;
				lock (lockObj)
				{
					tasks.Add(taskFactory.StartNew(() => theMethod(currTask), cts.Token));
				}
			}
			Task.WaitAll(tasks.ToArray());
		}

		private void validateParameters(int totalRuns)
		{
			if (totalRuns <= 0)
			{
				throw new ArgumentException("Task must run at least once");
			}
		}
	}
}
