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
			var address = await dbContext.FetchRecordAsync(((ILocationSql)sql).SelectAddress(idLocation), GetAddress);
			return await Task.FromResult(address);
		}

		async Task<ISet<Sector>> GetSectorListAsync(ulong idLocation)
		{
			var sectors = await dbContext.FetchRecordSetAsync(((ILocationSql)sql).SelectSector(idLocation), GetSectorModel);
			return await Task.FromResult(sectors);
		}

		public Location GetLocation(IDataReader R)
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

		private Address GetAddress(IDataReader R)
		{
			return new Address(Convert.ToUInt64(R["ID"]), R["PlaceName"].ToString(), R["StreetName"].ToString(), R["PropertyNumber"].ToString(), R["ApartmentNumber"].ToString(), R["PostalCode"].ToString(), R["PostOffice"].ToString(),null);
			
		}

		private Sector GetSectorModel(IDataReader S)
		{
			return new Sector(Convert.ToUInt64(S["ID"]), S["Name"].ToString(), S["Description"].ToString(), Convert.ToUInt32(S["SeatingRangeStart"]), Convert.ToUInt32(S["SeatingRangeEnd"]), Convert.ToUInt32(S["SeatingPrice"]),null);
		}

		public void CreateLocationParams(IDbCommand cmd)
		{
			cmd.Parameters.Add(dbContext.CreateParameter("@Name", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@IdAddress", DbType.Int64, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@PhoneNumber", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@Email", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@www", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@User", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@HostIP", DbType.String, cmd));
		}
		public void CreateAddressParams(IDbCommand cmd)
		{
			cmd.Parameters.Add(dbContext.CreateParameter("@PlaceName", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@StreetName", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@PropertyNumber", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@ApartmentNumber", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@PostalCode", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@PostOffice", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@User", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@HostIP", DbType.String, cmd));
		}
		public async Task AddAddressAsync(object[] sqlParamValues)
		{
			await dbContext.AddRecordAsync(((ILocationSql)sql).InsertAddress(), sqlParamValues, CreateAddressParams);
		}
	}
}
