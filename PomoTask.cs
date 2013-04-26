using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Pomoch
{
	public class PomoTaskList: IEnumerable<PomoTask>
	{
		List<PomoTask> tasks;
		int totalTaskId;
		int currentTaskId;

		public PomoTaskList()
		{
			tasks = new List<PomoTask>();
			totalTaskId = 0;
		}

		public IEnumerator<PomoTask> GetEnumerator()
		{
			return tasks.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return tasks.GetEnumerator();
		}

		public PomoTask CreatePomoTask(string content, int evaluated)
		{
			PomoTask ret = new PomoTask(currentTaskId++);
			ret.Create(content, DateTime.Now);
			ret.Evaluate(evaluated);
			return ret;
		}
	}

	public class PomoTask
	{
		public enum State
		{
			Invalid,
			Ready,
			Ongoing,
			Cancelled,
			Paused,
			Completed
		};

		private DateTime creationTime;
		private DateTime lastModifiedTime;

		private DateTime finishedTime;

		private int id;
		private UInt64 totalSeconds;
		private UInt64 estimatedSeconds;

		private int evaluatedPomo;
		private int currentPomo;

		private State state;

		private string content;

		public PomoTask(int id)
		{
			this.id = id;
			evaluatedPomo = 0;
			currentPomo = 0;
			state = State.Invalid;
			content = "";
		}

		public int ID
		{
			get { return id; }
		}

		public string Content
		{
			get { return content; }
		}

		public DateTime CreationTime
		{
			get { return creationTime; }
		}

		public DateTime LastModifiedTime
		{
			get { return lastModifiedTime; }
		}

		public int CurrentPomo
		{
			get { return currentPomo; }
		}

		public void Create(string content, DateTime time)
		{
			this.content = content;
			this.creationTime = time;
		}

		public void Elapse(int second)
		{
		}

		public void Modify(string content, DateTime time)
		{
			this.content = content;
			lastModifiedTime = time;
		}

		public void Evaluate(int evaluated)
		{
			this.evaluatedPomo = evaluated;
		}

		public State CurrentState
		{
			get { return state; }
		}
	}
}
