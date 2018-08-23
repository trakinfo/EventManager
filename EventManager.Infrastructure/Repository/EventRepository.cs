using EventManager.Core.DataBaseContext;
using EventManager.Core.DataBaseContext.SQL;
using EventManager.Core.Domain;
using EventManager.Core.Globals;
using EventManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Infrastructure.Repository
{
	public class EventRepository : IEventRepository
	{
		IDataBaseContext dbContext;
		ILocationRepository locationRepository;
		IEventSql sql;

		public EventRepository(IDataBaseContext context, ILocationRepository locationRepo, IEventSql eventSql)
		{
			dbContext = context;
			locationRepository = locationRepo;
			sql = eventSql;
		}
		public async Task<Event> GetEventAsync(ulong eventId)
		{
			var eventDR = dbContext.FetchDataRowAsync(sql.SelectEvent(eventId), GetEvent);
			//var idEvent = Convert.ToUInt64(eventDR["ID"]);
			//Location location = null;
			//if (!string.IsNullOrEmpty(eventDR["IdLocation"].ToString()))
			//	location = await GetLocationAsync(idEvent, Convert.ToUInt64(eventDR["IdLocation"]));

			//var _event = new Event
			//	(
			//		idEvent,
			//		eventDR["Name"].ToString(),
			//		eventDR["Description"].ToString(),
			//		location,
			//		Convert.ToDateTime(eventDR["StartDate"]),
			//		Convert.ToDateTime(eventDR["EndDate"]),
			//		new Signature(eventDR["User"].ToString(), eventDR["HostIP"].ToString(), Convert.ToDateTime(eventDR["Version"]))
			//	);
			return await eventDR;
		}

		public async Task<Event> GetEventAsync(string name)
		{
			var eventDR = dbContext.FetchDataRowAsync(sql.SelectEvent(name), GetEvent);
			//var idEvent = Convert.ToUInt64(eventDR["ID"]);
			//Location location = null;
			//if (!string.IsNullOrEmpty(eventDR["IdLocation"].ToString()))
			//	location = await GetLocationAsync(idEvent, Convert.ToUInt64(eventDR["IdLocation"]));

			//var _event = new Event
			//	(
			//		idEvent,
			//		eventDR["Name"].ToString(),
			//		eventDR["Description"].ToString(),
			//		location,
			//		Convert.ToDateTime(eventDR["StartDate"]),
			//		Convert.ToDateTime(eventDR["EndDate"]),
			//		new Signature(eventDR["User"].ToString(), eventDR["HostIP"].ToString(), Convert.ToDateTime(eventDR["Version"]))
			//	);
			return await eventDR;
		}

		public async Task<IEnumerable<Event>> GetEventListAsync(string name = "")
		{
			//var events = new HashSet<Event>();
			var events = await dbContext.FetchDataRowSetAsync(sql.SelectEvents(name), GetEvent);
			//using (var conn = dbContext.GetConnection())
			//{
			//	conn.Open();
			//	var T = conn.BeginTransaction();
			//	var cmd = conn.CreateCommand();
			//	cmd.CommandText = sql.SelectEvents(name);
			//	cmd.Transaction = T;
			//	try
			//	{
			//		using (var R = cmd.ExecuteReader())
			//		{
			//			while (R.Read())
			//			{
			//				var idEvent = Convert.ToUInt64(R["ID"]);
			//				Location location = null;

			//				if (!string.IsNullOrEmpty(R["IdLocation"].ToString()))
			//					location = await GetLocationAsync(idEvent, Convert.ToUInt64(R["IdLocation"]));

			//				events.Add(new Event
			//					(
			//						idEvent,
			//						R["Name"].ToString(),
			//						R["Description"].ToString(),
			//						location,
			//						Convert.ToDateTime(R["StartDate"]),
			//						Convert.ToDateTime(R["EndDate"]),
			//						new Signature(R["User"].ToString(), R["HostIP"].ToString(), Convert.ToDateTime(R["Version"]))
			//					)
			//					);
			//			}
			//		}
			//		T.Commit();
			//	}
			//	catch (Exception ex)
			//	{
			//		T.Rollback();
			//		Console.WriteLine(ex.Message);
			//	}

			//}
			return await Task.FromResult(events.AsEnumerable());
		}

		async Task<Location> GetLocationAsync(ulong idEvent, ulong idLocation)
		{
			var location = await locationRepository.GetAsync(idLocation);

			foreach (var S in location.Sectors)
			{
				S.Tickets = await GetTicketListAsync(idEvent, S.Id);
			}
			return await Task.FromResult(location);
		}

		async Task<ISet<Ticket>> GetTicketListAsync(ulong idEvent, ulong idSector)
		{
			//var tickets = new HashSet<Ticket>();
			var tickets = dbContext.FetchDataRowSetAsync(sql.SelectTicket(idEvent, idSector), GetTicket);
			//using (var conn = dbContext.GetConnection())
			//{
			//	conn.Open();
			//	var T = conn.BeginTransaction();
			//	var cmd = conn.CreateCommand();
			//	cmd.CommandText = sql.SelectTicket(idEvent, idSector);
			//	cmd.Transaction = T;
			//	try
			//	{
			//		using (var R = cmd.ExecuteReader())
			//		{
			//			while (R.Read())
			//			{
			//				tickets.Add(new Ticket(Convert.ToUInt64(R["ID"]), Convert.ToInt32(R["SeatingNumber"]), Convert.ToDecimal(R["Price"]), null));
			//			}
			//		}
			//		T.Commit();
			//	}
			//	catch (Exception ex)
			//	{
			//		T.Rollback();
			//		Console.WriteLine(ex.Message);
			//	}
			//}
			return await tickets;
		}

		public async Task AddEventAsync(ISet<object[]> paramValue)
		{
			await dbContext.AddDataAsync(sql.InsertEvent(), paramValue, CreateEventParams);
			//return await Task.FromResult(newEventId);
		}
		void CreateEventParams(IDbCommand cmd)
		{
			cmd.Parameters.Add(dbContext.CreateParameter("?Name", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("?Description", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("?IdLocation", DbType.Int64, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("?StartDate", DbType.DateTime, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("?EndDate", DbType.DateTime, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("?User", DbType.String, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("?HostIP", DbType.String, cmd));
		}

		//public async Task UpdateEventAsync(IDictionary<string, object> sqlParams)
		//{
		//await dbContext.ExecuteCommandAsync(sqlParams, sql.UpdateEvent());
		//}

		//public async Task DeleteEventAsync(IDictionary<string, object> sqlParams)
		//{
		//	await dbContext.ExecuteCommandAsync(sqlParams, sql.DeleteEvent());
		//}

		public async Task<int> AddTickets(object[] sqlParamValue, uint seatingCount)
		{
			var HS = new HashSet<object[]>();
			//object[] sqlParamValueSet = new object[4];
			//sqlParamValue.CopyTo(sqlParamValueSet, 0);
			Array.Resize(ref sqlParamValue, sqlParamValue.Length + 1);
			for (int i = 0; i < seatingCount; i++)
			{
				sqlParamValue[3] = i + 1;
				HS.Add(sqlParamValue.ToArray());
			}
			return await dbContext.AddDataAsync(sql.InsertTicket(), HS, CreateTicketParams);
		}

		private void CreateTicketParams(IDbCommand cmd)
		{
			cmd.Parameters.Add(dbContext.CreateParameter("?IdEvent", DbType.Int64, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("?IdSector", DbType.Int64, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("?Price", DbType.Decimal, cmd));
			cmd.Parameters.Add(dbContext.CreateParameter("?SeatingNumber", DbType.Int32, cmd));
		}

		Event GetEvent(IDataReader R)
		{
			var idEvent = Convert.ToUInt64(R["ID"]);
			Location location = null;

			if (!string.IsNullOrEmpty(R["IdLocation"].ToString()))
				location = GetLocationAsync(idEvent, Convert.ToUInt64(R["IdLocation"])).Result;
			return new Event
				(
					idEvent,
					R["Name"].ToString(),
					R["Description"].ToString(),
					location,
					Convert.ToDateTime(R["StartDate"]),
					Convert.ToDateTime(R["EndDate"]),
					new Signature(R["User"].ToString(), R["HostIP"].ToString(), Convert.ToDateTime(R["Version"]))
				);
		}

		Ticket GetTicket(IDataReader R)
		{
			return new Ticket(Convert.ToUInt64(R["ID"]), Convert.ToInt32(R["SeatingNumber"]), Convert.ToDecimal(R["Price"]), null);
		}
	}
}
