using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Core.DataBaseContext.SQL
{
	public interface ISectorSql :ISql
	{
		string SelectLocationSectors(ulong idLocation);
		string UpdateLocation(ulong sectorId);
	}
}
