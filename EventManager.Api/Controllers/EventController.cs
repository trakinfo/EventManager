using EventManager.Core.Repository;
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
		readonly IEventRepository _eventRepository;
		public EventController(IEventRepository eventRepository)
		{
			_eventRepository = eventRepository;
		}

		[HttpGet]
		public async Task<IActionResult> Get(string name = "")
		{
			var events = await _eventRepository.BrowseAsync(name);
			return Json(events);
		}
	}
}
