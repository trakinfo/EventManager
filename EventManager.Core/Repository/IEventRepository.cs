using EventManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Core.Repository
{
    public interface IEventRepository : IRepository
    {
		//Task<Event> GetEventAsync(ulong eventId);
		//Task<Event> GetEventAsync(string name);
		//Task<IEnumerable<Event>> GetEventListAsync(string name="");
		//Task AddEventAsync(object[] sqlParamValue);
		//Task DeleteEventAsync(object[] sqlParamValue);
		//Task UpdateEventAsync(object[] sqlParamValue);
		Task<int> AddTickets(object[] sqlParamValue, uint seatingCount);
		Event GetEvent(IDataReader R);
		void CreateInsertParams(IDbCommand cmd);
		void CreateUpdateParams(IDbCommand cmd);
		void CreateDeleteParams(IDbCommand cmd);
	}
}
