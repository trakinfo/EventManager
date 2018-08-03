using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Infrastructure.Command.Location
{
    public class NewLocation
    {
		public string Name { get; set; }
		public ulong? IdAddress { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string www { get; set; }
		public string Creator { get; set; }
		public string HostIP { get; set; }
    }
}
