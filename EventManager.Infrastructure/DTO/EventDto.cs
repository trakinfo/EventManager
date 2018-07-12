using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Infrastructure.DTO
{
    public class EventDto
    {
		public long ID { get; set; }
		public string Name { get; set; }
		public string Description { get;  set; }
		public string LocationAddress { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string www { get; set; }
		public DateTime StartDate { get;  set; }
		public DateTime EndDate { get;  set; }
		public int TicketCount { get; set; }
	}
}
