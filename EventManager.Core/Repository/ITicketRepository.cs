using EventManager.Core.Domain;
using EventManager.Core.Globals;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace EventManager.Core.Repository
{
	public interface ITicketRepository
	{
		DateSpan TicketDateSpan { set; }
		Task<Ticket> GetTicket(long id);
		Task<IList<Ticket>> GetTicketList();
		void CreateInsertParams(IDbCommand cmd);
	}
}
