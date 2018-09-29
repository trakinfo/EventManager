using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Core.DataBaseContext
{
	public delegate T GetData<T>(IDataReader R);
	public delegate void DataParameters(IDbCommand cmd);

	public interface IDataBaseContext
    {
		string ConnectionString { get; }
		IDbConnection GetConnection();
		IDataParameter CreateParameter(string name, DbType type, IDbCommand cmd);
		Task<ISet<T>> FetchRecordSetAsync<T>(string sqlString, GetData<T> getDataRow);
		Task<T> FetchRecordAsync<T>(string sqlString, GetData<T> getDataRow);
		Task<int> AddManyRecordsAsync(string sqlString,ISet<object[]> sqlParameterValue, DataParameters addParams);
		Task AddRecordAsync(string sqlString, object[] sqlParameterValue, DataParameters addParams);
		Task UpdateRecordAsync(string sqlString, object[] sqlParameterValue, DataParameters updateParams);
		Task RemoveRecordAsync(string sqlString, object[] sqlParameterValue, DataParameters delParams);
	}
}
