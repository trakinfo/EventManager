using EventManager.Infrastructure.DTO;
using EventManager.Infrastructure.Services.Generic;
using System.Data;
using System.Threading.Tasks;

namespace EventManager.Infrastructure.Services
{
	public interface IEventService : IService
    {
		EventDto GetEvent(IDataReader R);
		void CreateEventParams(IDbCommand cmd);
		void CreateUpdateParams(IDbCommand cmd);
		void CreateDeleteParams(IDbCommand cmd);
		//Task<EventDto> GetAsync(ulong id);
		//Task CreateAsync(string name, string descripion, ulong? idLocation, DateTime startDate, DateTime endDate, string creator, string hostIP);
		//Task<IEnumerable<EventDto>> GetListAsync(string name = null);
		Task<int> CreateTicketCollectionAsync(ulong eventId);
		//Task UpdateAsync(ulong id, string name, string description, ulong? idLocation, DateTime startDate, DateTime endDate, string modifier, string hostIP);
		//Task DeleteTicketsAsync(ISet<Ticket> tickets);
		//Task DeleteAsync(ulong id);
	}
}
