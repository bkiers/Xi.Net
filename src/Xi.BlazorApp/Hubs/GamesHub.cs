namespace Xi.BlazorApp.Hubs
{
  using System;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.SignalR;
  using Microsoft.Extensions.Logging;

  public class GamesHub : Hub
  {
    public const string HubUrl = "/games-hub";

    private readonly ILogger<GamesHub> logger;

    public GamesHub(ILogger<GamesHub> logger)
    {
      this.logger = logger;
    }

    public async Task MoveMade(int gameId)
    {
      await this.Clients.All.SendAsync("MoveMade", gameId);
    }

    public override Task OnConnectedAsync()
    {
      this.logger.LogDebug($"{this.Context.ConnectionId} connected");

      return base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? e)
    {
      this.logger.LogDebug($"Disconnected {e?.Message} {this.Context.ConnectionId}");

      await base.OnDisconnectedAsync(e);
    }
  }
}