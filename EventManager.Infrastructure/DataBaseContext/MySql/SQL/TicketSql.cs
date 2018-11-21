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

		public string SelectMany(string name)
		{
			return $"SELECT t.ID,t.SeatingNumber,t.Price,t.UserId,t.PurchaseDate,t.IdEvent,t.IdSector,t.User,t.HostIP,t.Version FROM ticket t WHERE t.UserId LIKE '{name}%';";
		}

		public string SelectMany(long idEvent)
		{
			return $"SELECT t.ID,t.SeatingNumber,t.Price,t.UserId,t.PurchaseDate,t.IdSector,t.User,t.HostIP,t.Version FROM ticket t WHERE t.IdEvent={idEvent};";
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
