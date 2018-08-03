using EventManager.Core.Domain;
using EventManager.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Infrastructure.Services
{
    public interface ILocationService
    {
		Task<LocationDto> GetAsync(ulong id);
		Task<long> CreateAsync(string name, ulong? idAddress, string phoneNumber, string email, string www, string creator, string hostIP);
		Task<IEnumerable<LocationDto>> BrowseAsync(string name = null);
		Task<ISet<Sector>> CreateSectorCollectionAsync(ulong locationId);
		Task UpdateAsync(ulong id, string name, ulong? idAddress, string phoneNumber, string email, string www, string modifier, string hostIP);
		Task DeleteSecorsAsync(ISet<Sector> Sectors);
		Task DeleteAsync(ulong id);
	}
}
