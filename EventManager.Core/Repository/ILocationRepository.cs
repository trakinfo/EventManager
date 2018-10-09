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
		Location GetLocation(long idLocation);
		IEnumerable<Location> GetLocationList(string name);
		Location CreateLocation(IDataReader Reader);
		void CreateInsertParams(IDbCommand cmd);
	}
}
