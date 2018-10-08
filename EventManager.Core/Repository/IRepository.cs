using EventManager.Core.DataBaseContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManager.Core.Repository
{
	public interface IRepository
    {
		Task<T> GetAsync<T>(long id, GetData<T> GetRow);
		Task<IEnumerable<T>> GetListAsync<T>(string name, GetData<T> GetRow);
		Task AddAsync<T>(object[] sqlParamValues, DataParameters addParams);
		//Task AddManyAsyng<T>();
		Task DeleteAsync<T>(object[] sqlParamValues, DataParameters delParams);
		Task UpdateAsync<T>(object[] sqlParamValues, DataParameters updateParams);
	}
}
