using EventManager.Core.Domain;
using EventManager.Core.Globals;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace EventManager.Core.Repository
{
	public interface ITicketRepository
	{
		//DateSpan TicketDateSpan { set; }
		Task<Ticket> GetTicket(long id);
		Task<IEnumerable<Ticket>> GetTicketList();
		Task<IEnumerable<Ticket>> GetTicketList(long idEvent, long idSector);
		void CreateInsertParams(IDbCommand cmd);
	}
}
