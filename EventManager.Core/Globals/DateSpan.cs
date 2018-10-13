using System;

namespace EventManager.Core.Globals
{
	public class DateSpan
	{
		public DateTime Start { get; } = DateTime.Now;
		public DateTime End { get; } = DateTime.MaxValue;
		public DateSpan(DateTime startDate, DateTime endDate)
		{
			Start = startDate;
			End = endDate;
		}
	}
}
