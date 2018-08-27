using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManager.Infrastructure.Command.Location;
using EventManager.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Api.Controllers
{
   
    [Route("Locations")]
    public class LocationController : Controller
    {
		ILocationService locationService;

		public LocationController(ILocationService service)
		{
			locationService = service;
		}
		[HttpGet]
		public async Task<IActionResult> Get(string name = "")
		{
			var locationList = await locationService.BrowseAsync(name);
			return Json(locationList);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] NewLocation location)
		{
			await locationService.CreateAsync(
				location.Name,
				location.IdAddress,
				location.PhoneNumber,
				location.Email,
				location.www,
				location.Creator,
				location.HostIP
				);
			return Created($"", null);
		}
    }
}