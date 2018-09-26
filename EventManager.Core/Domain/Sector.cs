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
		public uint SeatingRangeStart { get; protected set; }
		public uint SeatingRangeEnd { get; protected set; }
		public uint SeatingCount => GetSeatingCount();
		public decimal SeatingPrice { get; protected set; }
		public IEnumerable<Ticket> Tickets { get; set; }

		protected Sector() { }
		public Sector(ulong id, string name, string description, uint seatingRangeStart, uint seatingRangeEnd, decimal seatingPrice, Signature creator)
		{
			Add(id, name, description, seatingRangeStart, seatingRangeEnd, seatingPrice, creator);
		}
		void Add(ulong id, string name, string description, uint seatingRangeStart, uint seatingRangeEnd, decimal seatingPrice, Signature creator)
		{
			Id = id;
			Creator = creator;
			Update(name, description, seatingRangeStart, seatingRangeEnd, seatingPrice, null);
		}
		public void Update(string name, string description, uint seatingRangeStart, uint seatingRangeEnd, decimal seatingPrice, Signature modifier)
		{
			Name = name;
			Description = description;
			SeatingRangeStart = seatingRangeStart;
			SeatingRangeEnd = seatingRangeEnd;
			SeatingPrice = seatingPrice;
			if (modifier != null) ModifierList.Add(modifier);
		}
		uint GetSeatingCount()
		{
			var seatingCount = SeatingRangeEnd - SeatingRangeStart + 1;
			return seatingCount;
		}
	}
}
