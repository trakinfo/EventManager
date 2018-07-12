using EventManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Core.Repository
{
    public interface IEventRepository
    {
		Task<Event> GetAsync(long eventId);
		Task<Event> GetAsync(string name);
		Task<IEnumerable<Event>> BrowseAsync(string name="");
		Task AddAsync(Event @event);
		Task UpdateAsync(Event @event);
		Task DeleteAsync(Guid eventId);
    }
}
