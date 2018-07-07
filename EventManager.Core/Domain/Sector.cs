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
		public int SeatingCount { get; protected set; }

		protected Sector() { }
		public Sector(long id, string name, string description, int seatingCount, Signature creator)
		{
			Id = id;
			Name = name;
			Description = description;
			SeatingCount = seatingCount;
			Creator = creator;
		}
    }
}
