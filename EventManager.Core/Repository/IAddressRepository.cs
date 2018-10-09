using EventManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;

namespace EventManager.Core.Repository
{
	public interface IAddressRepository : IRepository
	{
		Address GetAddress(long id);
		IEnumerable<Address> GetAddressList(string name);
		void CreateInsertParams(IDbCommand cmd);
		Address CreateAddress(IDataReader R);
	}
}
