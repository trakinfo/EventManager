using EventManager.Core.DataBaseContext.SQL;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Infrastructure.DataBaseContext.MySql.SQL
{
    public class LocationSql:ILocationSql
    {
		public string SelectLocations(string name)
		{
			return $"SELECT l.ID,l.Name,l.IdAddress,l.PhoneNumber,l.Email,l.www,l.User,l.HostIP,l.Version FROM location l WHERE l.Name LIKE '{name}%' ORDER BY l.Name;";
		}

		public string SelectLocation(string name)
		{
			throw new NotImplementedException();
		}

		public string SelectLocation(ulong idLocation)
		{
			return $"SELECT l.ID,l.Name, l.IdAddress, l.PhoneNumber, l.Email, l.www, l.User,l.HostIP,l.Version FROM location l WHERE l.ID={idLocation};";
		}

		public string SelectAddress(ulong idLocation)
		{
			return $"SELECT a.PlaceName, a.StreetName, a.PropertyNumber, a.ApartmentNumber, a.PostalCode, a.PostOffice FROM address a INNER JOIN location l ON a.ID=l.IdAddress WHERE l.ID={idLocation};";
		}

		public string SelectSector(ulong idLocation)
		{
			return $"SELECT s.ID,s.Name,s.Description,s.SeatingCount,s.SeatingPrice FROM sector s WHERE s.IdLocation={idLocation};";
		}

		public string UpdateLocation()
		{
			throw new NotImplementedException();
		}

		public string InsertLocation()
		{
			return "INSERT INTO location VALUES(null,?Name,?IdAddress,?PhoneNumber,?Email,?www,?User,?HostIP,NULL);";
		}
		
		public string DeleteLocation()
		{
			return "DELETE FROM location WHERE ID=?ID;";
		}


	}
}
