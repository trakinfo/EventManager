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
		public long? UserId { get; protected set; }
		public bool IsPurchased => UserId.HasValue;
		public DateTime? PurchaseDate { get; protected set; }

		protected Ticket() { }

		public Ticket(long id, int seatingNumber, decimal price, Signature creator)
		{
			Id = id;
			SeatingNumber = seatingNumber;
			Price = price;
			Creator = creator;
		}
    }
}
