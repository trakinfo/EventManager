namespace EventManager.Core.Domain
{
	public class Address
	{
		public string PlaceName { get; set; }
		public string StreetName { get; set; }
		public string PropertyNumber { get; set; }
		public string ApartmentNumber { get; set; }
		public string PostalCode { get; set; }
		public string PostOffice { get; set; }

		public override string ToString()
		{
			var address = $"{PlaceName}, ".TrimStart(", ".ToCharArray());

			address += $"ul. {StreetName}".TrimEnd("ul. ".ToCharArray());
			address += $" {PropertyNumber}/{ApartmentNumber}".Trim(" /".ToCharArray());
			address += $", {PostalCode}".TrimEnd(", ".ToCharArray());
			address += $" {PostOffice}".TrimEnd(" ".ToCharArray());

			return address;
		}	
	}
}