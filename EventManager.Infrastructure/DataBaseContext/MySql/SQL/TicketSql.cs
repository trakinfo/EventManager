using EventManager.Core.DataBaseContext.SQL;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Infrastructure.DataBaseContext.MySql.SQL
{
	public class TicketSql : ITicketSql
	{
		public string Select(long id)
		{
			return $"SELECT t.ID,t.SeatingNumber,t.Price,t.UserId,t.PurchaseDate,t.IdEvent,t.IdSector,t.User,t.HostIP,t.Version FROM ticket t INNER JOIN event e ON e.ID=t.IdEvent WHERE t.ID={id};";
		}

		//public string SelectMany(DateTime startDate, DateTime endDate)
		//{
		//	return $"SELECT t.ID,t.SeatingNumber,t.Price,t.UserId,t.PurchaseDate,t.IdEvent,t.IdSector,t.User,t.HostIP,t.Version FROM ticket t INNER JOIN event e ON e.ID=t.IdEvent WHERE e.StartDate BETWEEN '{startDate.ToShortDateString()}' AND '{endDate.ToShortDateString()}';";
		//}

		public string SelectMany(string name)
		{
			return $"SELECT t.ID,t.SeatingNumber,t.Price,t.UserId,t.PurchaseDate,t.IdEvent,t.IdSector,t.User,t.HostIP,t.Version FROM ticket t;";
		}

		public string Insert()
		{
			return "INSERT INTO ticket VALUES(null,@IdSector,@IdEvent,null,@SeatingNumber,@Price,null,@User,@HostIP,null);";
		}

		public string Update()
		{
			throw new NotImplementedException();
		}

		public string Delete()
		{
			throw new NotImplementedException();
		}
	}
}
