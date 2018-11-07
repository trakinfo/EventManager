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
			RefreshRepo();
		}

		private void RefreshRepo()
		{
			contentList = GetListAsync(null, CreateEvent).Result;
		}

		public async Task<IEnumerable<Event>> GetList(DateTime startDate, DateTime endDate, string name)
		{
			return await Task.FromResult(contentList.Where(e => e.Name.StartsWith(name)).Where(e => e.StartDate >= startDate && e.EndDate <= endDate));
		}

		public async Task<Event> Get(long id)
		{
			return await Task.FromResult(contentList.Where(e => e.Id == id).FirstOrDefault());
		}

		//async Task<Location> GetLocationAsync(long idEvent, long idLocation)
		//{
		//	var location = await locationRepository.GetAsync(idLocation, locationRepository.CreateLocation);

		//	foreach (var S in location.Sectors)
		//	{
		//		S.Tickets = await GetTicketListAsync(idEvent, S.Id);
		//	}
		//	return await Task.FromResult(location);
		//}

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

		//public async Task<int> AddTickets(object[] sqlParamValue, int seatingCount)
		//{
		//	var HS = new HashSet<object[]>();

		//	for (int i = 0; i < seatingCount; i++)
		//	{
		//		sqlParamValue[3] = i + 1;
		//		HS.Add(sqlParamValue.ToArray());
		//	}
		//	return await dbContext.AddManyRecordsAsync((sql as IEventSql).InsertTicket(), HS, CreateTicketParams);
		//}

		//private void CreateTicketParams(IDbCommand cmd)
		//{
		//	cmd.Parameters.Add(dbContext.CreateParameter("@IdEvent", DbType.Int64, cmd));
		//	cmd.Parameters.Add(dbContext.CreateParameter("@IdSector", DbType.Int64, cmd));
		//	cmd.Parameters.Add(dbContext.CreateParameter("@Price", DbType.Decimal, cmd));
		//	cmd.Parameters.Add(dbContext.CreateParameter("@SeatingNumber", DbType.Int32, cmd));
		//}

		public Event CreateEvent(IDataReader R)
		{
			var idEvent = Convert.ToInt64(R["ID"]);
			Location location=null;

			if (!string.IsNullOrEmpty(R["IdLocation"].ToString()))
			{
				var l = _locationRepo.GetLocation(Convert.ToInt64(R["IdLocation"])).Result;
				var sectors = new List<Sector>();
				foreach (var s in l.Sectors)
				{
					var sector = new Sector(s.Id, s.Name, s.Description, s.SeatingRangeStart, s.SeatingRangeEnd, s.SeatingPrice, s.LocationId, s.Creator);
					sectors.Add(sector);
				}
				foreach (var s in sectors)
					s.Tickets = GetTicketList(idEvent, s.Id);
				location = new Location(l.Id,l.Name,l.Address,sectors,l.PhoneNumber,l.Email,l.WWW,l.Creator);
				
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

		IEnumerable<Ticket> GetTicketList(long idEvent, long idSector)
		{
			var tickets = _ticketRepo.GetTicketList(idEvent, idSector).Result;
			return tickets;
		}

		//Ticket GetTicket(IDataReader R)
		//{
		//	return new Ticket(Convert.ToInt64(R["ID"]), Convert.ToInt32(R["SeatingNumber"]), Convert.ToDecimal(R["Price"]), null);
		//}
	}
}
