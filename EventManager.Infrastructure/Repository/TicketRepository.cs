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
		public TicketRepository(IDataBaseContext context, ITicketSql ticketSql) : base(context, ticketSql) { }

		public async Task<IEnumerable<Ticket>> GetListAsync(long idEvent, GetData<Ticket> Get)
		{
			return await dbContext.FetchRecordSetAsync((sql as ITicketSql).SelectMany(idEvent), Get);
		}

		public async Task<int> PurchaseAsync(object[] sqlParamValue, DataParameters createUpdateParams)
		{
			return await dbContext.UpdateRecordAsync((sql as ITicketSql).Puchase(), sqlParamValue, createUpdateParams);
		}

		public Ticket CreateTicket(IDataReader R)
		{
			return new Ticket(
				Convert.ToInt64(R["ID"]),
				Convert.ToInt32(R["SeatingNumber"]),
				Convert.ToDecimal(R["Price"]),
				R["UserId"].ToString(),
				Convert.ToInt64(R["IdSector"]),
				new Signature(
					R["User"].ToString(),
					R["HostIP"].ToString(),
					Convert.ToDateTime(R["Version"])
					)
				);
		}

		public async Task<int> CreateTicketAsync(long eventId, Sector sector, string creator, string hostIP)
		{
			var sqlParamValue = new HashSet<object[]>();
			for (int i = sector.SeatingRangeStart; i <= sector.SeatingRangeEnd; i++)
			{
				if (sector.Tickets.Where(t => t.SeatingNumber == i).Count() > 0) continue;
				sqlParamValue.Add(new object[] { eventId, sector.Id, sector.SeatingPrice, i, creator, hostIP });
			}
			if (sqlParamValue.Count == 0) return 0;
			return await AddManyAsync(sqlParamValue, CreateInsertParams);
		}

		public async Task<int> CreateTicketAsync(long eventId, int? startRange, int? endRange, Sector sector, decimal? price, string creator, string hostIP)
		{
			var sqlParamValue = new HashSet<object[]>();
			var start = startRange ?? sector.SeatingRangeStart;
			var end = endRange ?? sector.SeatingRangeEnd;
			for (int i = start; i <= end; i++)
			{
				if (sector.Tickets.Where(t => t.SeatingNumber == i).Count() > 0) continue;
				sqlParamValue.Add(new object[] { eventId, sector.Id, price ?? sector.SeatingPrice, i, creator, hostIP });
			}
			if (sqlParamValue.Count == 0) return 0;
			return await AddManyAsync(sqlParamValue, CreateInsertParams);
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

		public void CreatePurchaseParams(IDbCommand cmd)
		{
			cmd.Parameters.Add(dbContext.CreateParameter("@ID", DbType.Int64, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@UserName", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@PurchaseDate", DbType.DateTime, cmd));
		}
	}
}
