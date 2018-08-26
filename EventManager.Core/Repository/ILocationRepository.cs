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
		//ILocationSql sql { get; }
		Task<Location> GetLocationModelAsync(IDataReader Reader);
		//Task<Location> GetAsync(ulong LocationId);
		//Task<IEnumerable<Location>> GetListAsync(string name = "");
		//Task AddAsync(object[] sqlParamValue);
		//Task UpdateAsync(object[] sqlParamValue);
		//Task DeleteAsync(object[] sqlParamValue);
	}
}
