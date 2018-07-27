using EventManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Core.Repository
{
    public interface ILocationRepository
    {
		Task<Location> GetAsync(ulong LocationId);
		Task<IEnumerable<Location>> GetListAsync(string name = "");
		Task AddAsync(Location location);
		Task UpdateAsync(Location location);
		Task DeleteAsync(ulong locationId);
	}
}
