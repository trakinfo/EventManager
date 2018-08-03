using System;
using System.Collections.Generic;
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
		readonly IMapper mapper;
		public LocationService(ILocationRepository repo, IMapper _mapper)
		{
			locationRepo = repo;
			mapper = _mapper;
		}
		public async Task<IEnumerable<LocationDto>> BrowseAsync(string name = null)
		{
			var locationList = await locationRepo.GetListAsync(name);
			return mapper.Map<IEnumerable<LocationDto>>(locationList);
		}

		public async Task<long> CreateAsync(string name, ulong? idAddress, string phoneNumber, string email, string www, string creator, string hostIP)
		{
			var SqlParams = new Dictionary<string, object>();

			SqlParams.Add("?Name", name);
			SqlParams.Add("?IdAddress", idAddress);
			SqlParams.Add("?PhoneNumber", phoneNumber);
			SqlParams.Add("?Email", email);
			SqlParams.Add("?www", www);
			SqlParams.Add("?User", creator);
			SqlParams.Add("?HostIP", hostIP);

			return await locationRepo.AddAsync(SqlParams);
		}

		public Task<ISet<Sector>> CreateSectorCollectionAsync(ulong locationId)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(ulong id)
		{
			throw new NotImplementedException();
		}

		public Task DeleteSecorsAsync(ISet<Sector> Sectors)
		{
			throw new NotImplementedException();
		}

		public Task<LocationDto> GetAsync(ulong id)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(ulong id, string name, ulong? idAddress, string phoneNumber, string email, string www, string modifier, string hostIP)
		{
			throw new NotImplementedException();
		}
	}
}
