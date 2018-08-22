﻿using EventManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Core.Repository
{
    public interface IEventRepository
    {
		Task<Event> GetEventAsync(ulong eventId);
		Task<Event> GetEventAsync(string name);
		Task<IEnumerable<Event>> GetEventListAsync(string name="");
		Task AddEventAsync(object[] sqlParamValue);
		//Task UpdateEventAsync(IDictionary<string, object> sqlParams);
		//Task DeleteEventAsync(IDictionary<string, object> sqlParams);
		//Task AddTickets(Dictionary<string, object> sqlParams, uint seatingCount);
	}
}
