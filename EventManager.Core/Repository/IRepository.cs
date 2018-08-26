using EventManager.Core.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Core.Repository
{
    public interface IRepository
    {
		Task<T> GetAsync<T>(ulong id, GetData<T> GetRow);
		Task<IEnumerable<T>> GetListAsync<T>(string sqlString, GetData<T> GetRow);
		Task AddAsync<T>(object[] sqlParamValues, AddDataParameters AddParams);
		Task DeleteAsync<T>(object[] sqlParamValues, AddDataParameters AddParams);
		Task UpdateAsync<T>(object[] sqlParamValues, AddDataParameters AddParams);
	}
}
