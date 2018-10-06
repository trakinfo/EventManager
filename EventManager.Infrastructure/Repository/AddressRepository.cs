using EventManager.Core.DataBaseContext;
using EventManager.Core.DataBaseContext.SQL;
using EventManager.Core.Domain;
using EventManager.Core.Globals;
using EventManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EventManager.Infrastructure.Repository
{
	public class AddressRepository : Repository, IAddressRepository
	{
		public IEnumerable<Address> AddressList { get; protected set; }
		public AddressRepository(IDataBaseContext context, IAddressSql addressSql) : base(context, addressSql)
		{
			RefreshRepo();
		}

		private void RefreshRepo()
		{
			AddressList = GetListAsync(null, GetAddress).Result;
		}

		public Address GetAddress(IDataReader R)
		{
			return new Address
				(
					Convert.ToUInt64(R["ID"]),
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
