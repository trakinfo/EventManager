﻿using EventManager.Core.DataBaseContext;
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
			var eventDR = dbContext.FetchRecordAsync(sql.SelectEvent(eventId), GetEvent);
			return await eventDR;
		}

		public async Task<Event> GetEventAsync(string name)
		{
			var eventDR = dbContext.FetchRecordAsync(sql.SelectEvent(name), GetEvent);
			return await eventDR;
		}

		public async Task<IEnumerable<Event>> GetEventListAsync(string name = "")
		{
			var events = await dbContext.FetchRecordSetAsync(sql.SelectEvents(name), GetEvent);
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
			var tickets = dbContext.FetchRecordSetAsync(sql.SelectTicket(idEvent, idSector), GetTicket);
			return await tickets;
		}

		public async Task AddEventAsync(object[] paramValue)
		{
			await dbContext.AddRecordAsync(sql.InsertEvent(), paramValue, CreateEventParams);
		}

		public async Task UpdateEventAsync(object[] paramValue)
		{
			await dbContext.UpdateRecordAsync(sql.UpdateEvent(), paramValue, CreateUpdateParams);
		}

		public async Task DeleteEventAsync(object[] paramValue)
		{
			await dbContext.RemoveRecordAsync(sql.DeleteEvent(), paramValue, CreateDeleteParams);
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

		private void CreateUpdateParams(IDbCommand cmd)
		{
			cmd.Parameters.Add(dbContext.CreateParameter("?ID", DbType.Int64, cmd));
			CreateEventParams(cmd);
		}

		private void CreateDeleteParams(IDbCommand cmd)
		{
			cmd.Parameters.Add(dbContext.CreateParameter("?ID", DbType.Int64, cmd));
		}

		public async Task<int> AddTickets(object[] sqlParamValue, uint seatingCount)
		{
			var HS = new HashSet<object[]>();
			
			for (int i = 0; i < seatingCount; i++)
			{
				sqlParamValue[3] = i + 1;
				HS.Add(sqlParamValue.ToArray());
			}
			return await dbContext.AddManyRecordsAsync(sql.InsertTicket(), HS, CreateTicketParams);
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
