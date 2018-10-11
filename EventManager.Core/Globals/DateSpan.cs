using System;

namespace EventManager.Core.Globals
{
	public class DateSpan
	{
		public DateTime Start { get; }
		public DateTime End { get; }
		public DateSpan(DateTime startDate, DateTime endDate)
		{
			Start = startDate;
			End = endDate;
		}
	}
}
