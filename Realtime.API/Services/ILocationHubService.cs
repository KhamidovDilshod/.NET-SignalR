using Microsoft.AspNetCore.SignalR;
using Realtime.API.Entities;

namespace Realtime.API.Services;

public class LocationHub : Hub
{
    private readonly IHubContext<LocationHub> _context;

    public LocationHub(IHubContext<LocationHub> context)
    {
        _context = context;
    }
    public async Task SetLocation(Coordinates coordinates)
    {
        await _context.Clients.All.SendAsync("GetLocation", coordinates);
    }
}