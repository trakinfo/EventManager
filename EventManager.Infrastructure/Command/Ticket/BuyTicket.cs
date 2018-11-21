using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Infrastructure.Command.Ticket
{
	public class BuyTicket
	{
		public long Id { get; set; }
		public string UserName { get; set; }
		public DateTime PuchaseDate { get => DateTime.Now; }
	}
}
