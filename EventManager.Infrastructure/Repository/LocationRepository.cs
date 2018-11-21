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
	public class LocationRepository : Repository<Location>, ILocationRepository
	{
		readonly IAddressRepository _addressRepo;
		readonly ISectorRepository _sectorRepo;
		public LocationRepository(IDataBaseContext context, ILocationSql locationSql, IAddressRepository addressRepo, ISectorRepository sectorRepo) : base(context, locationSql)
		{
			_addressRepo = addressRepo;
			_sectorRepo = sectorRepo;
			//RefreshRepo();
		}

		//private void RefreshRepo()
		//{
		//	contentList = GetListAsync(null, CreateLocation).Result;
		//}

		//public async Task<Location> GetLocation(long idLocation)
		//{
		//	var sectors = _sectorRepo.GetSectorList(idLocation).Result;
		//	var l = contentList.Where(L => L.Id == idLocation).FirstOrDefault();
		//	var location = new Location(l.Id, l.Name, l.Address, sectors, l.PhoneNumber, l.Email, l.WWW, l.Creator);
		//	return await Task.FromResult(location);
		//} 

		//public async Task<IEnumerable<Location>> GetLocationList(string name)
		//{
		//	return await Task.FromResult(contentList.Where(L => L.Name.StartsWith(name)));
		//}

		public Location CreateLocation(IDataReader R)
		{
			var idLocation = Convert.ToInt64(R["ID"]);
			Address address = null;

			if (!string.IsNullOrEmpty(R["IdAddress"].ToString()))
				address = _addressRepo.GetAsync(Convert.ToInt64(R["IdAddress"]), _addressRepo.CreateAddress).Result;

			return new Location
				(
					idLocation,
					R["Name"].ToString(),
					address,
					_sectorRepo.GetListAsync(idLocation, _sectorRepo.CreateSector).Result,
					R["PhoneNumber"].ToString(),
					R["Email"].ToString(),
					R["www"].ToString(),
					new Signature
					(
						R["User"].ToString(),
						R["HostIP"].ToString(),
						Convert.ToDateTime(R["Version"])
					)
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
