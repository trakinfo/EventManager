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
	public class LocationRepository : GenericRepository, ILocationRepository
	{
		public LocationRepository(IDataBaseContext context, ILocationSql locationSql) : base(context, locationSql) { }

		async Task<Address> GetLocationAddressAsync(ulong idLocation)
		{
			var address = await dbContext.FetchRecordAsync(((ILocationSql)sql).SelectAddress(idLocation), GetAddressModel);
			return await Task.FromResult(address);
		}
		
		async Task<ISet<Sector>> GetSectorListAsync(ulong idLocation)
		{
			var sectors = await dbContext.FetchRecordSetAsync(((ILocationSql)sql).SelectSector(idLocation), GetSectorModel);
			return await Task.FromResult(sectors);
		}

		public Location GetLocationModel(IDataReader R)
		{
			var idLocation = Convert.ToUInt64(R["ID"]);
			Address address = null;

			if (!string.IsNullOrEmpty(R["IdAddress"].ToString()))
				address = GetLocationAddressAsync(idLocation).Result;

			return new Location
				(
					idLocation,
					R["Name"].ToString(),
					address,
					GetSectorListAsync(idLocation).Result,
					R["PhoneNumber"].ToString(),
					R["Email"].ToString(),
					R["www"].ToString(),
					new Signature(R["User"].ToString(), R["HostIP"].ToString(), Convert.ToDateTime(R["Version"]))
				);
		}

		private Address GetAddressModel(IDataReader R)
		{
			return new Address()
			{
				PlaceName = R["PlaceName"].ToString(),
				StreetName = R["StreetName"].ToString(),
				PropertyNumber = R["PropertyNumber"].ToString(),
				ApartmentNumber = R["ApartmentNumber"].ToString(),
				PostalCode = R["PostalCode"].ToString(),
				PostOffice = R["PostOffice"].ToString()
			};
		}

		private Sector GetSectorModel(IDataReader S)
		{
			return new Sector(Convert.ToUInt64(S["ID"]), S["Name"].ToString(), S["Description"].ToString(), Convert.ToUInt32(S["SeatingCount"]), Convert.ToUInt32(S["SeatingPrice"]));
		}

		public void CreateLocationParams(IDbCommand cmd)
		{
			cmd.Parameters.Add(dbContext.CreateParameter("?Name", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("?IdAddress", DbType.Int64, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("?PhoneNumber", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("?Email", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("?www", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("?User", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("?HostIP", DbType.String, cmd));
		}
	}
}
