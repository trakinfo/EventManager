using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Infrastructure.Command.Ticket
{
	public class NewTicket
	{
		public int? StartRange { get; set; }
		public int? EndRange { get; set; }
		public long? SectorId { get; set; }
		public decimal? Price { get; set; }
		public string Creator { get; set; }
		public string HostIP { get; set; }
	}
}
