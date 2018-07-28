using EventManager.Core.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Infrastructure.Command.Event
{
    public class NewEvent
    {
		public string Name { get; set; }
		public string Description { get; set; }
		public ulong? IdLocation { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Creator { get; set; }
		public string HostIP { get; set; }
	}
}
