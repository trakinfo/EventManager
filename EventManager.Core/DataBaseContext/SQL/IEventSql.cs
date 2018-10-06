namespace EventManager.Core.DataBaseContext.SQL
{
	public interface IEventSql : ISql
    {
		string SelectTicket(ulong idEvent, ulong idSector);
		string InsertTicket();
		string UpdateEventLocation();
	}
}
