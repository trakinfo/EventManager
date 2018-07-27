using EventManager.Core.Domain;
using EventManager.Core.Repository;
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
		//[HttpPost]
		//public async Task<IActionResult> Post([FromBody] Event @event)
		//{
		//	await _eventService.CreateAsync(
		//		@event.Name,
		//		@event.Location,
		//		@event.StartDate,
		//		@event.EndDate,
		//		@event.Creator
		//		);
		//	return Created();
		//}
	}
}
