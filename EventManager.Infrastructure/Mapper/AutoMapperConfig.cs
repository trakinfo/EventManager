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
				.ForMember(DTO => DTO.TicketCount, e => e.MapFrom(p => p.Location.Sectors.Sum(x => x.Tickets.Count())))
				.ForMember(DTO => DTO.Email, e => e.MapFrom(p => p.Location.Email))
				.ForMember(DTO => DTO.PhoneNumber, e => e.MapFrom(p => p.Location.PhoneNumber))
				.ForMember(DTO => DTO.www, e => e.MapFrom(p => p.Location.WWW))
				.ForMember(DTO => DTO.LocationAddress, e => e.MapFrom(p => p.Location.Address.ToString()));
			}
			).CreateMapper();
		}
	}
}
