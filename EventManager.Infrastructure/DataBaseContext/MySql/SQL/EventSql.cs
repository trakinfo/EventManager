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
			return $"SELECT e.ID,e.Name,e.Description,e.IdLocation,e.StartDate,e.EndDate,e.User,e.HostIP,e.Version FROM event e WHERE e.Name LIKE '{name}%' AND e.StartDate >= @startDate AND e.EndDate <= @endDate ORDER BY e.StartDate DESC,e.Name;";
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

		public string UpdateEventLocation()
		{
			return "UPDATE event SET IdLocation=@IdLocation, User=@User, HostIP=@HostIP WHERE ID=@ID;";
		}


	}
}
