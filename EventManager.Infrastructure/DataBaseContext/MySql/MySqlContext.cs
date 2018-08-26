using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using EventManager.Core.DataBaseContext;
using MySql.Data.MySqlClient;

namespace EventManager.Infrastructure.DataBaseContext
{
	public class MySqlContext : IDataBaseContext
	{
		public string ConnectionString { get; }

		public IDbConnection GetConnection()
		{
			IDbConnection dbConn = new MySqlConnection(ConnectionString);
			return dbConn;
		}
		public MySqlContext(string connectionString)
		{
			ConnectionString = connectionString;
		}
		public async Task<ISet<T>> FetchRecordSetAsync<T>(string sqlString, GetData<T> GetDataRow)
		{
			var HS = new HashSet<T>();
			try
			{
				using (var conn = (MySqlConnection)GetConnection())
				{
					conn.Open();
					var t = conn.BeginTransaction();
					try
					{
						using (var R = await new MySqlCommand { CommandText = sqlString, Connection = conn, Transaction = t }.ExecuteReaderAsync())
						{
							//if (!R.HasRows) return HS;
							while (R.Read())
							{
								var D = GetDataRow(R);
								HS.Add(D);
							}
						}
						t.Commit();
					}
					catch (MySqlException ex)
					{
						Console.WriteLine(ex.Message);
						t.Rollback();
					}
				}
				return await Task.FromResult(HS);
			}
			catch (Exception exe)
			{
				Console.WriteLine(exe.Message);
				return await Task.FromResult(HS);
			}
		}

		public async Task<T> FetchRecordAsync<T>(string sqlString, GetData<T> GetDataRow)
		{
			T DR = default(T);
			try
			{
				using (var conn = (MySqlConnection)GetConnection())
				{
					conn.Open();
					var t = conn.BeginTransaction();
					try
					{
						using (var R = await new MySqlCommand { CommandText = sqlString, Connection = conn, Transaction = t }.ExecuteReaderAsync())
						{
							if (R.Read()) DR = GetDataRow(R);
						}
						t.Commit();
					}
					catch (MySqlException ex)
					{
						Console.WriteLine(ex.Message);
						t.Rollback();
					}
				}
				return await Task.FromResult(DR);
			}
			catch (Exception exe)
			{
				Console.WriteLine(exe.Message);
				return await Task.FromResult(DR);
			}
		}
		public async Task<int> AddManyRecordsAsync(string sqlString, ISet<object[]> sqlParamValue, AddDataParameters createParams)
		{
			using (var conn = GetConnection())
			{
				conn.Open();
				var T = conn.BeginTransaction();
				var cmd = conn.CreateCommand();
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = sqlString;
				cmd.Transaction = T;
				using (cmd)
				{
					try
					{
						createParams(cmd);
						int recordAffected = 0;
						foreach (var p in sqlParamValue)
						{
							for (int i = 0; i < p.Length; i++)
								((MySqlCommand)cmd).Parameters[i].Value = p[i];
							recordAffected += await ((MySqlCommand)cmd).ExecuteNonQueryAsync();
						}
						T.Commit();
						return recordAffected;
					}
					catch (MySqlException ex)
					{
						Console.WriteLine(ex.Message);
						T.Rollback();
						return 0;
					}
				}
			}
		}

		public async Task AddRecordAsync(string sqlString, object[] sqlParamValue, AddDataParameters createParams)
		{
			using (var conn = GetConnection())
			{
				conn.Open();
				var T = conn.BeginTransaction();
				var cmd = conn.CreateCommand();
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = sqlString;
				cmd.Transaction = T;
				using (cmd)
				{
					try
					{
						createParams(cmd);
						for (int i = 0; i < sqlParamValue.Length; i++)
							((MySqlCommand)cmd).Parameters[i].Value = sqlParamValue[i];
						await ((MySqlCommand)cmd).ExecuteNonQueryAsync();
						T.Commit();
					}
					catch (MySqlException ex)
					{
						Console.WriteLine(ex.Message);
						T.Rollback();
					}
				}
			}
		}

		public async Task UpdateRecordAsync(string sqlString, object[] sqlParameterValue, AddDataParameters AddParams)
		{
			await AddRecordAsync(sqlString, sqlParameterValue, AddParams);
		}

		public async Task RemoveRecordAsync(string sqlString, object[] sqlParameterValue, AddDataParameters AddParams)
		{
			await AddRecordAsync(sqlString, sqlParameterValue, AddParams);
		}

		public IDataParameter CreateParameter(string name, DbType type, IDbCommand cmd)
		{
			var p = cmd.CreateParameter();
			p.ParameterName = name;
			p.DbType = type;

			return p;
		}
	}
}
