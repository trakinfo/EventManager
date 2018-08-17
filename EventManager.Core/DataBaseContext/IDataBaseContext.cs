using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Core.DataBaseContext
{
    public interface IDataBaseContext
    {
		string ConnectionString { get; }
		IDbConnection GetConnection();
		//Task<ISet<IDictionary<string, object>>> FetchDataRowSetAsync(string sqlString);
		//Task<IDictionary<string, object>> FetchDataRowAsync(string sqlString);
		//Task<ISet<T>> FetchValueSetAsync<T>(string sqlString);
		//Task<String> FetchSingleValueAsync(string sqlString);
		//Task<long> AddDataAsync(IDictionary<string, object> sqlParameters, string sqlString);
		//Task<int> ExecuteCommandAsync(IDictionary<string, object> sqlParameters, string sqlString);
		//Task<int> RemoveDataAsync(IDictionary<string, object> sqlParameters, string sqlString);
	}
}
