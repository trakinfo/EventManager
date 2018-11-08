using EventManager.Core.DataBaseContext.SQL;
using System;

namespace EventManager.Infrastructure.DataBaseContext.MySql.SQL
{
	public class EventSql : IEventSql
    {
		public string Select(long id)
		{
			return $"SELECT e.ID,e.Name,e.Description,e.IdLocation,e.StartDate,e.EndDate,e.User,e.HostIP,e.Version FROM `event` e WHERE e.ID={id};";
		}

		public string SelectMany(string name)
		{
			return $"SELECT e.ID,e.Name,e.Description,e.IdLocation,e.StartDate,e.EndDate,e.User,e.HostIP,e.Version FROM `event` e WHERE e.Name LIKE '{name}%' ORDER BY e.StartDate DESC,e.Name;";
		}

		public string SelectMany(DateTime startDate, DateTime endDate, string name)
		{
			return $"SELECT e.ID,e.Name,e.Description,e.IdLocation,e.StartDate,e.EndDate,e.User,e.HostIP,e.Version FROM `event` e WHERE e.Name LIKE '{name}%' AND e.StartDate >= '{startDate}' AND e.EndDate <= '{endDate}' ORDER BY e.StartDate DESC,e.Name;";
		}

		public string Insert()
		{
			return "INSERT INTO event VALUES(null,@Name,@Description,@IdLocation,@StartDate,@EndDate,@User,@HostIP,NULL);";
		}

		public string Update()
		{
			return "UPDATE event SET Name=@Name, Description=@Description, IdLocation=@IdLocation, StartDate=@StartDate, EndDate=@EndDate, User=@User, HostIP=@HostIP WHERE ID=@ID;";
		}

		public string Delete()
		{
			return "DELETE FROM event WHERE ID=@ID";
		}

		//public string SelectTicket(long idEvent, long idSector)
		//{
		//	return $"SELECT t.ID,t.SeatingNumber,t.Price,t.UserId,t.PurchaseDate,t.User,t.HostIP,t.Version FROM ticket t WHERE t.IdEvent={idEvent} AND t.IdSector={idSector};";
		//}

		//public string InsertTicket()
		//{
		//	return "INSERT INTO ticket VALUES(null,@IdSector,@IdEvent,null,@SeatingNumber,@Price,null,@User,@HostIP,null);";
		//}

		public string UpdateEventLocation()
		{
			return "UPDATE event SET IdLocation=@IdLocation, User=@User, HostIP=@HostIP WHERE ID=@ID;";
		}


	}
}
