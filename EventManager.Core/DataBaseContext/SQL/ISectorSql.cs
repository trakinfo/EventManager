using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Core.DataBaseContext.SQL
{
	public interface ISectorSql :ISql
	{
		string SelectLocationSectors(long idLocation);
		string UpdateLocation(long sectorId);
	}
}
