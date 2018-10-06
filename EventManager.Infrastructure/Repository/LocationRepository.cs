using System;
using System.Data;
using EventManager.Core.DataBaseContext;
using EventManager.Core.DataBaseContext.SQL;
using EventManager.Core.Domain;
using EventManager.Core.Globals;
using EventManager.Core.Repository;

namespace EventManager.Infrastructure.Repository
{
	public class LocationRepository : Repository, ILocationRepository
	{
		readonly IAddressRepository _addressRepo;
		readonly ISectorRepository _sectorRepo;
		public LocationRepository(IDataBaseContext context, ILocationSql locationSql, IAddressRepository addressRepo, ISectorRepository sectorRepo) : base(context, locationSql)
		{
			_addressRepo = addressRepo;
			_sectorRepo = sectorRepo;
		}

		public Location GetLocation(IDataReader R)
		{
			var idLocation = Convert.ToUInt64(R["ID"]);
			Address address = null;

			if (!string.IsNullOrEmpty(R["IdAddress"].ToString()))
				address = _addressRepo.GetAsync(idLocation, _addressRepo.GetAddress).Result;
					
			return new Location
				(
					idLocation,
					R["Name"].ToString(),
					address,
					_sectorRepo.GetListAsync(idLocation, _sectorRepo.GetSector).Result,
					R["PhoneNumber"].ToString(),
					R["Email"].ToString(),
					R["www"].ToString(),
					new Signature(R["User"].ToString(), R["HostIP"].ToString(), Convert.ToDateTime(R["Version"]))
				);
		}

		public void CreateInsertParams(IDbCommand cmd)
		{
			cmd.Parameters.Add(dbContext.CreateParameter("@Name", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@IdAddress", DbType.Int64, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@PhoneNumber", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@Email", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@www", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@User", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("@HostIP", DbType.String, cmd));
		}
	}
}
