using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Core.DataBaseContext.SQL
{
    public interface ILocationSql : ISql
    {
		//string SelectLocations(string name);
		//string SelectLocation(ulong id);
		//string SelectLocation(string name);
		string SelectAddress(ulong id);
		string SelectSector(ulong id);
		//string InsertLocation();
		//string UpdateLocation();
		//string DeleteLocation();
	}
}
