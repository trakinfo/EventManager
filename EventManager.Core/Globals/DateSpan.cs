using System;

namespace EventManager.Core.Globals
{
	public class DateSpan
	{
		public DateTime Start { get; } 
		public DateTime End { get; } 
		public DateSpan()
		{
			Start = DateTime.Now;
			End = DateTime.MaxValue;
		}
		public DateSpan(DateTime startDate, DateTime endDate)
		{
			Start = startDate;
			End = endDate;
		}
	}
}
