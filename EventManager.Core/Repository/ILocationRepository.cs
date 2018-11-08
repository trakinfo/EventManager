using EventManager.Core.DataBaseContext.SQL;
using EventManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Core.Repository
{
    public interface ILocationRepository : IRepository
    {
		//Task<Location> GetLocation(long idLocation);
		//Task<IEnumerable<Location>> GetLocationList(string name);
		Location CreateLocation(IDataReader Reader);
		void CreateInsertParams(IDbCommand cmd);
	}
}
