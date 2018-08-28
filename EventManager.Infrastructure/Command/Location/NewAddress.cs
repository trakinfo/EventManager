using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Infrastructure.Command.Location
{
    public class NewAddress
    {
		public string PlaceName { get; set; }
		public string StreetName { get; set; }
		public string PropertyNumber { get; set; }
		public string ApartmentNumber { get; set; }
		public string PostalCode { get; set; }
		public string PostOffice { get; set; }
	}
}
