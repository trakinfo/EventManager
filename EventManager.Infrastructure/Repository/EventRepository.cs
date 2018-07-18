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
		public async Task<Event> GetAsync(long eventId)
		{
			throw new NotImplementedException();
		}

		public async Task<Event> GetAsync(string name)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Event>> BrowseAsync(string name = "")
		{
			var eventSet = await dbContext.FetchDataAsync(EventSql.SelectEvent(name));

			var events = new HashSet<Event>();
			foreach (var E in eventSet)
			{
				var idEvent = Convert.ToUInt32(E["ID"]);
				var idLocation = Convert.ToUInt64(E["IdLocation"]);
				//todo: rozbić na mniejsze metody, kwerendy już są
				var sectorSet = dbContext.FetchDataAsync(EventSql.SelectSector(idLocation));
				var ticketSet = dbContext.FetchDataAsync(EventSql.SelectTicket(idEvent));

				var sectors = new HashSet<Sector>();

				foreach (var S in await sectorSet)
				{
					var tickets = new HashSet<Ticket>();
					var idSector = (uint)S["ID"];
					foreach (var T in ticketSet.Result.ToList().Where(x => (uint)x["IdSector"]==idSector))
					{
						tickets.Add(new Ticket(Convert.ToUInt64(T["ID"]), Convert.ToInt32(T["SeatingNumber"]), Convert.ToDecimal(T["Price"]), null));
					}
					sectors.Add(new Sector(Convert.ToUInt64(S["ID"]), S["Name"].ToString(), S["Description"].ToString(), Convert.ToUInt32(S["SeatingCount"]), tickets));
				}

				events.Add(new Event
					(
						idEvent,
						E["Name"].ToString(),
						E["Description"].ToString(),
						new Location
						{
							Name = E["LocationName"].ToString(),
							Email = E["Email"].ToString(),
							PhoneNumber = E["PhoneNumber"].ToString(),
							WWW = E["www"].ToString(),
							Sectors = sectors,
							Address = new Address
							{
								PlaceName = E["PlaceName"].ToString(),
								StreetName = E["StreetName"].ToString(),
								PropertyNumber = E["PropertyNumber"].ToString(),
								ApartmentNumber = E["ApartmentNumber"].ToString(),
								PostalCode = E["PostalCode"].ToString(),
								PostOffice = E["PostOffice"].ToString()
							}
						},
						Convert.ToDateTime(E["StartDate"]),
						Convert.ToDateTime(E["EndDate"]),
						new Signature(E["User"].ToString(), E["HostIP"].ToString(), Convert.ToDateTime(E["Version"]))
					)
					);

			}
			return await Task.FromResult(events.AsEnumerable());
		}

		public async Task AddAsync(Event @event)
		{
			throw new NotImplementedException();
		}

		public async Task UpdateAsync(Event @event)
		{
			throw new NotImplementedException();
		}

		public async Task DeleteAsync(Guid eventId)
		{
			throw new NotImplementedException();
		}
	}
}
