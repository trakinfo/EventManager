using System;

namespace EventManager.Infrastructure.Command.Event
{
	public class NewEvent
    {
		public string Name { get; set; }
		public string Description { get; set; }
		public long? IdLocation { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Creator { get; set; }
		public string HostIP { get; set; }
	}
}
