using EventManager.Core.DataBaseContext;
using EventManager.Core.Domain;
using EventManager.Core.Globals;
using EventManager.Core.Repository;
using EventManager.Infrastructure.DataBaseContext.MySql.SQL;
using EventManager.Infrastructure.DataBaseContext.SQL;
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

		public EventRepository(IDataBaseContext context, ILocationRepository locationRepo)
		{
			dbContext = context;
			locationRepository = locationRepo;
		}
		public async Task<Event> GetEventAsync(ulong eventId)
		{
			var eventDR = await dbContext.FetchDataRowAsync(EventSql.SelectEvent(eventId));
			var idEvent = Convert.ToUInt64(eventDR["ID"]);
			
			var _event = new Event
				(
					idEvent,
					eventDR["Name"].ToString(),
					eventDR["Description"].ToString(),
					await GetLocationAsync(idEvent, Convert.ToUInt64(eventDR["IdLocation"])),
					Convert.ToDateTime(eventDR["StartDate"]),
					Convert.ToDateTime(eventDR["EndDate"]),
					new Signature(eventDR["User"].ToString(), eventDR["HostIP"].ToString(), Convert.ToDateTime(eventDR["Version"]))
				);
			return await Task.FromResult(_event);
		}

		//public async Task<Event> GetEventAsync(string name)
		//{
		//	var eventDR = await dbContext.FetchDataRowAsync(EventSql.SelectEvent(name));
		//	var idEvent = Convert.ToUInt64(eventDR["ID"]);

		//	var _event = new Event
		//		(
		//			idEvent,
		//			eventDR["Name"].ToString(),
		//			eventDR["Description"].ToString(),
		//			await GetLocationAsync(idEvent, Convert.ToUInt64(eventDR["IdLocation"])),
		//			Convert.ToDateTime(eventDR["StartDate"]),
		//			Convert.ToDateTime(eventDR["EndDate"]),
		//			new Signature(eventDR["User"].ToString(), eventDR["HostIP"].ToString(), Convert.ToDateTime(eventDR["Version"]))
		//		);
		//	return await Task.FromResult(_event);
		//}

		public async Task<IEnumerable<Event>> GetEventListAsync(string name = "")
		{
			var eventSet = await dbContext.FetchDataRowSetAsync(EventSql.SelectEvents(name));

			var events = new HashSet<Event>();
			foreach (var E in eventSet)
			{
				var idEvent = Convert.ToUInt64(E["ID"]);
				ulong? idLocation =  E["IdLocation"]==null ? null : Convert.ToUInt64(E["IdLocation"]);
				events.Add(new Event
					(
						idEvent,
						E["Name"].ToString(),
						E["Description"].ToString(),
						await GetLocationAsync(idEvent, idLocation),
						Convert.ToDateTime(E["StartDate"]),
						Convert.ToDateTime(E["EndDate"]),
						new Signature(E["User"].ToString(), E["HostIP"].ToString(), Convert.ToDateTime(E["Version"]))
					)
					);
			}
			return await Task.FromResult(events.AsEnumerable());
		}
		async Task<Location> GetLocationAsync(ulong idEvent, ulong? idLocation)
		{
			if (String.IsNullOrEmpty(idLocation.ToString())) return await Task.FromResult(new Location());

			var location = await locationRepository.GetAsync(idLocation);

			foreach (var S in location.Sectors)
			{
				S.Tickets = await GetTicketListAsync(idEvent, S.Id);
			}
			return await Task.FromResult(location);
		}

		async Task<ISet<Ticket>> GetTicketListAsync(ulong idEvent, ulong idSector)
		{
			var ticketSet = dbContext.FetchDataRowSetAsync(EventSql.SelectTicket(idEvent, idSector));
			var tickets = new HashSet<Ticket>();
			foreach (var T in ticketSet.Result)
			{
				tickets.Add(new Ticket(Convert.ToUInt64(T["ID"]), Convert.ToInt32(T["SeatingNumber"]), Convert.ToDecimal(T["Price"]), null));
			}
			return await Task.FromResult(tickets);
		}

		public async Task<long> AddEventAsync(IDictionary<string,object> sqlParams)
		{
			var newEventId = await dbContext.AddDataAsync(sqlParams, EventSql.InsertEvent());


			return await Task.FromResult(newEventId);
		}

		public async Task UpdateEventAsync(Event @event)
		{
			throw new NotImplementedException();
		}

		public async Task DeleteEventAsync(ulong eventId)
		{
			throw new NotImplementedException();
		}
	}
}
