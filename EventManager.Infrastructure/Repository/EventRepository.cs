using EventManager.Core.DataBaseContext;
using EventManager.Core.Domain;
using EventManager.Core.Globals;
using EventManager.Core.Repository;
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
		public EventRepository(IDataBaseContext context)
		{
			dbContext = context;
		}
		public async Task<Event> GetEventAsync(long eventId)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Event>> GetEventListAsync(string name = "")
		{
			var eventSet = await dbContext.FetchDataSetAsync(EventSql.SelectEvent(name));

			var events = new HashSet<Event>();
			foreach (var E in eventSet)
			{
				var idEvent = Convert.ToUInt32(E["ID"]);
				events.Add(new Event
					(
						idEvent,
						E["Name"].ToString(),
						E["Description"].ToString(),
						await GetLocationAsync(idEvent),
						Convert.ToDateTime(E["StartDate"]),
						Convert.ToDateTime(E["EndDate"]),
						new Signature(E["User"].ToString(), E["HostIP"].ToString(), Convert.ToDateTime(E["Version"]))
					)
					);
			}
			return await Task.FromResult(events.AsEnumerable());
		}
		async Task<Location> GetLocationAsync(ulong idEvent)
		{
			var locationDR = await dbContext.FetchDataRowAsync(EventSql.SelectLocation(idEvent));
			var idLocation = Convert.ToUInt64(locationDR["ID"]);
			var location = new Location()
			{
				Name = locationDR["Name"].ToString(),
				Email = locationDR["Email"].ToString(),
				PhoneNumber = locationDR["PhoneNumber"].ToString(),
				WWW = locationDR["www"].ToString(),
				Sectors = await GetSectorList(idEvent, idLocation),
				Address = await GetLocationAddressAsync(idLocation)
			};
			return await Task.FromResult(location);
		}

		async Task<Address> GetLocationAddressAsync(ulong idLocation)
		{
			var addressDR = await dbContext.FetchDataRowAsync(EventSql.SelectAddress(idLocation));
			var address = new Address()
			{
				PlaceName = addressDR["PlaceName"].ToString(),
				StreetName = addressDR["StreetName"].ToString(),
				PropertyNumber = addressDR["PropertyNumber"].ToString(),
				ApartmentNumber = addressDR["ApartmentNumber"].ToString(),
				PostalCode = addressDR["PostalCode"].ToString(),
				PostOffice = addressDR["PostOffice"].ToString()

			};
			return await Task.FromResult(address);
		}

		async Task<ISet<Sector>> GetSectorList(ulong idEvent, ulong idLocation)
		{
			var sectorSet = dbContext.FetchDataSetAsync(EventSql.SelectSector(idLocation));
			var sectors = new HashSet<Sector>();

			foreach (var S in await sectorSet)
			{
				var idSector = (uint)S["ID"];

				sectors.Add(new Sector(Convert.ToUInt64(S["ID"]), S["Name"].ToString(), S["Description"].ToString(), Convert.ToUInt32(S["SeatingCount"]), await GetTicketListAsync(idEvent, idSector)));
			}
			return await Task.FromResult(sectors);
		}
		async Task<ISet<Ticket>> GetTicketListAsync(ulong idEvent, ulong idSector)
		{
			var ticketSet = dbContext.FetchDataSetAsync(EventSql.SelectTicket(idEvent, idSector));
			var tickets = new HashSet<Ticket>();
			foreach (var T in ticketSet.Result)
			{
				tickets.Add(new Ticket(Convert.ToUInt64(T["ID"]), Convert.ToInt32(T["SeatingNumber"]), Convert.ToDecimal(T["Price"]), null));
			}
			return await Task.FromResult(tickets);
		}
		public async Task AddEventAsync(Event @event)
		{
			throw new NotImplementedException();
		}

		public async Task UpdateEventAsync(Event @event)
		{
			throw new NotImplementedException();
		}

		public async Task DeleteEventAsync(Guid eventId)
		{
			throw new NotImplementedException();
		}
	}
}
