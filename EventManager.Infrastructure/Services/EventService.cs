using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventManager.Core.Domain;
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

		public EventDto Get(long id)
		{
			try
			{
				//var _event = await _eventRepository.GetAsync(id, _eventRepository.CreateEvent);
				var _event = _eventRepository.Get(id);
				return _mapper.Map<EventDto>(_event);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				throw;
			}
		}

		public IEnumerable<EventDto> GetList(string name = null)
		{
			try
			{
				//var events = await _eventRepository.GetListAsync(name, _eventRepository.CreateEvent);
				var events = _eventRepository.GetList(name);
				return _mapper.Map<IEnumerable<EventDto>>(events);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				throw;
			}
		}

		public async Task CreateAsync(string name, string description, long? idLocation, DateTime startDate, DateTime endDate, string creator, string hostIP)
		{
			try
			{
				var sqlParamValue = new object[] { name, description, idLocation, startDate, endDate, creator, hostIP };
				await _eventRepository.AddAsync(sqlParamValue, _eventRepository.CreateInsertParams);
			}

			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}


		public async Task<int> CreateTicketCollectionAsync(long eventId)
		{
			int ticketCount = 0;
			try
			{
				var _event = await _eventRepository.GetAsync(eventId,_eventRepository.CreateEvent);
				if (_event.Location == null || _event.Location.Sectors == null) return 0;

				var HS = new HashSet<Ticket>();
				foreach (var S in _event.Location.Sectors)
				{
					var sqlParamValue = new object[4] { eventId, S.Id, S.SeatingPrice, null };
					ticketCount += await _eventRepository.AddTickets(sqlParamValue, S.SeatingCount);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return ticketCount;
		}

		public async Task DeleteAsync(long id)
		{
			try
			{
				var sqlParamValue = new object[1] { id };

				await _eventRepository.DeleteAsync(sqlParamValue,_eventRepository.CreateDeleteParams);
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

		public async Task UpdateAsync(long id, string name, string description, long? idLocation, DateTime startDate, DateTime endDate, string modifier, string hostIP)
		{
			try
			{
				var SqlParams = new object[] { id, name, description, idLocation, startDate, endDate, modifier, hostIP };
				await _eventRepository.UpdateAsync(SqlParams,_eventRepository.CreateUpdateParams);
			}

			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
