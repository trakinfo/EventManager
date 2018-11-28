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
		public TicketStatus PaymentStatus { get; set; }
		public DateTime? PurchaseDate { get; protected set; }
		public bool IsPurchased => PaymentStatus > 0;
		//public long EventId { get; protected set; }
		public long SectorId { get; protected set; }

		protected Ticket() { }

		public Ticket(long id, int seatingNumber, decimal price, string userId, DateTime? purchaseDate, TicketStatus paymentStatus, long sectorId, Signature creator)
		{
			Add(id, seatingNumber, price, userId, purchaseDate, paymentStatus, sectorId, creator);
		}
		private void Add(long id, int seatingNumber, decimal price, string userId, DateTime? purchaseDate, TicketStatus paymentStatus, long sectorId, Signature creator)
		{
			Id = id;
			UserId = userId;
			PurchaseDate = purchaseDate;
			SectorId = sectorId;
			Creator = creator;
			Update(seatingNumber, price, paymentStatus, null);
		}
		public void Update(int seatingNumber, decimal price, TicketStatus paymentStatus, Signature modifier)
		{
			SeatingNumber = seatingNumber;
			Price = price;
			PaymentStatus = paymentStatus;
			if (modifier != null) ModifierList.Add(modifier);
		}
	}
}
