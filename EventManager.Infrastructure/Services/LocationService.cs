using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EventManager.Core.Domain;
using EventManager.Core.Repository;
using EventManager.Infrastructure.DTO;

namespace EventManager.Infrastructure.Services
{
	public class LocationService : ILocationService
	{
		readonly ILocationRepository locationRepo;
		readonly IAddressRepository addressRepo;
		readonly IMapper mapper;
		public LocationService(ILocationRepository _locationRepo, IAddressRepository _addressRepo, IMapper _mapper)
		{
			locationRepo = _locationRepo;
			addressRepo = _addressRepo;
			mapper = _mapper;
		}
		public async Task<IEnumerable<LocationDto>> BrowseAsync(string name = null)
		{
			var locationList = await locationRepo.GetListAsync(name, locationRepo.GetLocation);
			return mapper.Map<IEnumerable<LocationDto>>(locationList);
		}

		public async Task CreateAddressAsync(string placeName, string streetName, string propertyNumber, string apartmentNumber, string postalCode, string postOffice)
		{
			var sqlParamValue = new object[] { placeName, streetName, propertyNumber, apartmentNumber, postalCode, postOffice };
			await addressRepo.AddAsync<Address>(sqlParamValue,addressRepo.CreateInsertParams);
		}

		public async Task CreateAsync(string name, ulong? idAddress, string phoneNumber, string email, string www, string creator, string hostIP)
		{
			var sqlParamValue = new object[] { name, idAddress, phoneNumber, email, www, creator, hostIP };
			await locationRepo.AddAsync<Location>(sqlParamValue, locationRepo.CreateInsertParams);
		}

		public Task<ISet<Sector>> CreateSectorCollectionAsync(ulong locationId)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(ulong id)
		{
			throw new NotImplementedException();
		}

		public Task DeleteSectorsAsync(ISet<Sector> Sectors)
		{
			throw new NotImplementedException();
		}

		public async Task<LocationDto> GetAsync(ulong id)
		{
			var location = await locationRepo.GetAsync(id, locationRepo.GetLocation);
			return mapper.Map<LocationDto>(location);
		}



		public Task UpdateAsync(ulong id, string name, ulong? idAddress, string phoneNumber, string email, string www, string modifier, string hostIP)
		{
			throw new NotImplementedException();
		}
	}
}
