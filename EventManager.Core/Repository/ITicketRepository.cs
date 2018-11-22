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
		Task<int> PurchaseAsync(object[] sqlParamValue, DataParameters createUpdateParams);
		Task<int> CreateTicketAsync(long eventId, Sector sector, string creator, string hostIP);
		Task<int> CreateTicketAsync(long eventId, int? startRange, int? endRange, Sector sector, decimal? price, string creator, string hostIP);
		//void CreateSelectParams(IDbCommand cmd);
		void CreateInsertParams(IDbCommand cmd);
		void CreatePurchaseParams(IDbCommand cmd);
		Ticket CreateTicket(IDataReader R);
	}
}
