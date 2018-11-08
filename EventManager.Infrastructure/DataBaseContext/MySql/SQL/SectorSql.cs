using EventManager.Core.DataBaseContext.SQL;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Infrastructure.DataBaseContext.MySql.SQL
{
	public class SectorSql : ISectorSql
	{
		public string Select(long id)
		{
			return $"SELECT s.ID,s.Name,s.Description,s.SeatingRangeStart,s.SeatingRangeEnd,s.SeatingPrice,s.IdLocation,s.User,s.HostIP,s.Version FROM sector s WHERE s.ID={id};";
		}

		public string SelectMany(string name)
		{
			return $"SELECT s.ID,s.Name,s.Description,s.SeatingRangeStart,s.SeatingRangeEnd,s.SeatingPrice,s.IdLocation,s.User,s.HostIP,s.Version FROM sector s WHERE s.Name LIKE '{name}%' ORDER BY s.Name;";
		}

		public string SelectMany(long idLocation)
		{
			return $"SELECT s.ID,s.Name,s.Description,s.SeatingRangeStart,s.SeatingRangeEnd,s.SeatingPrice,s.User,s.HostIP,s.Version FROM sector s WHERE s.IdLocation={idLocation};";
		}

		public string Insert()
		{
			throw new NotImplementedException();
		}
		
		public string Update()
		{
			throw new NotImplementedException();
		}

		public string Delete()
		{
			throw new NotImplementedException();
		}

		public string UpdateLocation(long sectorId)
		{
			throw new NotImplementedException();
		}

	}
}
