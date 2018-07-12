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
			var events = dbContext.FetchDataAsync(EventSql.SelectEvent()).Result;

			var eventSet = new HashSet<Event>();
			foreach (var E in events)
			{
				eventSet.Add(new Event
					(
						Convert.ToInt64(E["ID"]),
						E["Name"].ToString(),
						E["Description"].ToString(),
						new Location
						{
							Name =E["LocationName"].ToString(),
							Email =E["Email"].ToString(),
							PhoneNumber =E["PhoneNumber"].ToString(),
							WWW =E["Www"].ToString(),
							Sectors = { new Sector(1, "A", "VIP", 10, null) }
						},
						Convert.ToDateTime(E["StartDate"]),
						Convert.ToDateTime(E["EndDate"]),
						new Signature(E["User"].ToString(), E["HostIP"].ToString(), Convert.ToDateTime(E["Version"]))
					)
					);

			}
			return await Task.FromResult(eventSet.AsEnumerable());
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
