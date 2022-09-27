using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Realtime.API.Entities;
using Realtime.API.Services;

namespace Realtime.API.Controllers;

[Produces("application/json")]
[Route("api")]
[ApiController]
public class LocationController : ControllerBase
{
    private readonly LocationHub _hub;

    public LocationController(LocationHub hub)
    {
        _hub = hub;
    }

    [HttpPost]
    public async Task<string> Post([FromBody] Coordinates coordinates)
    {
        string retMessage;
        try
        {
            await _hub.SetLocation(coordinates);
            retMessage = "Success";
        }
        catch (Exception e)
        {
            retMessage = e.ToString();
        }

        return retMessage;
    }

    //Use this Get method to test if the route is working by viewing the response
    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
        return new string[] {"Hello", "World"};
    }
}