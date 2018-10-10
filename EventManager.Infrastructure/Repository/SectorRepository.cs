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
		public IEnumerable<Sector> SectorList { get => objectList; }
		public SectorRepository(IDataBaseContext context, ISectorSql sectorSql) : base(context, sectorSql)
		{
			RefreshRepo();
			RecordAffected -= (s, ex) => RefreshRepo();
			RecordAffected += (s, ex) => RefreshRepo();
		}

		private void RefreshRepo()
		{
			objectList = GetListAsync(null, CreateSector).Result;
		}

		public Sector GetSector(long id)
		{
			return SectorList.Where(S => S.Id == id).FirstOrDefault();
		} 

		public IEnumerable<Sector> GetSectorList(string name)
		{
			return SectorList.Where(S => S.Name.StartsWith(name));
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
