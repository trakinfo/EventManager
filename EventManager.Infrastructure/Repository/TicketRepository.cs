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

		public async Task<int> AddTickets(object[] sqlParamValue, int seatingCount)
		{
			var HS = new HashSet<object[]>();

			for (int i = 0; i < seatingCount; i++)
			{
				sqlParamValue[3] = i + 1;
				HS.Add(sqlParamValue.ToArray());
			}
			return await AddManyAsync(sqlParamValue, CreateInsertParams);
			//return await dbContext.AddManyRecordsAsync(sql.Insert(), HS, CreateTicketParams);
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
