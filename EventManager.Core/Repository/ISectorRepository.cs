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
		Task<ISet<Sector>> GetListAsync(ulong idLocation, GetData<Sector> Get);
		Sector GetSector(IDataReader R);
	}
}
