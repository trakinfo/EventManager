using EventManager.Core.DataBaseContext;
using EventManager.Core.DataBaseContext.SQL;
using EventManager.Core.Domain;
using EventManager.Core.Globals;
using EventManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Infrastructure.Repository
{
	public class EventRepository : Repository<Event>, IEventRepository
	{
		readonly ILocationRepository _locationRepo;
		readonly ITicketRepository _ticketRepo;

		public EventRepository(ILocationRepository locationRepo, ITicketRepository ticketRepo, IDataBaseContext context, IEventSql eventSql) : base(context, eventSql)
		{
			_locationRepo = locationRepo;
			_ticketRepo = ticketRepo;
		}

		public void CreateSelectParams(IDbCommand cmd)
		{
			cmd.Parameters.Add(dbContext.CreateParameter("@StartDate", DbType.DateTime, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@EndDate", DbType.DateTime, cmd));
		}

		public void CreateInsertParams(IDbCommand cmd)
		{
			cmd.Parameters.Add(dbContext.CreateParameter("@Name", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@Description", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@IdLocation", DbType.Int64, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@StartDate", DbType.DateTime, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@EndDate", DbType.DateTime, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@User", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@HostIP", DbType.String, cmd));
		}

		public void CreateUpdateParams(IDbCommand cmd)
		{
			cmd.Parameters.Add(dbContext.CreateParameter("@ID", DbType.Int64, cmd));
			CreateInsertParams(cmd);
		}

		public void CreateDeleteParams(IDbCommand cmd)
		{
			cmd.Parameters.Add(dbContext.CreateParameter("@ID", DbType.Int64, cmd));
		}

		public Event CreateEvent(IDataReader R)
		{
			var idEvent = Convert.ToInt64(R["ID"]);
			Location location = null;

			if (!string.IsNullOrEmpty(R["IdLocation"].ToString()))
			{
				location = _locationRepo.GetAsync(Convert.ToInt64(R["IdLocation"]), _locationRepo.CreateLocation).Result;
				var tickets = GetTicketList(idEvent);
				foreach (var s in location.Sectors)
					s.Tickets = tickets.Where(t => t.SectorId == s.Id);
			}

			return new Event
				(
					idEvent,
					R["Name"].ToString(),
					R["Description"].ToString(),
					location,
					Convert.ToDateTime(R["StartDate"]),
					Convert.ToDateTime(R["EndDate"]),
					new Signature(R["User"].ToString(), R["HostIP"].ToString(), Convert.ToDateTime(R["Version"]))
				);
		}

		IEnumerable<Ticket> GetTicketList(long idEvent)
		{
			var sqlParamValue = new object[] { idEvent };
			var tickets = _ticketRepo.GetListAsync(idEvent, _ticketRepo.CreateTicket).Result;
			return tickets;
		}



		//Ticket GetTicket(IDataReader R)
		//{
		//	return new Ticket(Convert.ToInt64(R["ID"]), Convert.ToInt32(R["SeatingNumber"]), Convert.ToDecimal(R["Price"]), null);
		//}
	}
}
