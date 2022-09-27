using Microsoft.AspNetCore.SignalR;

namespace Realtime.API.Services;

public interface ILocationHubService
{
    Task GetLocation(string lat, string lon);
}

public class LocationHub : Hub<ILocationHubService>
{
    
}