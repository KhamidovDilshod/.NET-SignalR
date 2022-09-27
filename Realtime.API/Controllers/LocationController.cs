using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Realtime.API.Entities;
using Realtime.API.Services;

namespace Realtime.API.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    private readonly IHubContext<LocationHub, ILocationHubService> _hubContext;

    public LocationController(IHubContext<LocationHub, ILocationHubService> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost]
    public string Post([FromBody] Coordinates coordinates)
    {
        string retMessage;
        try
        {
            _hubContext.Clients.All.GetLocation(coordinates.Latitude, coordinates.Longitude);
            retMessage = "Success";
        }
        catch (Exception e)
        {
            retMessage = e.ToString();
        }

        return retMessage;
    }

    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
        return new string[] {"Hello", "World"};
    }
}