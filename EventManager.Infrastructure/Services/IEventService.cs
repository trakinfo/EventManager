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
		Task<EventDto> GetAsync(ulong id);
		Task<EventDto> CreateAsync(string name, Location location, DateTime startDate, DateTime endDate, Signature creator);
		Task<IEnumerable<EventDto>> BrowseAsync(string name = null);
		Task<Ticket> CreateTicketAsync(int seatingNumber, Sector sector, decimal price, Signature creator);
		Task<Sector> CreateSectorAsync(string name, string description, int seatingCount, Signature creator);
		Task<ISet<Ticket>> CreateTicketsCollectionAsync();
		Task UpdateAsync(string name, string description, Location location, DateTime startDate, DateTime endDate, Signature modifier);
		Task DeleteTicketsAsync(ISet<Ticket> tickets);
		Task DeleteAsync(ulong id);
    }
}
