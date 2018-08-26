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
		Task<IEnumerable<LocationDto>> BrowseAsync(string name = null);
		Task CreateAsync(string name, ulong? idAddress, string phoneNumber, string email, string www, string creator, string hostIP);
		Task<ISet<Sector>> CreateSectorCollectionAsync(ulong locationId);
		Task UpdateAsync(ulong id, string name, ulong? idAddress, string phoneNumber, string email, string www, string modifier, string hostIP);
		Task DeleteSectorsAsync(ISet<Sector> Sectors);
		Task DeleteAsync(ulong id);
	}
}
