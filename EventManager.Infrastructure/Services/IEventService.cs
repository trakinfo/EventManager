﻿using EventManager.Core.Domain;
using EventManager.Core.Globals;
using EventManager.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Infrastructure.Services
{
    public interface IEventService
    {
		Task<EventDto> GetAsync(ulong id);
		Task<long> CreateAsync(string name, string descripion, ulong? idLocation, DateTime startDate, DateTime endDate, string creator, string hostIP);
		Task<IEnumerable<EventDto>> BrowseAsync(string name = null);
		//Task<Ticket> CreateTicketAsync(int seatingNumber, Sector sector, decimal price, string creator, string hostIP);
		//Task<Sector> CreateSectorAsync(string name, string description, int seatingCount, string creator, string hostIP);
		Task CreateTicketCollectionAsync(ulong eventId);
		Task UpdateAsync(ulong id, string name, string description, ulong? idLocation, DateTime startDate, DateTime endDate, string modifier, string hostIP);
		Task DeleteTicketsAsync(ISet<Ticket> tickets);
		Task DeleteAsync(ulong id);
    }
}
