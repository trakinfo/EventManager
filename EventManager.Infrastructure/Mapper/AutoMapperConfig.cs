using AutoMapper;
using EventManager.Core.Domain;
using EventManager.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventManager.Infrastructure.Mapper
{
	public static class AutoMapperConfig
	{
		public static IMapper Initialize()
		{
			return new MapperConfiguration(
				cfg =>
			{
				cfg.CreateMap<Event, EventDto>()
				.ForMember(DTO => DTO.AvailableTicketCount, e => e.MapFrom(p => p.Location.Sectors.Sum(x => x.Tickets.Count() - x.Tickets.Where(y => y.IsPurchased).Count())))
				.ForMember(DTO => DTO.Email, e => e.MapFrom(p => p.Location.Email))
				.ForMember(DTO => DTO.PhoneNumber, e => e.MapFrom(p => p.Location.PhoneNumber))
				.ForMember(DTO => DTO.www, e => e.MapFrom(p => p.Location.WWW))
				.ForMember(DTO => DTO.LocationAddress, e => e.MapFrom(p => p.Location.Address));
				cfg.CreateMap<Location, LocationDto>()
				.ForMember(DTO => DTO.Address, l => l.MapFrom(p => p.Address));
				cfg.CreateMap<Address, AddressDto>().ForMember(DTO => DTO.Address, a => a.MapFrom(p => p.ToString()));
			}
			).CreateMapper();
		}
	}
}
