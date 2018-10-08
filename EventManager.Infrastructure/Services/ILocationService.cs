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
		Task<LocationDto> GetAsync(long id);
		Task<IEnumerable<LocationDto>> BrowseAsync(string name = null);
		Task CreateAsync(string name, long? idAddress, string phoneNumber, string email, string www, string creator, string hostIP);
		Task CreateAddressAsync(string placeName, string streetName, string propertyNumber, string apartmentNumber, string postalCode, string postOffice);
		Task<ISet<Sector>> CreateSectorCollectionAsync(long locationId);
		Task UpdateAsync(long id, string name, long? idAddress, string phoneNumber, string email, string www, string modifier, string hostIP);
		Task DeleteSectorsAsync(ISet<Sector> Sectors);
		Task DeleteAsync(long id);
	}
}
