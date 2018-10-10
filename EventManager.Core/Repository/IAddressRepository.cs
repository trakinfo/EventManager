using EventManager.Core.Domain;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace EventManager.Core.Repository
{
	public interface IAddressRepository : IRepository
	{
		Task<Address> GetAddress(long id);
		Task<IEnumerable<Address>> GetAddressList(string name);
		void CreateInsertParams(IDbCommand cmd);
		Address CreateAddress(IDataReader R);
	}
}
