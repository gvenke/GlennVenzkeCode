using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intervals
{
	class Program
	{
		static void Main(string[] args)
		{
			// BIG SCENARIO
			//var intervals = new List<Interval>{
			//	new Interval(1,2),
			//	new Interval(3,5),
			//	new Interval(6,7),
			//	new Interval(8,10),
			//	new Interval(12,16)
			//};
			//var newInterval = new Interval(4, 8);

			// SMALL SCENARIO
			//var intervals = new List<Interval>{
			//	new Interval(1,3),
			//	new Interval(6,9)
			//};
			//var newInterval = new Interval(2, 5);

			//// NEW INTERVAL < LOWERBOUND
			//var intervals = new List<Interval>{
			//	new Interval(1,5)
			//};
			//var newInterval = new Interval(0, 3);

			// NEW INTERVAL > UPPERBOUND
			var intervals = new List<Interval>{
				new Interval(1,5)
			};
			var newInterval = new Interval(5, 12);

			// NO MERGE SCENARIO
			//var intervals = new List<Interval>{
			//	new Interval(1,2),
			//	new Interval(3,5),
			//	new Interval(6,9),
			//	new Interval(14, 16)
			//};
			//var newInterval = new Interval(10, 12);

			var writer = new StringBuilder();
			string comma = "";
			foreach (Interval curInterval in Solution.Insert(intervals, newInterval)) {
				writer.Append($"{comma}[{curInterval.Start},{curInterval.End}]");
				comma = ",";
			}
			Console.WriteLine(writer.ToString());
			Console.ReadKey();
		}
	}

	class Interval
	{
		public Interval(int start, int end)
		{
			Start = start;
			End = end;
		}
		public int Start {get; set;}
		public int End {get; set;}
	}

	static class Solution
	{
		public static IList<Interval> Insert(IList<Interval> intervals, Interval newInterval)
		{
			var solution  = new List<Interval>();
			Interval mergedInterval;
			int replaceIndex;

			var overLaps = intervals.Where(o => newInterval.Start <= o.End && o.Start <= newInterval.End).ToList();;			

			if (overLaps.Count > 0) {
				overLaps.Add(newInterval);
				int min = overLaps.Min(o => o.Start);
				int max = overLaps.Max(o => o.End);
			    mergedInterval = new Interval(min, max);
				replaceIndex = intervals.IndexOf(overLaps.First(o => o.Start >= min));
				intervals.Insert(replaceIndex, mergedInterval);				
				solution =  intervals.Where( o => !overLaps.Contains(o) || o.Equals(mergedInterval)).ToList();
			} else {
				replaceIndex = intervals.IndexOf(intervals.Where(o => o.End <= newInterval.Start).LastOrDefault()) + 1;
				intervals.Insert(replaceIndex, newInterval);
				solution = intervals.ToList();
			}
			return solution;
		}
			
	}
}
