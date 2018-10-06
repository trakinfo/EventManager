using EventManager.Core.DataBaseContext;
using EventManager.Core.DataBaseContext.SQL;
using EventManager.Core.Domain;
using EventManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Infrastructure.Repository
{
	public class SectorRepository : Repository, ISectorRepository
	{
		public IEnumerable<Sector> SectorList { get; protected set; }
		public SectorRepository(IDataBaseContext context, ISectorSql sectorSql) : base(context, sectorSql)
		{
			RefreshData();
		}

		private void RefreshData()
		{
			SectorList = GetListAsync(null, GetSector).Result;
		}

		public async Task<ISet<Sector>> GetListAsync(ulong idLocation, GetData<Sector> get)
		{
			return await dbContext.FetchRecordSetAsync(((ISectorSql)sql).SelectLocationSectors(idLocation), GetSector);
		}
		public Sector GetSector(IDataReader S)
		{
			return new Sector(Convert.ToUInt64(S["ID"]), S["Name"].ToString(), S["Description"].ToString(), Convert.ToUInt32(S["SeatingRangeStart"]), Convert.ToUInt32(S["SeatingRangeEnd"]), Convert.ToUInt32(S["SeatingPrice"]), null);
		}

		public void CreateInsertParams(IDbCommand cmd)
		{
			throw new NotImplementedException();
		}
	}
}
