using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Core.DataBaseContext.SQL
{
    public interface ISql
    {
		string Select(long id);
		string SelectMany(string name);
		string SelectMany();
		string Insert();
		string Update();
		string Delete();
	}
}
