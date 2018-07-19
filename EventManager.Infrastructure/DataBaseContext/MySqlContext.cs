using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using EventManager.Core.DataBaseContext;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace EventManager.Infrastructure.DataBaseContext
{
	public class MySqlContext : IDataBaseContext
	{
		readonly string connectionString = "server=localhost;userid=manager;password=manager;database=event_manager;Charset=utf8;Keepalive=15;ssl mode=0;";

		private MySqlConnection GetConnection()
		{
			return new MySqlConnection(connectionString);
		}

		public async Task<ISet<Dictionary<string, object>>> FetchDataSetAsync(string sqlString)
		{
			var HS = new HashSet<Dictionary<string, object>>();
			try
			{
				using (MySqlConnection conn = GetConnection())
				{
					conn.Open();
					var T = conn.BeginTransaction();
					try
					{
						using (var R = await new MySqlCommand { CommandText = sqlString, Connection = conn, Transaction = T }.ExecuteReaderAsync())
						{
							if (!R.HasRows) return HS;
							while (R.Read())
							{
								var D = new Dictionary<string, object>();
								for (int i = 0; i < R.FieldCount; i++)
								{
									D.Add(R.GetName(i), R.GetValue(i));
								}
								HS.Add(D);
							}
						}
						T.Commit();
					}
					catch (MySqlException ex)
					{
						Console.WriteLine(ex.Message);
						T.Rollback();
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

		public async Task<Dictionary<string,object>> FetchDataAsync(string sqlString)
		{

		}
		public Task<int> AddDataAsync(Dictionary<string, object> sqlParameters, string sqlString)
		{
			throw new NotImplementedException();
		}


		public Task<string> FetchSingleValueAsync(string sqlString)
		{
			throw new NotImplementedException();
		}

		public async Task<ISet<T>> FetchValueSetAsync<T>(string sqlString)
		{
			var HS = new HashSet<T>();
			try
			{
				using (var R = await new MySqlCommand { CommandText = sqlString, Connection = get, }.ExecuteReaderAsync())
				{
					if (!R.HasRows) return HS;
					while (R.Read())
					{
						HS.Add((T)Convert.ChangeType(R[0], typeof(T)));
					}
					return await Task.FromResult(HS);
				}
			}
			catch (MySqlException ex)
			{
				Console.WriteLine(ex.Message);
				
				return await Task.FromResult(HS);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public Task<int> RemoveDataAsync(Dictionary<string, object> sqlParameters, string sqlString)
		{
			throw new NotImplementedException();
		}

		public Task<int> UpdateDataAsync(Dictionary<string, object> sqlParameters, string sqlString)
		{
			throw new NotImplementedException();
		}
	}
}
