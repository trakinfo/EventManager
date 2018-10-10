using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EventManager.Core.DataBaseContext;
using EventManager.Core.DataBaseContext.SQL;
using EventManager.Core.Domain;
using EventManager.Core.Globals;
using EventManager.Core.Repository;

namespace EventManager.Infrastructure.Repository
{
	public class LocationRepository : Repository<Location>, ILocationRepository
	{
		readonly IAddressRepository _addressRepo;
		readonly ISectorRepository _sectorRepo;
		public LocationRepository(IDataBaseContext context, ILocationSql locationSql, IAddressRepository addressRepo, ISectorRepository sectorRepo) : base(context, locationSql)
		{
			_addressRepo = addressRepo;
			_sectorRepo = sectorRepo;
			RefreshRepo();
			RecordAffected -= (s, ex) => RefreshRepo();
			RecordAffected += (s, ex) => RefreshRepo();
		}

		private void RefreshRepo()
		{
			objectList = GetListAsync(null, CreateLocation).Result;
		}

		public Location GetLocation(long idLocation)
		{
			return objectList.Where(L => L.Id == idLocation).FirstOrDefault();
		}

		public IEnumerable<Location> GetLocationList(string name)
		{
			return objectList.Where(L => L.Name.StartsWith(name));
		}

		public Location CreateLocation(IDataReader R)
		{
			var idLocation = Convert.ToInt64(R["ID"]);
			Address address = null;

			if (!string.IsNullOrEmpty(R["IdAddress"].ToString()))
				address = _addressRepo.GetAddress(Convert.ToInt64(R["IdAddress"]));
				//address = _addressRepo.GetAsync(idLocation, _addressRepo.GetAddress).Result;
					
			return new Location
				(
					idLocation,
					R["Name"].ToString(),
					address,
					_sectorRepo.GetSectorList(null).Where(S => S.LocationId == idLocation),
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
