using EventManager.Core.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Core.Domain
{
    public class Event : Entity
    {
		private ISet<Signature> _modifier = new HashSet<Signature>();
		
		public string Name { get; protected set; }
		public string Description { get; protected set; }
		public Location Location { get; protected set; }
		public DateTime StartDate { get; protected set; }
		public DateTime EndDate { get; protected set; }
		public IEnumerable<Signature> Modifier  => _modifier; 
		

		protected Event() { }
		public Event(ulong id, string name, string description, Location location, DateTime startDate, DateTime endDate, Signature creator)
		{
			Id = id;
			Creator = creator;
			UpdateEvent(name, description, location, startDate, endDate, null);
		}
		public void UpdateEvent(string name, string description, Location location, DateTime startDate, DateTime endDate, Signature modifier)
		{
			Name = name;
			Description = description;
			Location = location;
			StartDate = startDate;
			EndDate = endDate;
			_modifier.Add(modifier);
		}
		//public void AddTickets(ISet<Ticket> tickets)
		//{
		//	foreach (var ticket in tickets)
		//	{
		//		_tickets.Add(ticket);
		//	}
		//}
		//public void RemoveTickets(ISet<Ticket> tickets)
		//{
		//	foreach (var ticket in tickets)
		//	{
		//		_tickets.Remove(ticket);
		//	}
		//}
	}
}
