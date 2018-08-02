using EventManager.Core.DataBaseContext;
using EventManager.Core.DataBaseContext.SQL;
using EventManager.Core.Domain;
using EventManager.Core.Globals;
using EventManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Infrastructure.Repository
{
	public class EventRepository : IEventRepository
	{
		IDataBaseContext dbContext;
		ILocationRepository locationRepository;
		IEventSql sql;

		public EventRepository(IDataBaseContext context, ILocationRepository locationRepo, IEventSql eventSql)
		{
			dbContext = context;
			locationRepository = locationRepo;
			sql = eventSql;
		}
		public async Task<Event> GetEventAsync(ulong eventId)
		{
			var eventDR = await dbContext.FetchDataRowAsync(sql.SelectEvent(eventId));
			var idEvent = Convert.ToUInt64(eventDR["ID"]);
			Location location = null;
			if (!string.IsNullOrEmpty(eventDR["IdLocation"].ToString()))
				location = await GetLocationAsync(idEvent, Convert.ToUInt64(eventDR["IdLocation"]));

			var _event = new Event
				(
					idEvent,
					eventDR["Name"].ToString(),
					eventDR["Description"].ToString(),
					location,
					Convert.ToDateTime(eventDR["StartDate"]),
					Convert.ToDateTime(eventDR["EndDate"]),
					new Signature(eventDR["User"].ToString(), eventDR["HostIP"].ToString(), Convert.ToDateTime(eventDR["Version"]))
				);
			return await Task.FromResult(_event);
		}

		public async Task<Event> GetEventAsync(string name)
		{
			var eventDR = await dbContext.FetchDataRowAsync(sql.SelectEvent(name));
			var idEvent = Convert.ToUInt64(eventDR["ID"]);
			Location location = null;
			if (!string.IsNullOrEmpty(eventDR["IdLocation"].ToString()))
				location = await GetLocationAsync(idEvent, Convert.ToUInt64(eventDR["IdLocation"]));

			var _event = new Event
				(
					idEvent,
					eventDR["Name"].ToString(),
					eventDR["Description"].ToString(),
					location,
					Convert.ToDateTime(eventDR["StartDate"]),
					Convert.ToDateTime(eventDR["EndDate"]),
					new Signature(eventDR["User"].ToString(), eventDR["HostIP"].ToString(), Convert.ToDateTime(eventDR["Version"]))
				);
			return await Task.FromResult(_event);
		}

		public async Task<IEnumerable<Event>> GetEventListAsync(string name = "")
		{
			var eventSet = await dbContext.FetchDataRowSetAsync(sql.SelectEvents(name));

			var events = new HashSet<Event>();
			foreach (var E in eventSet)
			{
				var idEvent = Convert.ToUInt64(E["ID"]);
				Location location = null;
				if (!string.IsNullOrEmpty(E["IdLocation"].ToString()))
					location = await GetLocationAsync(idEvent, Convert.ToUInt64(E["IdLocation"]));

				events.Add(new Event
					(
						idEvent,
						E["Name"].ToString(),
						E["Description"].ToString(),
						location,
						Convert.ToDateTime(E["StartDate"]),
						Convert.ToDateTime(E["EndDate"]),
						new Signature(E["User"].ToString(), E["HostIP"].ToString(), Convert.ToDateTime(E["Version"]))
					)
					);
			}
			return await Task.FromResult(events.AsEnumerable());
		}
		async Task<Location> GetLocationAsync(ulong idEvent, ulong idLocation)
		{
			var location = await locationRepository.GetAsync(idLocation);

			foreach (var S in location.Sectors)
			{
				S.Tickets = await GetTicketListAsync(idEvent, S.Id);
			}
			return await Task.FromResult(location);
		}

		async Task<ISet<Ticket>> GetTicketListAsync(ulong idEvent, ulong idSector)
		{
			var ticketSet = dbContext.FetchDataRowSetAsync(sql.SelectTicket(idEvent, idSector));
			var tickets = new HashSet<Ticket>();
			foreach (var T in ticketSet.Result)
			{
				tickets.Add(new Ticket(Convert.ToUInt64(T["ID"]), Convert.ToInt32(T["SeatingNumber"]), Convert.ToDecimal(T["Price"]), null));
			}
			return await Task.FromResult(tickets);
		}

		public async Task<long> AddEventAsync(IDictionary<string, object> sqlParams)
		{
			var newEventId = await dbContext.AddDataAsync(sqlParams, sql.InsertEvent());
			return await Task.FromResult(newEventId);
		}

		public async Task UpdateEventAsync(IDictionary<string, object> sqlParams)
		{
			await dbContext.ExecuteCommandAsync(sqlParams, sql.UpdateEvent());
		}

		public async Task DeleteEventAsync(IDictionary<string, object> sqlParams)
		{
			await dbContext.ExecuteCommandAsync(sqlParams, sql.DeleteEvent());
		}
	}
}
