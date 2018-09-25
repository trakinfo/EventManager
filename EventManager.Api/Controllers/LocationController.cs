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

		[Route("address")]
		[HttpGet]
		public async Task<IActionResult> GetAddressList(string name = "")
		{
			var addressList = await locationService.BrowseAsync(name);
			return Json(addressList);
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
		[Route("address")]
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] NewAddress address)
		{
			await locationService.CreateAddressAsync(
							address.PlaceName,
							address.StreetName,
							address.PropertyNumber,
							address.ApartmentNumber,
							address.PostalCode,
							address.PostOffice
							);
			return Created($"", null);
		}
	}
}