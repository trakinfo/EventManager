using EventManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;

namespace EventManager.Core.Repository
{
	public interface IAddressRepository : IRepository
	{
		IEnumerable<Address> AddressList { get; }
		event EventHandler RecordAffected;
		void CreateInsertParams(IDbCommand cmd);
		Address CreateAddress(IDataReader R);
		Address GetAddress(long id);
	}
}
