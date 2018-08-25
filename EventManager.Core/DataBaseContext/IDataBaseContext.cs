using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Core.DataBaseContext
{
	public delegate T GetData<T>(IDataReader R);
	public delegate void AddDataParameters(IDbCommand cmd);
	//public delegate void AddDataSet(object[] data, IDbCommand cmd);

	public interface IDataBaseContext
    {
		string ConnectionString { get; }
		IDbConnection GetConnection();
		IDataParameter CreateParameter(string name, DbType type, IDbCommand cmd);
		Task<ISet<T>> FetchRecordSetAsync<T>(string sqlString, GetData<T> GetDataRow);
		Task<T> FetchRecordAsync<T>(string sqlString, GetData<T> GetDataRow);
		Task<int> AddManyRecordsAsync(string sqlString,ISet<object[]> sqlParameterValue, AddDataParameters AddParams);
		Task AddRecordAsync(string sqlString, object[] sqlParameterValue, AddDataParameters AddParams);
		//Task<ISet<T>> FetchValueSetAsync<T>(string sqlString);
		//Task<String> FetchSingleValueAsync(string sqlString);
		Task UpdateRecordAsync(string sqlString, object[] sqlParameterValue, AddDataParameters AddParams);
		Task RemoveRecordAsync(string sqlString, object[] sqlParameterValue, AddDataParameters AddParams);
	}
}
