using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EventManager.Core.Domain;
using EventManager.Core.Globals;
using EventManager.Core.Repository;
using EventManager.Infrastructure.DTO;

namespace EventManager.Infrastructure.Services
{
	public class EventService : IEventService
	{
		readonly IEventRepository _eventRepository;
		readonly IMapper _mapper;

		public EventService(IEventRepository eventRepository, IMapper mapper)
		{
			_eventRepository = eventRepository;
			_mapper = mapper;
		}

		public async Task<EventDto> GetAsync(ulong id)
		{
			var @event = await _eventRepository.GetEventAsync(id);
			return _mapper.Map<EventDto>(@event);
		}

		public async Task<IEnumerable<EventDto>> BrowseAsync(string name = null)
		{
			var events = await _eventRepository.GetEventListAsync(name);
			return _mapper.Map<IEnumerable<EventDto>>(events);
		}

		public Task<EventDto> CreateAsync(string name, Location location, DateTime startDate, DateTime endDate, Signature creator)
		{
			throw new NotImplementedException();
		}

		public Task<Sector> CreateSectorAsync(string name, string description, int seatingCount, Signature creator)
		{
			throw new NotImplementedException();
		}

		public Task<Ticket> CreateTicketAsync(int seatingNumber, Sector sector, decimal price, Signature creator)
		{
			throw new NotImplementedException();
		}

		public Task<ISet<Ticket>> CreateTicketsCollectionAsync()
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(ulong id)
		{
			throw new NotImplementedException();
		}

		public Task DeleteTicketsAsync(ISet<Ticket> tickets)
		{
			throw new NotImplementedException();
		}



		public Task UpdateAsync(string name, string description, Location location, DateTime startDate, DateTime endDate, Signature modifier)
		{
			throw new NotImplementedException();
		}
	}
}
