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

		protected Ticket() { }

		public Ticket(ulong id, int seatingNumber, decimal price, Signature creator)
		{
			Add(id, seatingNumber, price, creator);
		}
		private void Add(ulong id, int seatingNumber, decimal price, Signature creator)
		{
			Id = id;
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
