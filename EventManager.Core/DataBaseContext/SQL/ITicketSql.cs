﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager.Core.DataBaseContext.SQL
{
	public interface ITicketSql : ISql
	{
		string SelectMany(long idEvent);
		string Puchase();
	}
}
