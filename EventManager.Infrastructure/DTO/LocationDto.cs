using EventManager.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Infrastructure.DTO
{
    public class LocationDto
    {
		public string Name { get; set; }
		public string Address { get; set; }
		public IEnumerable<Sector> Sectors { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string WWW { get; set; }
	}
}
