using EventManager.Core.Globals;
using System.Collections.Generic;

namespace EventManager.Core.Domain
{
	public class Location: Entity
	{
		public string Name { get; protected set; }
		public Address Address { get; protected set; }
		public IEnumerable<Sector> Sectors { get; protected set; }
		public string PhoneNumber { get; protected set; }
		public string Email { get; protected set; }
		public string WWW { get; protected set; }

		protected Location() { }
		public Location(long id, string name, Address address, IEnumerable<Sector> sectors, string phoneNmuber, string email, string www, Signature creator)
		{
			Add(id, name, address, sectors, phoneNmuber, email, www, creator);
		}
		private void Add(long id, string name, Address address, IEnumerable<Sector> sectors, string phoneNmuber, string email, string www, Signature creator)
		{
			Id = id;
			Creator = creator;
			Update(name, address, sectors, phoneNmuber, email, www, null);
		}
		public void Update(string name, Address address, IEnumerable<Sector> sectors, string phoneNmuber, string email, string www, Signature modifier)
		{
			Name = name;
			Address = address;
			Sectors = sectors;
			PhoneNumber = phoneNmuber;
			Email = email;
			WWW = www;
			if (modifier != null) ModifierList.Add(modifier);
		}
	}
}