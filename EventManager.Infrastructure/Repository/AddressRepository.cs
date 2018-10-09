﻿using EventManager.Core.DataBaseContext;
using EventManager.Core.DataBaseContext.SQL;
using EventManager.Core.Domain;
using EventManager.Core.Globals;
using EventManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EventManager.Infrastructure.Repository
{
	public class AddressRepository : Repository<Address>, IAddressRepository
	{
		public AddressRepository(IDataBaseContext context, IAddressSql addressSql) : base(context, addressSql)
		{
			RefreshRepo();
			RecordAffected -= (s, ex) =>  RefreshRepo(); 
			RecordAffected += (s, ex) => RefreshRepo();
		}

		private void RefreshRepo()
		{
			objectList = GetListAsync(null, CreateAddress).Result;
		}
		public Address GetAddress(long idAddress)
		{
			return objectList.Where(A => A.Id == idAddress).FirstOrDefault();
		}

		public IEnumerable<Address> GetAddressList(string name)
		{
			return objectList.Where(A => A.PlaceName.StartsWith(name));
		}

		public Address CreateAddress(IDataReader R)
		{
			return new Address
				(
					Convert.ToInt64(R["ID"]),
					R["PlaceName"].ToString(),
					R["StreetName"].ToString(),
					R["PropertyNumber"].ToString(),
					R["ApartmentNumber"].ToString(),
					R["PostalCode"].ToString(),
					R["PostOffice"].ToString(),
					new Signature
						(
							R["User"].ToString(),
							R["HostIP"].ToString(),
							Convert.ToDateTime(R["Version"].ToString())
						)
				);
		}

		public void CreateInsertParams(IDbCommand cmd)
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
	}
}
