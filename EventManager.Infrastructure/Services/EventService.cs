using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventManager.Core.Domain;
using EventManager.Core.Repository;
using EventManager.Infrastructure.DTO;
namespace EventManager.Infrastructure.Services
{
	public class EventService : IEventService
	{
		readonly IEventRepository _eventRepo;
		readonly ITicketRepository _ticketRepo;
		readonly IMapper _mapper;

		public EventService(IEventRepository eventRepo, ITicketRepository ticketRepo, IMapper mapper)
		{
			_eventRepo = eventRepo;
			_ticketRepo = ticketRepo;
			_mapper = mapper;
		}

		public async Task<EventDto> Get(long id)
		{
			try
			{
				var _event = await _eventRepo.GetAsync(id, _eventRepo.CreateEvent);
				return _mapper.Map<EventDto>(_event);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				throw;
			}
		}

		public async Task<IEnumerable<EventDto>> GetList(DateTime startDate, DateTime endDate, string name = null)
		{
			try
			{
				var sqlParamValue = new object[] { startDate, endDate };
				var events = await _eventRepo.GetListAsync(name, sqlParamValue, _eventRepo.CreateSelectParams, _eventRepo.CreateEvent);
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
				await _eventRepo.AddAsync(sqlParamValue, _eventRepo.CreateInsertParams);
			}

			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public async Task<int> CreateTicketCollectionAsync(long eventId, int? startRange, int? endRange, long? sectorId, decimal? price, string creator, string hostIP)
		{
			int ticketCount = 0;
			try
			{
				var _event = await _eventRepo.GetAsync(eventId, _eventRepo.CreateEvent);
				if (_event.Location == null || _event.Location.Sectors == null) return 0;

				if (sectorId != null)
				{
					if (_event.Location.Sectors.Count(s => s.Id == sectorId) > 0)
						ticketCount = await CreateTicketAsync(eventId, startRange, endRange, sectorId, price, creator, hostIP);
				}
				else
					foreach (var s in _event.Location.Sectors)
					{
						ticketCount += await CreateTicketAsync(eventId, s, creator, hostIP);
					}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return ticketCount;
		}

		private async Task<int> CreateTicketAsync(long eventId, Sector s, string creator, string hostIP)
		{
			var sqlParamValue = new HashSet<object[]>();
			for (int i = s.SeatingRangeStart; i <= s.SeatingRangeEnd; i++)
			{
				sqlParamValue.Add(new object[] { eventId, s.Id, s.SeatingPrice, i, creator, hostIP });

			}
			return await _ticketRepo.AddManyAsync(sqlParamValue, _ticketRepo.CreateInsertParams);
		}

		private async Task<int> CreateTicketAsync(long eventId, int? startRange, int? endRange, long? sectorId, decimal? price, string creator, string hostIP)
		{
			//todo: add tickets for selected sector and selected range
			throw new NotImplementedException();
		}

		public async Task DeleteAsync(long id)
		{
			try
			{
				var sqlParamValue = new object[1] { id };

				await _eventRepo.DeleteAsync(sqlParamValue, _eventRepo.CreateDeleteParams);
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
				await _eventRepo.UpdateAsync(SqlParams, _eventRepo.CreateUpdateParams);
			}

			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
