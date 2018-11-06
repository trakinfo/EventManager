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
		Task<Event> Get(long id);
		Task<IEnumerable<Event>> GetList(DateTime startDate, DateTime endDate, string name);
		//Task<int> AddTickets(object[] sqlParamValue, int seatingCount);
		Event CreateEvent(IDataReader R);
		void CreateInsertParams(IDbCommand cmd);
		void CreateUpdateParams(IDbCommand cmd);
		void CreateDeleteParams(IDbCommand cmd);
	}
}
