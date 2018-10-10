using EventManager.Core.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Core.Domain
{
    public class Ticket : Entity
    {
		public int SeatingNumber { get; protected set; }
		public decimal Price { get; protected set; }
		public string UserId { get; protected set; }
		public bool IsPurchased => !string.IsNullOrEmpty(UserId);
		public DateTime? PurchaseDate { get; protected set; }
		public long EventId { get; protected set; }
		public long SectorId { get; protected set; }

		protected Ticket() { }

		public Ticket(long id, int seatingNumber, decimal price, long eventId, long sectorId, Signature creator)
		{
			Add(id, seatingNumber, price, eventId, sectorId, creator);
		}
		private void Add(long id, int seatingNumber, decimal price, long eventId, long sectorId, Signature creator)
		{
			Id = id;
			EventId = eventId;
			SectorId = sectorId;
			Creator = creator;
			Update(seatingNumber, price, null);
		}
		public void Update(int seatingNumber, decimal price, Signature modifier)
		{
			SeatingNumber = seatingNumber;
			Price = price;
			if (modifier != null) ModifierList.Add(modifier);
		}
	}
}
