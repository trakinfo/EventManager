using System;

namespace EventManager.Core.DataBaseContext.SQL
{
	public interface IEventSql : ISql
    {
		//string SelectTicket(long idEvent, long idSector);
		//string InsertTicket();
		//string SelectMany(DateTime startDate, DateTime endDate, string name);
		string UpdateEventLocation();
	}
}
