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
		readonly ILocationRepository _locationRepo;
		readonly IAddressRepository _addressRepo;
		readonly IMapper mapper;
		public LocationService(ILocationRepository locationRepo, IAddressRepository addressRepo, IMapper _mapper)
		{
			_locationRepo = locationRepo;
			_addressRepo = addressRepo;
			mapper = _mapper;
		}
		public async Task<IEnumerable<LocationDto>> GetListAsync(string name = null)
		{
			var locationList = await _locationRepo.GetListAsync(name, _locationRepo.CreateLocation);
			return mapper.Map<IEnumerable<LocationDto>>(locationList);
		}

		public async Task<IEnumerable<AddressDto>> GetAddressList(string name = null)
		{
			//var addressList = _addressRepo.GetListAsync(name, _addressRepo.CreateAddress).Result;
			var addressList = await Task.FromResult(_addressRepo.AddressList);
			return mapper.Map<IEnumerable<AddressDto>>(addressList);
		} 

		public async Task CreateAddressAsync(string placeName, string streetName, string propertyNumber, string apartmentNumber, string postalCode, string postOffice)
		{
			var sqlParamValue = new object[] { placeName, streetName, propertyNumber, apartmentNumber, postalCode, postOffice };
			await _addressRepo.AddAsync<Address>(sqlParamValue, _addressRepo.CreateInsertParams);
		}

		public async Task CreateAsync(string name, long? idAddress, string phoneNumber, string email, string www, string creator, string hostIP)
		{
			var sqlParamValue = new object[] { name, idAddress, phoneNumber, email, www, creator, hostIP };
			await _locationRepo.AddAsync<Location>(sqlParamValue, _locationRepo.CreateInsertParams);
		}

		public Task<ISet<Sector>> CreateSectorCollectionAsync(long locationId)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(long id)
		{
			throw new NotImplementedException();
		}

		public Task DeleteSectorsAsync(ISet<Sector> Sectors)
		{
			throw new NotImplementedException();
		}

		public async Task<LocationDto> GetAsync(long id)
		{
			var location = await _locationRepo.GetAsync(id, _locationRepo.CreateLocation);
			return mapper.Map<LocationDto>(location);
		}



		public Task UpdateAsync(long id, string name, long? idAddress, string phoneNumber, string email, string www, string modifier, string hostIP)
		{
			throw new NotImplementedException();
		}
	}
}
