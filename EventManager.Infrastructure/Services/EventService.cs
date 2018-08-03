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

		public async Task<long> CreateAsync(string name, string description, ulong? idLocation, DateTime startDate, DateTime endDate, string creator, string hostIP)
		{
			try
			{
				var SqlParams = new Dictionary<string, object>();
				
				SqlParams.Add("?Name", name);
				SqlParams.Add("?Description", description);
				SqlParams.Add("?IdLocation", idLocation);
				SqlParams.Add("?StartDate", startDate);
				SqlParams.Add("?EndDate", endDate);
				SqlParams.Add("?User", creator);
				SqlParams.Add("?HostIP", hostIP);

				return await _eventRepository.AddEventAsync(SqlParams);
			}

			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return await Task.FromResult(-1);
			}
		}

		//public Task<Ticket> CreateTicketAsync(int seatingNumber, ulong idSector, decimal price, string creator, string hostIP)
		//{
		//	throw new NotImplementedException();
		//}

		public Task<ISet<Ticket>> CreateTicketCollectionAsync(ulong eventId)
		{
			throw new NotImplementedException();
		}

		public async Task DeleteAsync(ulong id)
		{
			try
			{
				var SqlParams = new Dictionary<string, object>();

				SqlParams.Add("?ID", id);
				
				await _eventRepository.DeleteEventAsync(SqlParams);
			}

			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public Task DeleteTicketsAsync(ISet<Ticket> tickets)
		{
			throw new NotImplementedException();
		}



		public async Task UpdateAsync(ulong id, string name, string description, ulong? idLocation, DateTime startDate, DateTime endDate, string modifier, string hostIP)
		{
			try
			{
				var SqlParams = new Dictionary<string, object>();

				SqlParams.Add("?ID", id);
				SqlParams.Add("?Name", name);
				SqlParams.Add("?Description", description);
				SqlParams.Add("?IdLocation", idLocation);
				SqlParams.Add("?StartDate", startDate);
				SqlParams.Add("?EndDate", endDate);
				SqlParams.Add("?User", modifier);
				SqlParams.Add("?HostIP", hostIP);

				await _eventRepository.UpdateEventAsync(SqlParams);
			}

			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
