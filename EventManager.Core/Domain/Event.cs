using EventManager.Core.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Core.Domain
{
    class Event : Entity
    {
	
		public string Name { get; protected set; }
		public string Description { get; protected set; }
		public DateTime StartDate { get; protected set; }
		public DateTime EndDate { get; protected set; }
		public Signature Owner { get; protected set; }
		public Signature Modifier { get; protected set; }
		public IEnumerable<Ticket> Tickets { get; protected set; }

		protected Event() { }
		public Event(Guid id, string name, string description, DateTime startDate, DateTime endDate, Signature owner, IEnumerable<Ticket> tickets)
		{
			Tickets = tickets;
			
		}
	}
}
