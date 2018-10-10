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
		public TicketRepository(IDataBaseContext context, ITicketSql ticketSql) : base(context, ticketSql)
		{
			RefreshRepo();
			RecordAffected -= (s, ex) => RefreshRepo();
			RecordAffected += (s, ex) => RefreshRepo();
		}

		private void RefreshRepo()
		{
			objectList = GetListAsync(null, CreateTicket).Result;
		}

		public async Task<Ticket> GetTicket(long id)
		{
			return await Task.FromResult(objectList.Where(T => T.Id == id).FirstOrDefault());
		}

		public async Task<IEnumerable<Ticket>> GetTicketList()
		{
			return await Task.FromResult(objectList);
		}

		private Ticket CreateTicket(IDataReader R)
		{
			return new Ticket(
				Convert.ToInt64(R["ID"]), 
				Convert.ToInt32(R["SeatingNumber"]), 
				Convert.ToDecimal(R["Price"]), 
				Convert.ToInt64(R["IdEvent"]), 
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
