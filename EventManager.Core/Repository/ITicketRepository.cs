using EventManager.Core.Domain;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace EventManager.Core.Repository
{
	public interface ITicketRepository
	{
		Task<Ticket> GetTicket(long id);
		Task<IEnumerable<Ticket>> GetTicketList();
		void CreateInsertParams(IDbCommand cmd);
	}
}
