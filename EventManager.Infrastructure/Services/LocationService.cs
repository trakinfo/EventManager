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
		readonly IMapper mapper;
		public LocationService(ILocationRepository repo, IMapper _mapper)
		{
			locationRepo = repo;
			mapper = _mapper;
		}
		public async Task<IEnumerable<LocationDto>> BrowseAsync(string name = null)
		{
			var locationList = await locationRepo.GetListAsync(name, locationRepo.GetLocationModel);
			return mapper.Map<IEnumerable<LocationDto>>(locationList);
		}

		public async Task CreateAsync(string name, ulong? idAddress, string phoneNumber, string email, string www, string creator, string hostIP)
		{
			//var SqlParams = new Dictionary<string, object>();

			//SqlParams.Add("?Name", name);
			//SqlParams.Add("?IdAddress", idAddress);
			//SqlParams.Add("?PhoneNumber", phoneNumber);
			//SqlParams.Add("?Email", email);
			//SqlParams.Add("?www", www);
			//SqlParams.Add("?User", creator);
			//SqlParams.Add("?HostIP", hostIP);
			var sqlParamValue = new object[] { name, idAddress, phoneNumber, email, www, creator, hostIP };
			await locationRepo.AddAsync<Location>(sqlParamValue, locationRepo.CreateLocationParams);
			//return await locationRepo.AddAsync(SqlParams);
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
			var location = await locationRepo.GetAsync(id, locationRepo.GetLocationModel);
			return mapper.Map<LocationDto>(location);
		}



		public Task UpdateAsync(ulong id, string name, ulong? idAddress, string phoneNumber, string email, string www, string modifier, string hostIP)
		{
			throw new NotImplementedException();
		}
	}
}
