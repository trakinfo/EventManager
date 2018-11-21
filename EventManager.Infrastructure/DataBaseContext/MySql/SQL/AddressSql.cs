using EventManager.Core.DataBaseContext.SQL;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Infrastructure.DataBaseContext.MySql.SQL
{
	public class AddressSql : IAddressSql
	{
		public string Select(long id)
		{
			return $"SELECT a.ID, a.PlaceName, a.StreetName, a.PropertyNumber, a.ApartmentNumber, a.PostalCode, a.PostOffice, a.User,a.HostIP,a.Version FROM address a WHERE a.ID={id};";
		}

		public string SelectMany(string name)
		{
			return $"SELECT a.ID, a.PlaceName, a.StreetName, a.PropertyNumber, a.ApartmentNumber, a.PostalCode, a.PostOffice, a.User,a.HostIP,a.Version FROM address a WHERE a.PlaceName LIKE '{name}%' ORDER BY a.PlaceName,a.StreetName;";
		}

		public string Insert()
		{
			return "INSERT INTO address VALUES (null,@placeName,@streetName,@propertyNumber,@apartmentNumber,@postalCode,@postOffice,@User,@HostIP,NULL);";
		}
		
		public string Update()
		{
			throw new NotImplementedException();
		}

		public string Delete()
		{
			throw new NotImplementedException();
		}
	}
}
