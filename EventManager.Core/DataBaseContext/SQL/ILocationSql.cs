using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Core.DataBaseContext.SQL
{
    public interface ILocationSql : ISql
    {
		//string SelectAddress(long id);
		//string SelectSector(long id);
		string UpdateAddress();
	}
}
