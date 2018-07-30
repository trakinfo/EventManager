using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Core.DataBaseContext.SQL
{
    public interface IEventSql
    {
		string SelectEvents(string name);
		string SelectEvent(ulong id);
		string SelectEvent(string name);
		string SelectTicket(ulong idEvent, ulong idSector);
		string InsertEvent();
		string UpdateEvent();
		string UpdateEventLocation();
		string DeleteEvent();
    }
}
