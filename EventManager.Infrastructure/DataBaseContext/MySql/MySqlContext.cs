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
		public async Task<ISet<T>> FetchDataRowSetAsync<T>(string sqlString, GetData<T> GetDataRow)
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
							if (!R.HasRows) return HS;
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

		//public async Task<IDictionary<string, object>> FetchDataRowAsync(string sqlString)
		//{
		//	var DR = new Dictionary<string, object>();
		//	try
		//	{
		//		using (MySqlConnection conn = (MySqlConnection)GetConnection())
		//		{
		//			conn.Open();
		//			var T = conn.BeginTransaction();
		//			try
		//			{
		//				using (var R = await new MySqlCommand { CommandText = sqlString, Connection = conn, Transaction = T }.ExecuteReaderAsync())
		//				{
		//					if (R.Read())
		//					{
		//						for (int i = 0; i < R.FieldCount; i++)
		//						{
		//							DR.Add(R.GetName(i), R.GetValue(i));
		//						}
		//					}
		//				}
		//				T.Commit();
		//			}
		//			catch (MySqlException ex)
		//			{
		//				Console.WriteLine(ex.Message);
		//				T.Rollback();
		//			}
		//		}
		//		return await Task.FromResult(DR);
		//	}
		//	catch (Exception exe)
		//	{
		//		Console.WriteLine(exe.Message);
		//		return await Task.FromResult(DR);
		//	}
		//}
		//public async Task<long> AddDataAsync(IDictionary<string, object> sqlParameters, string sqlString)
		//{
		//	using (MySqlConnection conn = (MySqlConnection)GetConnection())
		//	{
		//		conn.Open();
		//		var T = conn.BeginTransaction();
		//		using (var cmd = new MySqlCommand() { CommandText = sqlString, Connection = conn, Transaction = T })
		//		{
		//			try
		//			{
		//				cmd.Parameters.Clear();
		//				cmd.Transaction = T;
		//				foreach (var P in sqlParameters)
		//				{
		//					var myParam = new MySqlParameter(P.Key, P.Value);
		//					cmd.Parameters.Add(myParam);
		//				}
		//				await cmd.ExecuteNonQueryAsync();
		//				T.Commit();
		//				return await Task.FromResult(cmd.LastInsertedId);
		//			}
		//			catch (MySqlException ex)
		//			{
		//				Console.WriteLine(ex.Message);
		//				T.Rollback();
		//				return await Task.FromResult(-1);
		//			}
		//		}
		//	}
		//}


		//public Task<string> FetchSingleValueAsync(string sqlString)
		//{
		//	throw new NotImplementedException();
		//}

		//public async Task<ISet<T>> FetchValueSetAsync<T>(string sqlString)
		//{
		//	var HS = new HashSet<T>();
		//	try
		//	{
		//		using (var R = await new MySqlCommand { CommandText = sqlString, Connection = (MySqlConnection)GetConnection(), }.ExecuteReaderAsync())
		//		{
		//			if (!R.HasRows) return HS;
		//			while (R.Read())
		//			{
		//				HS.Add((T)Convert.ChangeType(R[0], typeof(T)));
		//			}
		//			return await Task.FromResult(HS);
		//		}
		//	}
		//	catch (MySqlException ex)
		//	{
		//		Console.WriteLine(ex.Message);

		//		return await Task.FromResult(HS);
		//	}
		//	catch (Exception)
		//	{
		//		throw;
		//	}
		//}

		////public Task<int> RemoveDataAsync(IDictionary<string, object> sqlParameters, string sqlString)
		////{
		////	throw new NotImplementedException();
		////}

		//public async Task<int> ExecuteCommandAsync(IDictionary<string, object> sqlParameters, string sqlString)
		//{
		//	using (MySqlConnection conn = (MySqlConnection)GetConnection())
		//	{
		//		conn.Open();
		//		var T = conn.BeginTransaction();
		//		using (var cmd = new MySqlCommand() { CommandText = sqlString, Connection = conn, Transaction = T })
		//		{
		//			try
		//			{
		//				cmd.Parameters.Clear();
		//				cmd.Transaction = T;
		//				foreach (var P in sqlParameters)
		//				{
		//					var myParam = new MySqlParameter(P.Key, P.Value);
		//					cmd.Parameters.Add(myParam);
		//				}
		//				var x = await cmd.ExecuteNonQueryAsync();
		//				T.Commit();
		//				return await Task.FromResult(x);
		//			}
		//			catch (MySqlException ex)
		//			{
		//				Console.WriteLine(ex.Message);
		//				T.Rollback();
		//				return await Task.FromResult(-1);
		//			}
		//		}
		//	}
		//}


	}
}
