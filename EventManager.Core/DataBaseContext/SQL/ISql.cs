using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Core.DataBaseContext.SQL
{
    public interface ISql
    {
		string SelectMany(string name);
		string Select(ulong id);
		string Insert();
		string Update();
		string Delete();
	}
}
