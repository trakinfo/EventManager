using EventManager.Core.DataBaseContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Core.Repository
{
	public interface IRepository
    {
		Task<T> GetAsync<T>(long id, GetData<T> GetRow);
		Task<IEnumerable<T>> GetListAsync<T>(string name, GetData<T> GetRow);
		Task<IEnumerable<T>> GetListAsync<T>(string name, object[] sqlParamValue, DataParameters createParams, GetData<T> Get);
		Task AddAsync(object[] sqlParamValues, DataParameters addParams);
		Task<int> AddManyAsync(ISet<object[]> sqlParamValue, DataParameters createParams);
		Task<int> DeleteAsync(object[] sqlParamValues, DataParameters delParams);
		Task<int> UpdateAsync(object[] sqlParamValues, DataParameters updateParams);
	}
}
