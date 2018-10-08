using EventManager.Core.Domain;
using System;
using System.Data;

namespace EventManager.Core.Repository
{
	public interface IAddressRepository : IRepository
	{
		event EventHandler RecordAffected;
		void CreateInsertParams(IDbCommand cmd);
		Address CreateAddress(IDataReader R);
		Address GetAddress(long id);
	}
}
