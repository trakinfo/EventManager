using EventManager.Core.DataBaseContext;
using EventManager.Core.Domain;
using EventManager.Core.Globals;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace EventManager.Core.Repository
{
	public interface ITicketRepository : IRepository
	{
		//DateSpan TicketDateSpan { set; }
		//Task<Ticket> GetTicket(long id);
		//Task<IEnumerable<Ticket>> GetTicketList();
		Task<IEnumerable<Ticket>> GetListAsync(long idEvent, GetData<Ticket> Get);
		//Task<int> AddTickets(ISet<object[]> sqlParamValue, int seatingCount);
		//void CreateSelectParams(IDbCommand cmd);
		void CreateInsertParams(IDbCommand cmd);
		Ticket CreateTicket(IDataReader R);
	}
}
