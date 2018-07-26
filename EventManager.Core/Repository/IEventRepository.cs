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
		Task<IEnumerable<Event>> GetEventListAsync(string name="");
		Task AddEventAsync(Event @event);
		Task UpdateEventAsync(Event @event);
		Task DeleteEventAsync(Guid eventId);
    }
}
