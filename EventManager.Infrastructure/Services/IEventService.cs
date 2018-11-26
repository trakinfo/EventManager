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
		Task<EventDto> Get(long id);
		Task<IEnumerable<EventDto>> GetList(DateTime startDate, DateTime endDate,string name = null);
		Task CreateAsync(string name, string descripion, long? idLocation, DateTime startDate, DateTime endDate, string creator, string hostIP);
		Task<int> CreateTicketCollectionAsync(long eventId, int? startRange, int? endRange, long? sectorId, decimal? price, string creator, string hostIP);
		Task UpdateAsync(long id, string name, string description, long? idLocation, DateTime startDate, DateTime endDate, string modifier, string hostIP);
		Task<int> BuyTicketAsync(long[] id, string userName, DateTime purchaseDate);
		//Task DeleteTicketsAsync(ISet<Ticket> tickets);
		Task DeleteAsync(long id);
	}
}
