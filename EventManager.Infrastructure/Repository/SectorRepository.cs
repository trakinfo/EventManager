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
	public class SectorRepository : Repository<Sector>, ISectorRepository
	{
		public SectorRepository(IDataBaseContext context, ISectorSql sectorSql) : base(context, sectorSql) { }

		public async Task<IEnumerable<Sector>> GetListAsync(long idLocation, GetData<Sector> Get)
		{
			return await dbContext.FetchRecordSetAsync((sql as ISectorSql).SelectMany(idLocation), Get);
		}

		public Sector CreateSector(IDataReader S)
		{
			return new Sector
				(
					Convert.ToInt64(S["ID"]),
					S["Name"].ToString(),
					S["Description"].ToString(),
					Convert.ToInt32(S["SeatingRangeStart"]),
					Convert.ToInt32(S["SeatingRangeEnd"]),
					Convert.ToUInt32(S["SeatingPrice"]),
					new Signature
					(
						S["User"].ToString(),
						S["HostIP"].ToString(),
						Convert.ToDateTime(S["Version"])
					)
				);
		}

		public void CreateInsertParams(IDbCommand cmd)
		{
			throw new NotImplementedException();
		}

		public void CreateUpdateParams(IDbCommand cmd)
		{
			throw new NotImplementedException();
		}

		public void CreateDeleteParams(IDbCommand cmd)
		{
			throw new NotImplementedException();
		}
	}
}
