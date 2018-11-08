using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Core.DataBaseContext.SQL
{
	public interface ISectorSql :ISql
	{
		string SelectMany(long idLocation);
		string UpdateLocation(long sectorId);
	}
}
