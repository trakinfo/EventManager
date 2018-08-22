using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Core.DataBaseContext
{
	public delegate T GetData<T>(IDataReader R);
	public delegate void AddData(IDbCommand cmd);
	public delegate void AddDataSet(object[] data, IDbCommand cmd);

	public interface IDataBaseContext
    {
		string ConnectionString { get; }
		IDbConnection GetConnection();
		Task<ISet<T>> FetchDataRowSetAsync<T>(string sqlString, GetData<T> GetDataRow);
		Task<T> FetchDataRowAsync<T>(string sqlString, GetData<T> GetDataRow);
		//Task<ISet<T>> FetchValueSetAsync<T>(string sqlString);
		//Task<String> FetchSingleValueAsync(string sqlString);
		Task AddDataAsync(string sqlString,object[] sqlParameters, AddData AddData);
		//Task<int> AddDataSetAsync(string sqlString, AddDataSet AddData);
		//Task AddRecordAsync(string sqlString, AddData AddRecord);
		//Task<int> ExecuteCommandAsync(IDictionary<string, object> sqlParameters, string sqlString);
		//Task<int> RemoveDataAsync(IDictionary<string, object> sqlParameters, string sqlString);
	}
}
