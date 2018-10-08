using EventManager.Core.Domain;
using EventManager.Core.Globals;
using EventManager.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Infrastructure.Services
{
    public interface IEventService
    {
		Task<EventDto> GetAsync(long id);
		Task<IEnumerable<EventDto>> GetListAsync(string name = null);
		Task CreateAsync(string name, string descripion, long? idLocation, DateTime startDate, DateTime endDate, string creator, string hostIP);
		Task<int> CreateTicketCollectionAsync(long eventId);
		Task UpdateAsync(long id, string name, string description, long? idLocation, DateTime startDate, DateTime endDate, string modifier, string hostIP);
		//Task DeleteTicketsAsync(ISet<Ticket> tickets);
		Task DeleteAsync(long id);
	}
}
