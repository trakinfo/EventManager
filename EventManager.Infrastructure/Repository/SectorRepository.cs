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
	public class SectorRepository : Repository, ISectorRepository
	{
		public IEnumerable<Sector> SectorList { get; protected set; }
		public SectorRepository(IDataBaseContext context, ISectorSql sectorSql) : base(context, sectorSql)
		{
			RefreshData();
		}

		private void RefreshData()
		{
			SectorList = GetListAsync(null, CreateSector).Result;
		}

		public IEnumerable<Sector> GetLocationSectors(long idLocation)
		{
			return SectorList.Where(L => L.LocationId == idLocation);
		}
		//public async Task<ISet<Sector>> GetListAsync(long idLocation, GetData<Sector> get)
		//{
		//	return await dbContext.FetchRecordSetAsync(((ISectorSql)sql).SelectLocationSectors(idLocation), GetSector);
		//}
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
					Convert.ToInt64(S["IdLocation"]),
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
