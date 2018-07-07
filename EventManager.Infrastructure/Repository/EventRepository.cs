using EventManager.Core.DataBaseContext;
using EventManager.Core.Domain;
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
		public async Task<Event> GetAsync(Guid eventId)
		{
			throw new NotImplementedException();
		}

		public async Task<Event> GetAsync(string name)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Event>> BrowseAsync(string name = "")
		{
			var events = dbContext.FetchDataAsync(EventSql.SelectStudent("1","2017/2018")).Result;

			var eventSet = new HashSet<Event>();
			foreach (var E in events)
			{
				eventSet.Add(new Event
					(
						Guid.NewGuid(),
						E["Imie"].ToString(),
						E["Nazwisko"].ToString(),
						null,
						DateTime.Now,
						DateTime.Now.AddDays(2),
						null,
						null)
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
