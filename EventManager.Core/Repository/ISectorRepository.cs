using EventManager.Core.DataBaseContext;
using EventManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Core.Repository
{
	public interface ISectorRepository : IRepository
	{
		void CreateInsertParams(IDbCommand cmd);
		void CreateUpdateParams(IDbCommand cmd);
		void CreateDeleteParams(IDbCommand cmd);
		Task<Sector> GetSector(long id);
		Task<IEnumerable<Sector>> GetSectorList(long idLocation);
		//Task<ISet<Sector>> GetListAsync(long idLocation, GetData<Sector> Get);
		Sector CreateSector(IDataReader R);
	}
}
