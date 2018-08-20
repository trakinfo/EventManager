using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using EventManager.Core.DataBaseContext;
using EventManager.Core.DataBaseContext.SQL;
using EventManager.Core.Domain;
using EventManager.Core.Globals;
using EventManager.Core.Repository;

namespace EventManager.Infrastructure.Repository
{
	public class LocationRepository : ILocationRepository
	{
		IDataBaseContext dbContext;
		ILocationSql sql;

		public LocationRepository(IDataBaseContext context, ILocationSql locationSql)
		{
			dbContext = context;
			sql = locationSql;
		}
		public async Task<long> AddAsync(IDictionary<string, object> sqlParams)
		{
			using (var conn = dbContext.GetConnection())
			{
				conn.Open();
				var T = conn.BeginTransaction();
				var cmd = conn.CreateCommand();
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = sql.InsertLocation();
				cmd.Transaction = T;
				using (cmd)
				{
					try
					{
						cmd.Parameters.Clear();
						foreach (var P in sqlParams)
						{
							var myParam = cmd.CreateParameter();
							myParam.ParameterName = P.Key;
							myParam.Value = P.Value;
							cmd.Parameters.Add(myParam);
						}
						var nr = cmd.ExecuteNonQuery();
						T.Commit();
						return await Task.FromResult(nr);
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
						T.Rollback();
						return await Task.FromResult(-1);
					}
				}
			}
		}

		public Task DeleteAsync(ulong locationId)
		{
			throw new NotImplementedException();
		}

		public async Task<Location> GetAsync(ulong locationId)
		{
			Location location = null;
			using (var conn = dbContext.GetConnection())
			{
				conn.Open();
				var T = conn.BeginTransaction();
				var cmd = conn.CreateCommand();
				cmd.CommandText = sql.SelectLocation(locationId);
				cmd.Transaction = T;
				try
				{
					using (var R = cmd.ExecuteReader())
					{
						while (R.Read())
						{
							var idLocation = Convert.ToUInt64(R["ID"]);
							Address address = null;

							if (!string.IsNullOrEmpty(R["IdAddress"].ToString()))
								address = await GetLocationAddressAsync(idLocation);

							location = new Location
								(
									idLocation,
									R["Name"].ToString(),
									address,
									await GetSectorListAsync(locationId),
									R["PhoneNumber"].ToString(),
									R["Email"].ToString(),
									R["www"].ToString(),
									new Signature(R["User"].ToString(), R["HostIP"].ToString(), Convert.ToDateTime(R["Version"]))
								);
						}
					}
					T.Commit();
				}
				catch (Exception ex)
				{
					T.Rollback();
					Console.WriteLine(ex.Message);
				}
				return await Task.FromResult(location);
			}
		}

		public async Task<IEnumerable<Location>> GetListAsync(string name = "")
		{
			var locations = new HashSet<Location>();
			using (var conn = dbContext.GetConnection())
			{
				conn.Open();
				var Tr = conn.BeginTransaction();
				var cmd = conn.CreateCommand();
				cmd.CommandText = sql.SelectLocations(name);
				cmd.Transaction = Tr;
				try
				{
					using (var R = cmd.ExecuteReader())
					{
						while (R.Read())
						{
							
							var idLocation = Convert.ToUInt64(R["ID"]);
							
							Address address = null;
							if (!string.IsNullOrEmpty(R["IdAddress"].ToString()))
								address = await GetLocationAddressAsync(idLocation);

							locations.Add(new Location
								(
									idLocation,
									R["Name"].ToString(),
									address,
									await GetSectorListAsync(idLocation),
									R["PhoneNumber"].ToString(),
									R["Email"].ToString(),
									R["www"].ToString(),
									new Signature(R["User"].ToString(), R["HostIP"].ToString(), Convert.ToDateTime(R["Version"]))
								)
								);
						}
					}
					Tr.Commit();
				}
				catch (Exception ex)
				{
					Tr.Rollback();
					Console.WriteLine(ex.Message);
				}

			}
			return await Task.FromResult(locations.AsEnumerable());
		}

		public Task UpdateAsync(IDictionary<string, object> sqlParams)
		{
			throw new NotImplementedException();
		}

		async Task<Address> GetLocationAddressAsync(ulong idLocation)
		{
			Address address = null;
			using (var conn = dbContext.GetConnection())
			{
				conn.Open();
				var T = conn.BeginTransaction();
				var cmd = conn.CreateCommand();
				cmd.CommandText = sql.SelectAddress(idLocation);
				cmd.Transaction = T;
				try
				{
					using (var R = cmd.ExecuteReader())
					{
						while (R.Read())
						{
							address = new Address()
							{
								PlaceName = R["PlaceName"].ToString(),
								StreetName = R["StreetName"].ToString(),
								PropertyNumber = R["PropertyNumber"].ToString(),
								ApartmentNumber = R["ApartmentNumber"].ToString(),
								PostalCode = R["PostalCode"].ToString(),
								PostOffice = R["PostOffice"].ToString()
							};
						}
					}
					T.Commit();
				}
				catch (Exception ex)
				{
					T.Rollback();
					Console.WriteLine(ex.Message);
				}
				return await Task.FromResult(address);
			}

		}

		async Task<ISet<Sector>> GetSectorListAsync(ulong idLocation)
		{
			var sectors = new HashSet<Sector>();

			using (var conn = dbContext.GetConnection())
			{
				conn.Open();
				var T = conn.BeginTransaction();
				var cmd = conn.CreateCommand();
				cmd.CommandText = sql.SelectSector(idLocation);
				cmd.Transaction = T;
				try
				{
					using (var S = cmd.ExecuteReader())
					{
						while (S.Read())
						{
							sectors.Add(new Sector(Convert.ToUInt64(S["ID"]), S["Name"].ToString(), S["Description"].ToString(), Convert.ToUInt32(S["SeatingCount"]), Convert.ToUInt32(S["SeatingPrice"])));
						}
					}
					T.Commit();
				}
				catch (Exception ex)
				{
					T.Rollback();
					Console.WriteLine(ex.Message);
				}
				return await Task.FromResult(sectors);
			}
		}
	}
}
