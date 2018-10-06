using EventManager.Core.Domain;
using System.Data;

namespace EventManager.Core.Repository
{
	public interface IAddressRepository : IRepository
	{
		void CreateInsertParams(IDbCommand cmd);
		Address GetAddress(IDataReader R);
	}
}
