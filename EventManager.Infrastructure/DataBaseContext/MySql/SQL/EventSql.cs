using EventManager.Core.DataBaseContext.SQL;

namespace EventManager.Infrastructure.DataBaseContext.MySql.SQL
{
	public class EventSql : IEventSql
    {
		public string SelectEvents(string name)
		{
			return $"SELECT e.ID,e.Name,e.Description,e.IdLocation,e.StartDate,e.EndDate,e.User,e.HostIP,e.Version FROM `event` e WHERE e.Name LIKE '{name}%' ORDER BY e.StartDate DESC,e.Name;";
		}
		public string SelectEvent(ulong id)
		{
			return $"SELECT e.ID,e.Name,e.Description,e.IdLocation,e.StartDate,e.EndDate,e.User,e.HostIP,e.Version FROM `event` e WHERE e.ID={id};";
		}

		public string SelectEvent(string name)
		{
			return $"SELECT e.ID,e.Name,e.Description,e.IdLocation,e.StartDate,e.EndDate,e.User,e.HostIP,e.Version FROM `event` e WHERE e.Name='{name}';";
		}

		public string SelectTicket(ulong idEvent, ulong idSector)
		{
			return $"SELECT t.ID,t.SeatingNumber,t.Price,IdSector FROM ticket t WHERE t.IdEvent={idEvent} AND t.IdSector={idSector};";
		}
		public string InsertEvent()
		{
			return "INSERT INTO event VALUES(null,?Name,?Description,?IdLocation,?StartDate,?EndDate,?User,?HostIP,NULL);";
		}

		public string UpdateEvent()
		{
			return "UPDATE event SET Name=?Name, Description=?Description, StartDate=?StartDate, EndDate=?EndDate, User=?User, HostIP=?HostIP WHERE ID=?ID;";
		}

		public string UpdateEventLocation()
		{
			return "UPDATE event SET IdLocation=?IdLocation, User=?User, HostIP=?HostIP WHERE ID=?ID;";
		}

		public string DeleteEvent()
		{
			return "DELETE FROM event WHERE ID=?ID";
		}
	}
}
