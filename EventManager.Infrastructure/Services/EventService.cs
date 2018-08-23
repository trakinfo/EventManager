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

		public async Task CreateAsync(string name, string description, ulong? idLocation, DateTime startDate, DateTime endDate, string creator, string hostIP)
		{
			try
			{
				var sqlParamValue = new object[] { name, description, idLocation, startDate, endDate, creator, hostIP };

				//SqlParams.Add("?Name", name);
				//SqlParams.Add("?Description", description);
				//SqlParams.Add("?IdLocation", idLocation);
				//SqlParams.Add("?StartDate", startDate);
				//SqlParams.Add("?EndDate", endDate);
				//SqlParams.Add("?User", creator);
				//SqlParams.Add("?HostIP", hostIP);
				var HS = new HashSet<object[]>();
				HS.Add(sqlParamValue);
				await _eventRepository.AddEventAsync(HS);
			}

			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				//return await Task.FromResult(-1);
			}
		}


		public async Task<int> CreateTicketCollectionAsync(ulong eventId)
		{

			var _event = await _eventRepository.GetEventAsync(eventId);
			if (_event.Location == null) return 0;
			if (_event.Location.Sectors == null) return 0;

			var HS = new HashSet<Ticket>();
			int ticketCount=0;
			foreach (var S in _event.Location.Sectors)
			{
				//var sqlParams = new Dictionary<string, object>();
				//sqlParams.Add("?IdEvent", eventId);
				//sqlParams.Add("?IdSector", S.Id);
				//sqlParams.Add("?Price", S.SeatingPrice);
				var sqlParamValue = new object[] { eventId, S.Id, S.SeatingPrice };
				ticketCount = await _eventRepository.AddTickets(sqlParamValue, S.SeatingCount);
			}
			return ticketCount;

		}

		//public async Task DeleteAsync(ulong id)
		//{
		//	try
		//	{
		//		var sqlParams = new Dictionary<string, object>();

		//		sqlParams.Add("?ID", id);

		//		await _eventRepository.DeleteEventAsync(sqlParams);
		//	}

		//	catch (Exception e)
		//	{
		//		Console.WriteLine(e.Message);
		//	}
		//}

		public Task DeleteTicketsAsync(ISet<Ticket> tickets)
		{
			throw new NotImplementedException();
		}



		//public async Task UpdateAsync(ulong id, string name, string description, ulong? idLocation, DateTime startDate, DateTime endDate, string modifier, string hostIP)
		//{
		//	try
		//	{
		//		var SqlParams = new Dictionary<string, object>();

		//		SqlParams.Add("?ID", id);
		//		SqlParams.Add("?Name", name);
		//		SqlParams.Add("?Description", description);
		//		SqlParams.Add("?IdLocation", idLocation);
		//		SqlParams.Add("?StartDate", startDate);
		//		SqlParams.Add("?EndDate", endDate);
		//		SqlParams.Add("?User", modifier);
		//		SqlParams.Add("?HostIP", hostIP);

		//		await _eventRepository.UpdateEventAsync(SqlParams);
		//	}

		//	catch (Exception e)
		//	{
		//		Console.WriteLine(e.Message);
		//	}
		//}
	}
}
