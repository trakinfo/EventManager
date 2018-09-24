﻿using System;
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
							while (R.Read()) HS.Add(GetDataRow(R));
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
				var cmd = CreateCommand(conn, T, CommandType.Text, sqlString);
				createParams(cmd);
				try
				{
					int recordAffected = 0;
					foreach (var p in sqlParamValue)
					{
						recordAffected += await ExecuteCommandAsync(cmd, p);
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
				//using (cmd)
				//{
				//	try
				//	{
				//		int recordAffected = 0;
				//		foreach (var p in sqlParamValue)
				//		{
				//			for (int i = 0; i < p.Length; i++)
				//				((MySqlCommand)cmd).Parameters[i].Value = p[i];
				//			recordAffected += await ((MySqlCommand)cmd).ExecuteNonQueryAsync();
				//		}
				//		T.Commit();
				//		return recordAffected;
				//	}
				//	catch (MySqlException ex)
				//	{
				//		Console.WriteLine(ex.Message);
				//		T.Rollback();
				//		return 0;
				//	}
				//}
			}
		}

		public async Task AddRecordAsync(string sqlString, object[] sqlParamValue, AddDataParameters createParams)
		{
			await ExecuteCommandAsync(sqlString, sqlParamValue, createParams);
		}

		public async Task UpdateRecordAsync(string sqlString, object[] sqlParameterValue, AddDataParameters updateParams)
		{
			await ExecuteCommandAsync(sqlString, sqlParameterValue, updateParams);
		}

		public async Task RemoveRecordAsync(string sqlString, object[] sqlParameterValue, AddDataParameters delParams)
		{
			await ExecuteCommandAsync(sqlString, sqlParameterValue, delParams);
		}

		async Task ExecuteCommandAsync(string sqlString, object[] sqlParamValue, AddDataParameters createParams)
		{
			using (var conn = GetConnection())
			{
				conn.Open();
				var T = conn.BeginTransaction();
				var cmd = CreateCommand(conn, T, CommandType.Text, sqlString);
				createParams(cmd);
				try
				{
					await ExecuteCommandAsync(cmd, sqlParamValue);
					T.Commit();
				}
				catch (MySqlException ex)
				{
					Console.WriteLine(ex.Message);
					T.Rollback();
				}
				//using (cmd)
				//{
				//	try
				//	{
				//		for (int i = 0; i < sqlParamValue.Length; i++)
				//			((MySqlCommand)cmd).Parameters[i].Value = sqlParamValue[i];
				//		await ((MySqlCommand)cmd).ExecuteNonQueryAsync();
				//		T.Commit();
				//	}
				//	catch (MySqlException ex)
				//	{
				//		Console.WriteLine(ex.Message);
				//		T.Rollback();
				//	}
				//}
			}
		}

		async Task<int> ExecuteCommandAsync(IDbCommand cmd, object[] sqlParamValue)
		{
			using (cmd)
			{
				for (int i = 0; i < sqlParamValue.Length; i++)
					((MySqlCommand)cmd).Parameters[i].Value = sqlParamValue[i];
				return await ((MySqlCommand)cmd).ExecuteNonQueryAsync();
			}
		}
		public IDataParameter CreateParameter(string name, DbType type, IDbCommand cmd)
		{
			var p = cmd.CreateParameter();
			p.ParameterName = name;
			p.DbType = type;

			return p;
		}

		IDbCommand CreateCommand(IDbConnection conn, IDbTransaction T, CommandType cmdType, string cmdText)
		{
			var cmd = conn.CreateCommand();
			cmd.CommandType = cmdType;
			cmd.CommandText = cmdText;
			cmd.Transaction = T;
			return cmd;
		}
	}
}
