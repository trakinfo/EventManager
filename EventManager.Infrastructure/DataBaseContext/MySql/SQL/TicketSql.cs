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
			return $"SELECT t.ID,t.SeatingNumber,t.Price,t.IdEvent,t.IdSector,t.User,t.HostIP,t.Version FROM ticket t WHERE t.ID={id};";
		}

		public string SelectMany(string name)
		{
			//return $"SELECT t.ID,t.SeatingNumber,t.Price,t.UserId,t.PurchaseDate,t.IdEvent,t.IdSector,t.User,t.HostIP,t.Version FROM ticket t WHERE t.UserId LIKE '{name}%';";
			return $"SELECT t.ID,t.SeatingNumber,t.Price,pt.IdUser,pt.PurchaseDate,pt.PaymentStatus,t.IdEvent,t.IdSector,t.User,t.HostIP,t.Version FROM ticket t LEFT JOIN purchasedTicket pt ON pt.IdTicket=t.ID;";
		}

		public string SelectMany(long idEvent)
		{
			return $"SELECT t.ID,t.SeatingNumber,t.Price,pt.IdUser,pt.PurchaseDate,pt.PaymentStatus,t.IdSector,t.User,t.HostIP,t.Version FROM ticket t LEFT JOIN purchasedTicket pt ON pt.IdTicket=t.ID WHERE t.IdEvent={idEvent};";
		}

		public string Insert()
		{
			return "INSERT INTO ticket VALUES(null,@IdSector,@IdEvent,@SeatingNumber,@Price,@User,@HostIP,null);";
		}

		public string Update()
		{
			throw new NotImplementedException();
		}

		public string Delete()
		{
			throw new NotImplementedException();
		}

		public string Puchase()
		{
			//return $"UPDATE ticket SET UserId=@UserName, PurchaseDate=@PurchaseDate WHERE ID=@ID;";
			return $"INSERT INTO purchasedTicket VALUES(@ID,@UserName,@PurchaseDate,1,@User,@HostIP,null);";
		}
	}
}
