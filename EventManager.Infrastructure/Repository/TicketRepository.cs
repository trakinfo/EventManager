using EventManager.Core.DataBaseContext;
using EventManager.Core.DataBaseContext.SQL;
using EventManager.Core.Domain;
using EventManager.Core.Globals;
using EventManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Infrastructure.Repository
{
	public class TicketRepository : Repository<Ticket>, ITicketRepository
	{
		//public DateSpan TicketDateSpan { get; set; } = new DateSpan(DateTime.MinValue, DateTime.MaxValue);

		public TicketRepository(IDataBaseContext context, ITicketSql ticketSql) : base(context, ticketSql)
		{
			//RefreshRepo();
			
		}

		//private void RefreshRepo()
		//{
		//	contentList = GetListAsync(null, CreateTicket).Result;
		//}

		//public async Task<Ticket> GetTicket(long id)
		//{
		//	return await Task.FromResult(contentList.Where(T => T.Id == id).FirstOrDefault());
		//}

		//public async Task<IEnumerable<Ticket>> GetTicketList()
		//{
		//	return await Task.FromResult(contentList);
		//}

		//public async Task<IEnumerable<Ticket>> GetTicketList(long idEvent,long idSector)
		//{
		//	return await Task.FromResult(contentList.Where(T => T.EventId==idEvent && T.SectorId==idSector));
		//}

		public async Task<IEnumerable<Ticket>> GetListAsync(long idEvent, GetData<Ticket> Get)
		{
			return await dbContext.FetchRecordSetAsync((sql as ITicketSql).SelectMany(idEvent), Get);
		}

		public Ticket CreateTicket(IDataReader R)
		{
			//var id = Convert.ToInt64(R["ID"]);
			return new Ticket(
				Convert.ToInt64(R["ID"]),
				Convert.ToInt32(R["SeatingNumber"]),
				Convert.ToDecimal(R["Price"]),
				R["UserId"].ToString(),
				//Convert.ToInt64(R["IdEvent"]),
				Convert.ToInt64(R["IdSector"]),
				new Signature(
					R["User"].ToString(),
					R["HostIP"].ToString(),
					Convert.ToDateTime(R["Version"])
					)
				);
		}

		public void CreateInsertParams(IDbCommand cmd)
		{
			cmd.Parameters.Add(dbContext.CreateParameter("@IdEvent", DbType.Int64, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@IdSector", DbType.Int64, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@Price", DbType.Decimal, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@SeatingNumber", DbType.Int32, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@User", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@HostIP", DbType.String, cmd));
		}


	}
}
