using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Core.DataBaseContext
{
    public interface IDataBaseContext
    {
		Task<ISet<IDictionary<string, object>>> FetchDataSetAsync(string sqlString);
		Task<IDictionary<string, object>> FetchDataAsync(string sqlString);
		Task<ISet<T>> FetchValueSetAsync<T>(string sqlString);
		Task<String> FetchSingleValueAsync(string sqlString);
		Task<int> AddDataAsync(Dictionary<string, object> sqlParameters, string sqlString);
		Task<int> UpdateDataAsync(Dictionary<string, object> sqlParameters, string sqlString);
		Task<int> RemoveDataAsync(Dictionary<string, object> sqlParameters, string sqlString);
	}
}
