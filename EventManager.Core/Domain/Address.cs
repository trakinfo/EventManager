using EventManager.Core.Globals;

namespace EventManager.Core.Domain
{
	public class Address : Entity
	{
		public string PlaceName { get; set; }
		public string StreetName { get; set; }
		public string PropertyNumber { get; set; }
		public string ApartmentNumber { get; set; }
		public string PostalCode { get; set; }
		public string PostOffice { get; set; }

		protected Address() { }
		public Address(ulong id, string placeName, string streetName, string propertyNumber, string apartmentNumber, string  postalCode, string  postOffice, Signature creator)
		{
			Add(id, placeName, streetName, propertyNumber, apartmentNumber, postalCode, postOffice, creator);
		}
		private void Add(ulong id, string placeName, string streetName, string propertyNumber, string apartmentNumber, string postalCode, string postOffice, Signature creator)
		{
			Id = id;
			Creator = creator;
			Update(placeName, streetName, propertyNumber, apartmentNumber, postalCode, postOffice, null);
		}
		public void Update(string placeName, string streetName, string propertyNumber, string apartmentNumber, string postalCode, string postOffice, Signature modifier)
		{
			PlaceName = placeName;
			StreetName = streetName;
			PropertyNumber = propertyNumber;
			ApartmentNumber = apartmentNumber;
			PostalCode = postalCode;
			PostOffice = postOffice;
			if (modifier != null) ModifierList.Add(modifier);
		}

		public override string ToString()
		{
			var address = $"{PlaceName}";

			address += $", ul. {StreetName}".TrimEnd(", ul. ".ToCharArray());
			address += $" {PropertyNumber}".TrimEnd(" ".ToCharArray());
			address += $"/{ApartmentNumber}".TrimEnd("/".ToCharArray());
			address += $", {PostalCode}".TrimEnd(", ".ToCharArray());
			address += $" {PostOffice}".TrimEnd(" ".ToCharArray());

			return address;
		}	
	}
}