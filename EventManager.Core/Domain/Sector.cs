using EventManager.Core.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Core.Domain
{
    public class Sector : Entity
    {
		public string Name { get; protected set; }
		public string Description { get; protected set; }
		public uint SeatingCount { get; set; }
		public IEnumerable<Ticket> Tickets { get; protected set; }

		protected Sector() { }
		public Sector(ulong id, string name, string description, uint seatingCount,IEnumerable<Ticket> tickets)
		{
			Id = id;
			Name = name;
			Description = description;
			SeatingCount = seatingCount;
			Tickets = tickets;
		}
    }
}
