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
		public int SeatingRangeStart { get; protected set; }
		public int SeatingRangeEnd { get; protected set; }
		public int SeatingCount => GetSeatingCount();
		public decimal SeatingPrice { get; protected set; }
		public long LocationId { get; protected set; }
		public IEnumerable<Ticket> Tickets { get; set; }

		protected Sector() { }
		public Sector(long id, string name, string description, int seatingRangeStart, int seatingRangeEnd, decimal seatingPrice, long locationId, Signature creator)
		{
			Add(id, name, description, seatingRangeStart, seatingRangeEnd, seatingPrice, locationId, creator);
		}
		void Add(long id, string name, string description, int seatingRangeStart, int seatingRangeEnd, decimal seatingPrice, long locationId, Signature creator)
		{
			Id = id;
			Creator = creator;
			Update(name, description, seatingRangeStart, seatingRangeEnd, seatingPrice, locationId, null);
		}
		public void Update(string name, string description, int seatingRangeStart, int seatingRangeEnd, decimal seatingPrice, long locationId, Signature modifier)
		{
			Name = name;
			Description = description;
			SeatingRangeStart = seatingRangeStart;
			SeatingRangeEnd = seatingRangeEnd;
			SeatingPrice = seatingPrice;
			LocationId = locationId;
			if (modifier != null) ModifierList.Add(modifier);
		}
		int GetSeatingCount()
		{
			var seatingCount = SeatingRangeEnd - SeatingRangeStart + 1;
			return seatingCount;
		}
	}
}
