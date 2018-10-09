using EventManager.Core.DataBaseContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Core.Repository
{
	public interface IRepository
    {
		Task<T> GetAsync<T>(long id, GetData<T> GetRow);
		Task<IEnumerable<T>> GetListAsync<T>(string name, GetData<T> GetRow);
		Task AddAsync(object[] sqlParamValues, DataParameters addParams);
		Task AddManyAsync(ISet<object[]> sqlParamValue, DataParameters createParams);
		Task DeleteAsync(object[] sqlParamValues, DataParameters delParams);
		Task UpdateAsync(object[] sqlParamValues, DataParameters updateParams);
	}
}
