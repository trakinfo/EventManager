using EventManager.Core.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Core.Domain
{
    class Event : Entity
    {
		private ISet<Signature> _modifier = new HashSet<Signature>();
		private ISet<Ticket> _tickets;

		public string Name { get; protected set; }
		public string Description { get; protected set; }
		public DateTime StartDate { get; protected set; }
		public DateTime EndDate { get; protected set; }
		public IEnumerable<Signature> Modifier  => _modifier; 
		public IEnumerable<Ticket> Tickets => _tickets;

		protected Event() { }
		public Event(Guid id, string name, string description, DateTime startDate, DateTime endDate, Signature creator, ISet<Ticket> tickets)
		{
			Id = id;
			Creator = creator;
			UpdateEvent(name, description, startDate, endDate, null);
			_tickets = tickets;
		}
		public void UpdateEvent(string name, string description, DateTime startDate, DateTime endDate, Signature modifier)
		{
			Name = name;
			Description = description;
			StartDate = startDate;
			EndDate = endDate;
			_modifier.Add(modifier);
		}
		public void AddTickets(ISet<Ticket> tickets)
		{
			foreach (var ticket in tickets)
			{
				_tickets.Add(ticket);
			}
		}
		public void RemoveTickets(ISet<Ticket> tickets)
		{
			foreach (var ticket in tickets)
			{
				_tickets.Remove(ticket);
			}
		}
	}
}
