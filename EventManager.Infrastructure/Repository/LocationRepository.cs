using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventManager.Core.DataBaseContext;
using EventManager.Core.DataBaseContext.SQL;
using EventManager.Core.Domain;
using EventManager.Core.Globals;
using EventManager.Core.Repository;
using EventManager.Infrastructure.DataBaseContext.MySql.SQL;

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
		public Task AddAsync(Location location)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(ulong locationId)
		{
			throw new NotImplementedException();
		}

		public async Task<Location> GetAsync(ulong locationId)
		{
			var locationDR = await dbContext.FetchDataRowAsync(sql.SelectLocation(locationId));

			var location = new Location
				(
					Convert.ToUInt64(locationDR["ID"]),
					locationDR["Name"].ToString(),
					await GetLocationAddressAsync(locationId),
					await GetSectorListAsync(locationId),
					locationDR["PhoneNumber"].ToString(),
					locationDR["Email"].ToString(),
					locationDR["www"].ToString(),
					new Signature(locationDR["User"].ToString(), locationDR["HostIP"].ToString(), Convert.ToDateTime(locationDR["Version"]))
				);
			return await Task.FromResult(location);
		}

		public Task<IEnumerable<Location>> GetListAsync(string name = "")
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(Location location)
		{
			throw new NotImplementedException();
		}

		async Task<Address> GetLocationAddressAsync(ulong idLocation)
		{
			var addressDR = await dbContext.FetchDataRowAsync(sql.SelectAddress(idLocation));
			var address = new Address()
			{
				PlaceName = addressDR["PlaceName"].ToString(),
				StreetName = addressDR["StreetName"].ToString(),
				PropertyNumber = addressDR["PropertyNumber"].ToString(),
				ApartmentNumber = addressDR["ApartmentNumber"].ToString(),
				PostalCode = addressDR["PostalCode"].ToString(),
				PostOffice = addressDR["PostOffice"].ToString()

			};
			return await Task.FromResult(address);
		}

		async Task<ISet<Sector>> GetSectorListAsync(ulong idLocation)
		{
			var sectorSet = dbContext.FetchDataRowSetAsync(sql.SelectSector(idLocation));
			var sectors = new HashSet<Sector>();

			foreach (var S in await sectorSet)
			{
				sectors.Add(new Sector(Convert.ToUInt64(S["ID"]), S["Name"].ToString(), S["Description"].ToString(), Convert.ToUInt32(S["SeatingCount"])));
			}
			return await Task.FromResult(sectors);
		}
	}
}
