﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Infrastructure.DataBaseContext.MySql.SQL
{
    public static class LocationSql
    {
		internal static string SelectLocations(string name)
		{
			return $"SELECT l.ID,l.Name,l.IdAddress,l.PhoneNumber,l.Email,l.www,l.User,l.HostIP,l.Version FROM location l WHERE l.Name LIKE '{name}%' ORDER BY l.Name;";
		}

		internal static string SelectLocation(ulong idLocation)
		{
			return $"SELECT l.ID,l.Name, l.IdAddress, l.PhoneNumber, l.Email, l.www, l.User,l.HostIP,l.Version FROM location l WHERE l.ID={idLocation};";
		}

		internal static string SelectAddress(ulong idLocation)
		{
			return $"SELECT a.PlaceName, a.StreetName, a.PropertyNumber, a.ApartmentNumber, a.PostalCode, a.PostOffice FROM address a INNER JOIN location l ON a.ID=l.IdAddress WHERE l.ID={idLocation};";
		}

		internal static string SelectSector(ulong idLocation)
		{
			return $"SELECT s.ID,s.Name,s.Description,s.SeatingCount FROM sector s WHERE s.IdLocation={idLocation};";
		}

		internal static string InsertLocation()
		{
			return "INSERT INTO location VALUES(null,?Name,?IdAddress,?PhoneNumber,?Email,?www,?User,?HostIP,NULL);";
		}
	}
}