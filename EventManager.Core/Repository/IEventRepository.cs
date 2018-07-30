using EventManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Core.Repository
{
    public interface IEventRepository
    {
		Task<Event> GetEventAsync(ulong eventId);
		Task<Event> GetEventAsync(string name);
		Task<IEnumerable<Event>> GetEventListAsync(string name="");
		Task<long> AddEventAsync(IDictionary<string,object> sqlParams);
		Task UpdateEventAsync(Event @event);
		Task DeleteEventAsync(ulong eventId);
    }
}
