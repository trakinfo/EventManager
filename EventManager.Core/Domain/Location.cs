using System.Collections.Generic;

namespace EventManager.Core.Domain
{
	public class Location
	{
		public string Name { get; set; }
		public Address Address { get; set; }
		public IEnumerable<Sector> Sectors { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string WWW { get; set; }
	}
}