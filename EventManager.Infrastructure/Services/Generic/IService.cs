using EventManager.Core.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Infrastructure.Services.Generic
{
	public interface IService
	{
		Task<T> GetAsync<T>(ulong id, GetData<T> GetRow);
		Task<IEnumerable<T>> GetListAsync<T>(string name, GetData<T> GetRow);
		Task AddAsync(object[] sqlParamValue, DataParameters createParams);
		Task DeleteAsync(object[] sqlParamValue, DataParameters deleteParams);
		Task UpdateAsync(object[] sqlParamValue, DataParameters updateParams);
	}
}