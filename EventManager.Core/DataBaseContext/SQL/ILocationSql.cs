using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Core.DataBaseContext.SQL
{
    public interface ILocationSql : ISql
    {
		//string SelectAddress(ulong id);
		//string SelectSector(ulong id);
		string UpdateAddress();
	}
}
