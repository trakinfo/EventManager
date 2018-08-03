using EventManager.Core.Domain;
using EventManager.Core.Repository;
using EventManager.Infrastructure.Command.Event;
using EventManager.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Api.Controllers
{
	[Route("events")]
	public class EventController : Controller
	{
		readonly IEventService _eventService;
		public EventController(IEventService eventService)
		{
			_eventService = eventService;
		}

		[HttpGet]
		public async Task<IActionResult> Get(string name = "")
		{
			var events = await _eventService.BrowseAsync(name);
			return Json(events);
		}
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] NewEvent newEvent)
		{
			var insertedId = await _eventService.CreateAsync(
				newEvent.Name,
				newEvent.Description,
				newEvent.IdLocation,
				newEvent.StartDate,
				newEvent.EndDate,
				newEvent.Creator,
				newEvent.HostIP
				);
			return Created($"/events/{insertedId}", null);
		}

		[HttpPost("{eventId}")]
		public async Task<IActionResult> Post(ulong eventId)
		{
			await _eventService.CreateTicketCollectionAsync(eventId);
			return Created();
		}

		[HttpPut("{eventId}")]
		public async Task<IActionResult> Put(ulong eventId, [FromBody] NewEvent Event)
		{
			await _eventService.UpdateAsync(
				eventId,
				Event.Name,
				Event.Description,
				Event.IdLocation,
				Event.StartDate,
				Event.EndDate,
				Event.Creator,
				Event.HostIP
				);
			return NoContent();
		}

		[HttpDelete("{eventId}")]
		public async Task<IActionResult> Delete(ulong eventId)
		{
			await _eventService.DeleteAsync(eventId);
			return NoContent();
		}
	}
}
