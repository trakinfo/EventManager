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
		readonly string connectionString = "server=localhost;userid=belfer;password=belfer;database=belfer2;Charset=utf8;Keepalive=15;ssl mode=0;";

		//public MySqlContext()
		//{
		//	ConnectionString = connectionString;

		//}

		private MySqlConnection GetConnection()
		{
			return new MySqlConnection(connectionString);
		}

		public async Task<ISet<Dictionary<string, object>>> FetchDataAsync(string sqlString)
		{
			var HS = new HashSet<Dictionary<string, object>>();
			try
			{
				using (MySqlConnection conn = GetConnection())
				{
					conn.Open();
					using (var R = await new MySqlCommand { CommandText = sqlString, Connection = conn }.ExecuteReaderAsync())
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
				}
				return await Task.FromResult(HS);
			}
			catch (MySqlException exe)
			{
				Console.WriteLine(exe.Message);
				return await Task.FromResult(HS);
			}
		}
		public Task<int> AddDataAsync(Dictionary<string, object> sqlParameters, string sqlString)
		{
			throw new NotImplementedException();
		}


		public Task<string> FetchSingleValueAsync(string sqlString)
		{
			throw new NotImplementedException();
		}

		public Task<ISet<T>> FetchValueSetAsync<T>(string sqlString)
		{
			throw new NotImplementedException();
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
